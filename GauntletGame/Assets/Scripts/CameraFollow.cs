using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] GameObject playerToFollow;
    [SerializeField] float cameraHeight = 2.0f;
    // Thing that the Camera should follow

    Transform target;
    



    // Use this for initialization
    void Start () 
    {
        target = playerToFollow.GetComponent<Transform>();
        transform.position = new Vector3(playerToFollow.transform.position.x, playerToFollow.transform.position.y + cameraHeight, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () 
    {
        transform.position = new Vector3(playerToFollow.transform.position.x, transform.position.y, transform.position.z);
	}

    //void OnTriggerEnter2D(collision collision)
    //{
    //    if (collision.gameObject.tag == "CameraMove")
    //    {
    //        transform.position = new Vector3(playerToFollow.transform.position.x, playerToFollow.transform.position.y + 1.0f, transform.position.z);
    //    }
    //}
}
