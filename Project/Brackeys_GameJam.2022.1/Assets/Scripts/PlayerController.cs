using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasKey = false;

    public bool isHiding = false;
    public bool allowWalking = true;

    public bool isGrounded;


    [SerializeField] private bool isStaying = false;
    [SerializeField] private GameObject objectForInteraction;

    [SerializeField] private float hidingSpeed = 10;

    //public GameObject door;


    private void Update()
    {

        if (isStaying)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Interact(objectForInteraction);
            }
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

        yield return null;
    }

    public IEnumerator UnHide(GameObject hidingChar, Vector2 initialPos, Vector2 hidePos, float speed)
    {
        if (!isGrounded)
            speed = 12;

        int i = 0;
        float t = 0;

        while (Vector2.Distance(hidingChar.transform.position, hidePos) < 0.6)
        {
            t += Time.deltaTime * speed;
            if (i >= 1000)
                break;
            hidingChar.transform.position = Vector2.MoveTowards(initialPos, new Vector2(hidePos.x + (0.7f * hidingChar.transform.localScale.x), hidePos.y), t);
            i++;
            Debug.Log("occccrwehf892fesFDSSF");
            yield return null;
        }


        yield return null;
    }


}
