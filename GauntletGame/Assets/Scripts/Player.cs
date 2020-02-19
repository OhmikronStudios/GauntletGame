using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    bool isFacingLeft = true;
    BoxCollider2D collider2D;

    [Header("Player Controls")]
    [SerializeField] public float speed = 4f;
    [SerializeField] public float jumpForce = 3f;
    [SerializeField] bool isGrounded;
    [SerializeField] public int health = 5;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;
    [SerializeField] GameObject sword;


    [Header("Special Attacks")]

    [SerializeField] GameObject axe;
    [SerializeField] int axeCount = 0;

    [SerializeField] GameObject projectileSpawn;
    float projectileSpeed = 4f;



    

    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
            if (isGrounded)
            { anim.SetBool("grounded", true); }
            else if (!isGrounded)
            { anim.SetBool("grounded", false); }

        Move();
        Attack();
        Crouch();
       
        if (Input.GetButtonDown("SpecialAttack"))
            {SpecialAttack();}



    }
    private void Move()
    {
        //Horizontal Movement
        float moveSpeed = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveSpeed * speed, rb.velocity.y);
        if (moveSpeed > 0 && !isFacingLeft || moveSpeed < 0 && isFacingLeft)
        {
            flip();
        }
        anim.SetFloat("speed", Mathf.Abs(moveSpeed));
        
        //Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            Debug.Log("Jump");
        }

    }


    void flip()
    {
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x *= -1;
        transform.localScale = scaleFactor;

        isFacingLeft = !isFacingLeft;
    }

    private void Crouch()
    {
        float colX = 0.13f;
        float colY = 0.25f;
        float offsetY = 0.125f;
        float offsetX = 0.0f;

        if (Input.GetButtonDown("Crouch"))
        {
            anim.SetBool("crouched", true);
            collider2D.size = new Vector2(colX, colY / 2);
            collider2D.offset = new Vector2(offsetX, offsetY / 2);
            rb.velocity = Vector2.zero;
            Debug.Log("Crouching");


        }
        if (Input.GetButtonUp("Crouch"))
        {
            anim.SetBool("crouched", false);
            collider2D.size = new Vector2(colX, colY);
            collider2D.offset = new Vector2(offsetX, offsetY);
        }
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("attack");
            GameObject Sword = Instantiate(sword, projectileSpawn.transform.position, Quaternion.identity) as GameObject;
            
        }
    }

    void SpecialAttack()
    {
        if (axeCount > 0)
        {
            anim.SetTrigger("toss");
            Debug.Log("Toss");
            GameObject Axe = Instantiate(axe, projectileSpawn.transform.position, Quaternion.identity) as GameObject;
            if (isFacingLeft)
            {
                Axe.GetComponent<Rigidbody2D>().velocity = new Vector2(projectileSpeed, projectileSpeed);
            }

            else if (!isFacingLeft)
            {
                Axe.GetComponent<Rigidbody2D>().velocity = new Vector2(-projectileSpeed, projectileSpeed);
                Axe.GetComponent<SpriteRenderer>().flipX = false;
            }
            axeCount -= 1;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }

        

    }


}
