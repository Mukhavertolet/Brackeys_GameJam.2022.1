using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaitForText : MonoBehaviour
{
    [SerializeField] private float secondsToWait;
    [SerializeField] private string nextScene;



    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Timer()
    {
        for (float t = 0; t < secondsToWait; t += 0.1f)
        {
            yield return new WaitForSeconds(0.1f);
        }


        SceneManager.LoadScene(nextScene);

        yield return null;



    }

}
