using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;


    public float moveSpeed = 1;

    public Vector2 jumpDir;
    public float jumpStrength = 1;
    [SerializeField] private float coyoteTime = 0.05f;
    private bool allowJumpCheck = true;

    public BoxCollider2D boxCollider2D;

    private Animator animator;
    private string currentAnimation;

    private float direction;


    private void Awake()
    {
        jumpDir = new Vector2(0, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            Jump();

        direction = Input.GetAxisRaw("Horizontal");

        if (direction != 0)
        {
            transform.localScale = new Vector2(1 * direction, 1);

            ChangeAnimation("Walk");
        }
        else
        {
            ChangeAnimation("Idle");
        }
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0);
    }


    private void Jump()
    {
        Debug.Log("Jump");
        gameObject.GetComponent<Rigidbody2D>().AddForce(jumpDir * jumpStrength, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        if (!allowJumpCheck)
            return true;

        RaycastHit2D hitColliders2D = Physics2D.BoxCast(new Vector2(boxCollider2D.bounds.center.x, boxCollider2D.bounds.center.y - 0.3f),
            new Vector2(boxCollider2D.transform.localScale.x - 0.1f,
            boxCollider2D.transform.localScale.y + 0.2f),
            0,
            Vector2.down, 0, groundLayerMask);

        if(hitColliders2D.collider != null)
        {
            StartCoroutine(CoyoteTime(coyoteTime));
        }

        return hitColliders2D.collider != null;
    }

    public IEnumerator CoyoteTime(float coyoteTime)
    {
        allowJumpCheck = false;

        for(float t = 0; t <= coyoteTime; t+=0.01f)
        {
            yield return new WaitForSeconds(0.01f);
        }

        allowJumpCheck = true;

        yield return null;
    }


    void ChangeAnimation(string animation)
    {
        if (currentAnimation == animation)
            return;

        animator.Play(animation);
        currentAnimation = animation;
    }
}
