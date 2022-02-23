using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenChange : MonoBehaviour
{
    [SerializeField] private GameObject cameraToMove;
    [SerializeField] private GameObject moveToPos;
    [SerializeField] private GameObject movePlyarToPos;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ChangeScreen(GameObject character)
    {
        //play screen change animation

        cameraToMove.transform.position = moveToPos.transform.position;
        character.transform.position = movePlyarToPos.transform.position;


        //finish playing screen change animation


        yield return null;
    }

}
