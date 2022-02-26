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
    [SerializeField] private AudioClip typing;

    public float time = 0.2f;

    private void Start()
    {
        text = TextGameOdject.text;
        TextGameOdject.text = "";
        StartCoroutine(TextCoroutine());
        audioSource.clip = typing;
        StartCoroutine(Play());
    }

    private void Update()
    {
    }
    IEnumerator TextCoroutine()
    {
       foreach (char abc in text)
       {
           TextGameOdject.text += abc;
           yield return new WaitForSeconds(timeBetweenLetters);
       }
    }

    IEnumerator Play()
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(PlayCrtn());
    }
    IEnumerator PlayCrtn()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 13; j++)
            {
                audioSource.Play();
                yield return new WaitForSeconds(timeBetweenLetters);
            }
            yield return new WaitForSeconds(1.5f);
        }
    }
}
