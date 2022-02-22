using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorcontroller : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[2];

    [SerializeField] private GameObject text;
    [SerializeField] private string nextSceneName;

    public bool isOpened = false;


    private void Awake()
    {
        text.SetActive(false);
    }

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
    
    public void OpenDoor()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
        isOpened = true;
    }

    public void ShowPrompt()
    {
        text.SetActive(true);
    }

    public void HidePrompt()
    {
        text.SetActive(false);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(nextSceneName);
    }

}
