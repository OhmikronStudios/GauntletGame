using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] public int health = 5;
    public Rigidbody2D rb;
    public bool isFacingRight;
    [SerializeField] public float moveSpeed = 2f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        
    }

    void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x *= -1;
        transform.localScale = scaleFactor;
    }

    void Move()
    {
        if (isFacingRight)
        {
            rb.velocity = new Vector2(moveSpeed, 0);
        }

        else
        {
            rb.velocity = new Vector2(-moveSpeed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            flip();
        }

        else if (collision.gameObject.tag == "Weapon")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
