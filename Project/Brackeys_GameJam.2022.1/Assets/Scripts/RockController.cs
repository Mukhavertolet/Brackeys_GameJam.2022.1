using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour
{
    [SerializeField] private GameObject text;
    public GameObject hidePoint;

    private void Awake()
    {
        text.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ShowPrompt()
    {
        text.SetActive(true);
    }

    public void HidePrompt()
    {
        text.SetActive(false);
    }
}
