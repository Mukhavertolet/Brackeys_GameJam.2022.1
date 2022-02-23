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
    void Update()
    {

    }

    public IEnumerator DeathSequence()
    {
        //play animation (depends on type of death zone player got in)

        switch (deathZoneType)
        {
            case "PixelFallRegular":
                {
                    yield return new WaitForSeconds(0.3f);
                    break;
                }
            default:
                {
                    break;
                }

        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);



        yield return null;
    }


}
