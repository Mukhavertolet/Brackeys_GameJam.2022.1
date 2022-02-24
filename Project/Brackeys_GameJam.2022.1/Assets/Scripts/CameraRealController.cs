using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRealController : MonoBehaviour
{
    public GameObject objectToFollow;
    private PlayerController playerController;

    public float cameraSpeed;
    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        objectToFollow = GameObject.FindGameObjectWithTag("Player");
        playerController = objectToFollow.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //if(!playerController.isGrounded)
        //{
        //    Vector3 targetPosition = objectToFollow.transform.TransformPoint(new Vector3(0, 0, -50));
        //    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetPosition.x, transform.position.y, targetPosition.z - 50), ref velocity, cameraSpeed * Time.deltaTime);

        //}
        //else
        //{
            Vector3 targetPosition = objectToFollow.transform.TransformPoint(new Vector3(0, 0, -50));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition + new Vector3(0, 1f, -50), ref velocity, cameraSpeed * Time.deltaTime);

        //}

    }

}
