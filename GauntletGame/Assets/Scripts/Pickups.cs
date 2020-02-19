using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Picked UP");
        if (this.gameObject.tag == "Axe")
        {
            if (collision.gameObject.tag == "Player")
            {
               
                Destroy(gameObject);
               
            }
        }
    }

 




}
