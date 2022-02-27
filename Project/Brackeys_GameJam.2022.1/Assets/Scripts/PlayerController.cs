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


    [SerializeField] private bool isStaying = false;
    [SerializeField] private GameObject objectForInteraction;

    [SerializeField] private float hidingSpeed = 10;

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip doorSound;
    [SerializeField] private AudioClip levelSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip keySound;

    [SerializeField] public GameObject audioSourceObject;

    //[SerializeField] private GameObject audioSourceObject;

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
        //audioSource = audioSourceObject.GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (isStaying)
        {
            if (Input.GetKeyDown(KeyCode.W) && allowInteraction)
            {
                Interact(objectForInteraction);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MenuEscape");


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectForInteraction = collision.gameObject;
        
        if (objectForInteraction.CompareTag("Key"))
        {
            hasKey = true;
            audioSource.PlayOneShot(keySound);
            Destroy(collision.gameObject);
        }
        else if (objectForInteraction.CompareTag("Door"))
        {
            Doorcontroller doorcontroller = objectForInteraction.GetComponent<Doorcontroller>();

            if (doorcontroller.isOpened)
                doorcontroller.ShowPrompt();

            if (hasKey)
            {
                audioSource.clip = doorSound;
                doorcontroller.ShowPrompt();
                doorcontroller.OpenDoor(audioSource);
            }

            Debug.Log("change door sprite");
        }
        else if (objectForInteraction.CompareTag("Rock"))
        {
            RockController rockController = objectForInteraction.GetComponent<RockController>();
            rockController.ShowPrompt();
        }
        else if (objectForInteraction.CompareTag("ScreenChangeBox"))
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
        else if (objectForInteraction.CompareTag("BeastDetectBox"))
        {
            objectForInteraction.GetComponent<BeastLevel2_3>().Collided(gameObject);
            //Destroy(collision.gameObject);
        }
        
    }



    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject == objectForInteraction)
        {
            Debug.Log("staying");
            isStaying = true;
            
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            objectForInteraction = collision.gameObject;
            RockController rockController = objectForInteraction.GetComponent<RockController>();
            rockController.ShowPrompt();
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
                        audioSourceObject.GetComponent<AudioSource>().clip = levelSound;
                        interactedObject.GetComponent<Doorcontroller>().LoadNextLevel(audioSourceObject.GetComponent<AudioSource>());
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
        StartCoroutine(deathController.DeathSequence(gameObject.GetComponent<PlayerController>()));
        audioSource.PlayOneShot(deathSound);
    }


}
