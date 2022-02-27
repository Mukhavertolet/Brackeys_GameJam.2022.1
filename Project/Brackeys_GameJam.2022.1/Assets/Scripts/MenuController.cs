using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip klickSound;
    [SerializeField] public AudioClip hoverSound;
    [SerializeField] public AudioClip happyJingle;


    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MenuEscape")
            HappyJingle();
    }


    public void Play()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void Exit()
    { 
        
        Application.Quit();
    }

    public void Klick()
    {
        audioSource.PlayOneShot(klickSound);
    }

    public void Hover()
    {
        audioSource.PlayOneShot(hoverSound);
    }

    public void HappyJingle()
    {
        audioSource.PlayOneShot(happyJingle);
    }



}

