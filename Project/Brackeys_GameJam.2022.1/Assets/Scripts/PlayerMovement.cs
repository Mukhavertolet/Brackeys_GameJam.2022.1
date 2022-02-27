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

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip footstepsSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] public AudioClip landingSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip spikeDeathSound;
    

    [SerializeField] private PlayerController playerController;

    [SerializeField] private GameObject audioSourceObject;
    [SerializeField] private AudioSource audioSourceObj;

    [SerializeField] public string deathZoneType;
    

    private void Awake()
    {
        jumpDir = new Vector2(0, 1);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSourceObj = audioSourceObject.GetComponent<AudioSource>();
        //playerController.audioSource = audioSource;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.clip = jumpSound;
            
            audioSource.Play();

           Jump();
        }

        direction = Input.GetAxisRaw("Horizontal");

        if (direction != 0)
        {
            Debug.Log(IsGrounded());

            if (IsGrounded())
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.clip = footstepsSound;

                    audioSource.Play();

                }
                

            }
            transform.localScale = new Vector2(1 * direction, 1);
            
            ChangeAnimation("Walk");
        }
        else
        {
            if (audioSource.clip != jumpSound && audioSource != landingSound)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                
            }

            ChangeAnimation("Idle");
        }

        
    }

    

    private void FixedUpdate()
    {
        gameObject.transform.Translate(direction * moveSpeed * Time.deltaTime, 0, 0);
    }


    private void Jump()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();

        Debug.Log("Jump");


        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(jumpDir * jumpStrength, ForceMode2D.Impulse);
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

        if (hitColliders2D.collider != null)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(LayerMask.LayerToName(collision.gameObject.layer));

        if (LayerMask.LayerToName(collision.gameObject.layer)=="DeathZone")
        {
            if (!audioSourceObj.isPlaying)
            {
                audioSourceObj.PlayOneShot(deathSound);
                
            }
        }
        else if (LayerMask.LayerToName(collision.gameObject.layer) == "Spike")
        {
            if (!audioSourceObj.isPlaying)
            {
                audioSourceObj.PlayOneShot(spikeDeathSound);
            }
        }
    }
}
