using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasKey = false;

    [SerializeField] private bool isStaying = false;
    [SerializeField] private GameObject objectForInteraction;

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

            isStaying = false;
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


            //case fkwemnfwejonof


            default:
                {
                    break;
                }
        }
    }


}
