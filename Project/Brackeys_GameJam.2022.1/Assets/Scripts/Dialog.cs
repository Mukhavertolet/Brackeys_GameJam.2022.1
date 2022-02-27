using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI TextGameOdject;
    public float timeBetweenLetters = 0.1f;

    private string text;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip textSound;

    private void Start()
    {
        text = TextGameOdject.text;
        TextGameOdject.text = "";
        StartCoroutine(TextCoroutine());
        StartCoroutine(Delay());
        
        audioSource.clip = textSound;
    }
    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);

        for (int j = 0; j < 3; j++)
        {
            for (int i = 0; i < 13; i++)
            {
                audioSource.Play();

                yield return new WaitForSeconds(timeBetweenLetters);

            }

            yield return new WaitForSeconds(1.5f);

        }
        

        
    }
    IEnumerator TextCoroutine()
    {
        

        foreach (char abc in text)
        {
            TextGameOdject.text += abc;
            yield return new WaitForSeconds(timeBetweenLetters);
        }






    }
}
