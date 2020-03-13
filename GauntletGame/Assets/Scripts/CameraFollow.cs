using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] GameObject playerToFollow;
    // Thing that the Camera should follow

    Transform target;




    // Use this for initialization
    void Start () 
    {
        target = playerToFollow.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () 
    {
                transform.position = new Vector3(playerToFollow.transform.position.x, playerToFollow.transform.position.y + 1.0f, transform.position.z);
	}
}
