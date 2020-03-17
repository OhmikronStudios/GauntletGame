using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    [SerializeField] int axes = 3;
    [SerializeField] int score = 500;



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
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "PowerUp_Axe")
            {
                FindObjectOfType<Player>().GainAxes(axes);
            }
            else if(gameObject.tag == "PowerUp_Score")
            {
                FindObjectOfType<HUDManager>().AddToScore(score);
            }
            Destroy(gameObject);
        }

    }

 




}
