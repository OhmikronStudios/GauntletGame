using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    [Header("powerup prefabs")]
    [SerializeField] GameObject PickUp1;
    [SerializeField] GameObject PickUp2;

    float itemNumber = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        itemNumber = Random.Range(0, 2);
        

        if (itemNumber < 1)
            {
                GameObject pickUp = Instantiate(PickUp1, transform.position, Quaternion.identity) as GameObject;
            }
            else if (itemNumber >= 1)
            {
                GameObject pickUp = Instantiate(PickUp2, transform.position, Quaternion.identity) as GameObject;
            }
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

       
}
