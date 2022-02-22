using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool hasKey = false;

    public bool isHiding = false;
    public bool allowWalking = true;
    public bool allowInteraction = true;

    public bool isGrounded;


    [SerializeField] private bool isStaying = false;
    [SerializeField] private GameObject objectForInteraction;

    [SerializeField] private float hidingSpeed = 10;

    //[SerializeField] private SpriteRenderer headSprite;
    //[SerializeField] private SpriteRenderer bodySprite;
    //[SerializeField] private SpriteRenderer legLSprite;
    //[SerializeField] private SpriteRenderer legRSprite;

    //[SerializeField] private Color lightColor;
    //[SerializeField] private Color darkColor;

    //[SerializeField] private float colorChangeStep = 1;




    private void Update()
    {

        if (isStaying)
        {
            if (Input.GetKeyDown(KeyCode.W) && allowInteraction)
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
        allowInteraction = false;

        if (!isGrounded)
            speed = 12;

        int i = 0;
        float t = 0;

        //StartCoroutine(ChangeCharColor(lightColor, darkColor));

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

        //StartCoroutine(ChangeCharColor(darkColor, lightColor));


        allowInteraction = true;
        yield return null;
    }




    //public IEnumerator ChangeCharColor(Color fromColor, Color toColor)
    //{
    //    float t = 0;

    //    Color currentColor = fromColor;

    //    if (currentColor.r < toColor.r || currentColor.g < toColor.g || currentColor.b < toColor.b)
    //    {
    //        while (currentColor != toColor)
    //        {
    //            t += colorChangeStep * Time.deltaTime;

    //            headSprite.color = Color.Lerp(fromColor, toColor, t);
    //            bodySprite.color = Color.Lerp(fromColor, toColor, t);
    //            legLSprite.color = Color.Lerp(fromColor, toColor, t);
    //            legRSprite.color = Color.Lerp(fromColor, toColor, t);
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        while (currentColor != toColor)
    //        {
    //            headSprite.color = Color.Lerp(fromColor, toColor, t);
    //            bodySprite.color = Color.Lerp(fromColor, toColor, t);
    //            legLSprite.color = Color.Lerp(fromColor, toColor, t);
    //            legRSprite.color = Color.Lerp(fromColor, toColor, t);
    //            yield return null;
    //        }
    //    }


    //    yield return null;
    //}


}
