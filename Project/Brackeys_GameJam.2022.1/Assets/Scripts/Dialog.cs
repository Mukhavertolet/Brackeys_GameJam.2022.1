using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI TextGameOdject;
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
            yield return new WaitForSeconds(0.5f);
        }
    }
}
