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

    [Header("Sounds")]
    // Create a reference to AudioSource to be used when playing sounds for 'Character'
    public AudioSource aSource;
    // Used to store Audio files to played
    public AudioClip jumpSnd;
    public AudioClip deathSnd;
    public AudioClip hurtSnd;
    public AudioClip swingSnd;
    public AudioClip throwSnd;





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

        // Check if variable is set to something
        if (!aSource)
        {
            // Add an 'AudioSource' because it is not added
            aSource = gameObject.AddComponent<AudioSource>();
            // Change variables on the 'AudioSource'
            aSource.loop = false;
            aSource.playOnAwake = false;
        }


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
            PlaySound(jumpSnd, 0.25f);
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
            
                {rb.velocity = new Vector2(0,0);}
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
            PlaySound(swingSnd, 1.0f);
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
            PlaySound(throwSnd, 1.0f);
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
            Debug.Log(collision.gameObject.name);
            health--;
            PlaySound(hurtSnd, 0.5f);
            if (health <= 0)
            {
                PlaySound(deathSnd, 1.0f);
                Destroy(gameObject);
                FindObjectOfType<GameManager>().LoadGameOver();
            }

            else
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetTrigger("hurt");
            }
        }

        


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathBox")
        {
            health = 0;
            PlaySound(hurtSnd, 0.5f);
            if (health <= 0)
            {
                //PlaySound(deathSnd, 1.0f);
                Destroy(gameObject, 2.5f);
                FindObjectOfType<GameManager>().LoadGameOver();
            }
        }
        if (collision.gameObject.tag == "EnemyProj")
        {
            health--;
            PlaySound(hurtSnd, 0.5f);
            if (health <= 0)
            {
                PlaySound(deathSnd, 1.0f);
                Destroy(gameObject);
                FindObjectOfType<GameManager>().LoadGameOver();
            }

            else
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                anim.SetTrigger("hurt");
            }
        }
    }






    // Called when a SFX needs to be played
    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        // Assign 'AudioClip' when function is called
        aSource.clip = clip;

        // Assign 'volume' to 'AudioSource' when function is called
        aSource.volume = volume;

        // Play assigned 'clip' through 'AudioSource'
        aSource.Play();
    }
    
    public int GetHealth()
    {
        return health;
    }
    public int GetAxes()
    {
        return axeCount;
    }

    public void GainAxes(int axes)
    {
        axeCount += axes;
    }
}
