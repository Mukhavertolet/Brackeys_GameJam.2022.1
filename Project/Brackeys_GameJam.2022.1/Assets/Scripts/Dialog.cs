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



    private void Start()
    {
        text = TextGameOdject.text;
        TextGameOdject.text = "";
        StartCoroutine(TextCoroutine());
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
