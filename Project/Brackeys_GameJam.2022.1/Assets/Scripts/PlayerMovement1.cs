using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;


    public float moveSpeed = 1;

    public Vector2 jumpDir;
    public float jumpStrength = 1;

    public BoxCollider2D boxCollider2D;



    private void Awake()
    {
        jumpDir = new Vector2(0, 1);
    }

    void Update()
    {
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            Jump();


    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 0, 0);

    }


    private void Jump()
    {
        Debug.Log("Jump");
        gameObject.GetComponent<Rigidbody2D>().AddForce(jumpDir * jumpStrength, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hitColliders2D = Physics2D.BoxCast(boxCollider2D.bounds.center, new Vector2(boxCollider2D.transform.localScale.x - 0.1f, boxCollider2D.transform.localScale.y + 0.5f), 0, Vector2.down, 0, groundLayerMask);

        Debug.Log(hitColliders2D);

        return hitColliders2D.collider != null;
    }


}
