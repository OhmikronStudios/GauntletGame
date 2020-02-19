using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantShot : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);

        rb = GetComponent<Rigidbody2D>();
        if (transform.localRotation.y == 0)
        {
            speed = speed*1;
        }
        else
        {
            speed = speed*-1;
        }
        
        rb.velocity = new Vector3(speed, 0, 0);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
            Destroy(gameObject);
    }
}
