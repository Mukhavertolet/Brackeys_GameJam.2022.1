using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathController : MonoBehaviour
{
    public string deathZoneType;

   
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame


    public IEnumerator DeathSequence(PlayerController playerController)
    {
        //play animation (depends on type of death zone player got in)


        switch (deathZoneType)
        {
            
            case "PixelFallRegular":
                {
                    yield return new WaitForSeconds(0.3f);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                }
            case "PixelSpike":
                {
                    
                    //yield return new WaitForSeconds(0.3f);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                }
            case "RealHand":
                {
                    //yield return new WaitForSeconds(0.3f);
                    StartCoroutine(KillTimer(1f, playerController));
                    break;
                }
            case "BeastLight":
                {
                    //yield return new WaitForSeconds(0.3f);
                    StartCoroutine(KillTimer(0.5f, playerController));
                    break;
                }
            default:
                {
                    yield return new WaitForSeconds(0.3f);
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                }

        }




        yield return null;
    }

    private IEnumerator KillTimer(float timeLimit, PlayerController playerController)
    {
        for (float t = 0; t < timeLimit; t += 0.1f)
        {
            Debug.Log("trhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrhrho");
            yield return new WaitForSeconds(0.1f);

            if (playerController.isHiding)
            {
                break;
            }
        }
        if (!playerController.isHiding)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        yield return null;


    }

}