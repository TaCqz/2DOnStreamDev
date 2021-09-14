using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // PlayerObject and CameraOffset
    [SerializeField] private GameObject playerObject;
    [SerializeField] private float offset = 0f;
    private float startTime = 0f;
    private float journeyTime = 1.0f;

    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float fracComplete = (Time.time - startTime) / journeyTime;
        // Moves the camera with the player (within level bounds)
        if (playerObject != null)
        {
            this.transform.position = new Vector3(playerObject.transform.position.x, playerObject.transform.position.y,
                this.transform.position.z);
            if (this.transform.position.x <= 4)
                this.transform.position = new Vector3(4, this.transform.position.y, this.transform.position.z);
        }
    }


}
