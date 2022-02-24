using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

    public bool hasKey = false;
    public bool hasTheKey;

    public bool isHiding = false;
    public bool allowWalking = true;
    public bool allowInteraction = true;

    public bool isGrounded;

    public float direction;


    [SerializeField] private bool isStaying = false;
    [SerializeField] private GameObject objectForInteraction;

    [SerializeField] private float hidingSpeed = 10;

    [SerializeField] private ParticleSystem runParticle;
    [SerializeField] private ParticleSystem jumpParticle;


    //public GameObject door;

    private void Awake()
    {
        //bruh
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        hasTheKey = gameManager.playerHasTheKey;
       

    }


    private void Update()
    {   
        if (isGrounded)
        {
            if (direction != 0)
            {
                runParticle.Play();
            }
            else
            {
                runParticle.Stop();
            }
            

        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            jumpParticle.Play();
        }

            if (isStaying)
        {
            if (Input.GetKeyDown(KeyCode.W) && allowInteraction)
            {
                Interact(objectForInteraction);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (LayerMask.LayerToName(collision.gameObject.layer) == "Ground")
        {
            jumpParticle.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {   
        objectForInteraction = collision.gameObject;

        if (objectForInteraction.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(collision.gameObject);
        }
        else if (objectForInteraction.CompareTag("Door") && hasKey)
        {
            Doorcontroller doorcontroller = objectForInteraction.GetComponent<Doorcontroller>();

            doorcontroller.ShowPrompt();

            Debug.Log("change door sprite");
            doorcontroller.OpenDoor();
        }
        else if (objectForInteraction.CompareTag("Rock"))
        {
            RockController rockController = objectForInteraction.GetComponent<RockController>();
            rockController.ShowPrompt();
        }
        else if(objectForInteraction.CompareTag("ScreenChangeBox"))
        {
            ScreenChange screenChange = objectForInteraction.GetComponent<ScreenChange>();
            StartCoroutine(screenChange.ChangeScreen(gameObject));
        }
        else if (objectForInteraction.CompareTag("DeathZone"))
        {
            StartDeath(objectForInteraction.GetComponent<DeathController>());
        }
        else if (objectForInteraction.CompareTag("TheKey"))
        {
            hasTheKey = true;
            gameManager.playerHasTheKey = hasTheKey;
            Destroy(collision.gameObject);
        }
        else if (objectForInteraction.CompareTag("Lock") && hasTheKey)
        {
            hasTheKey = false;
            gameManager.playerHasTheKey = hasTheKey;
            Destroy(collision.gameObject);
        }
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == objectForInteraction)
        {
            Debug.Log("staying");
            isStaying = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == objectForInteraction)
        {
            if (objectForInteraction.CompareTag("Door"))
            {
                objectForInteraction.GetComponent<Doorcontroller>().HidePrompt();
            }

            if (objectForInteraction.CompareTag("Rock"))
            {
                objectForInteraction.GetComponent<RockController>().HidePrompt();
            }

            isStaying = false;
            objectForInteraction = null;
        }


    }

    private void Interact(GameObject interactedObject)
    {
        Debug.Log("interaction");

        switch (LayerMask.LayerToName(interactedObject.layer))
        {
            case "Door":
                {
                    if (interactedObject.GetComponent<Doorcontroller>().isOpened)
                    {
                        interactedObject.GetComponent<Doorcontroller>().LoadNextLevel();
                    }

                    break;
                }


            case "Rock":
                {
                    RockController rockController = interactedObject.GetComponent<RockController>();

                    if (!isHiding)
                    {
                        allowWalking = false;
                        StartCoroutine(Hide(gameObject, gameObject.transform.position, rockController.hidePoint.transform.position, hidingSpeed));
                        isHiding = true;
                    }
                    else
                    {
                        StartCoroutine(UnHide(gameObject, gameObject.transform.position, rockController.hidePoint.transform.position, hidingSpeed));
                        isHiding = false;
                        allowWalking = true;
                    }

                    break;
                }


            default:
                {
                    break;
                }
        }
    }

    public IEnumerator Hide(GameObject hidingChar, Vector2 initialPos, Vector2 hidePos, float speed)
    {
        allowInteraction = false;

        if (!isGrounded)
            speed = 12;

        int i = 0;
        float t = 0;

        while (Vector2.Distance(hidingChar.transform.position, hidePos) > 0.1)
        {
            t += Time.deltaTime * speed;
            if (i >= 1000)
                break;
            hidingChar.transform.position = Vector2.MoveTowards(initialPos, hidePos, t);
            i++;
            Debug.Log("occccrwehf892fesFDSSF");
            yield return null;
        }

        allowInteraction = true;
        yield return null;
    }

    public IEnumerator UnHide(GameObject hidingChar, Vector2 initialPos, Vector2 hidePos, float speed)
    {
        allowInteraction = false;

        if (!isGrounded)
            speed = 12;

        int i = 0;
        float t = 0;

        while (Vector2.Distance(hidingChar.transform.position, hidePos) < 1.3f)
        {
            t += Time.deltaTime * speed;
            if (i >= 1000)
                break;
            hidingChar.transform.position = Vector2.MoveTowards(initialPos, new Vector2(hidePos.x + (2f * hidingChar.transform.localScale.x), hidePos.y), t);
            i++;
            Debug.Log("occccrwehf892fesFDSSF");
            yield return null;
        }


        allowInteraction = true;
        yield return null;
    }

    public void StartDeath(DeathController deathController)
    {
        StartCoroutine(deathController.DeathSequence());
    }


}
