using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    
    [SerializeField] public int scoreValue = 100;

    public Rigidbody2D rb;
    public bool isFacingRight;
    [SerializeField] public float moveSpeed = 2f;

    // Create a reference to AudioSource to be used when playing sounds for 'Character'
    public AudioSource aSource;

    // Used to store Audio files to played
    public AudioClip dieSnd;
    

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
        if (collision.gameObject.tag == "Barrier" || collision.gameObject.tag == "Enemy")
        {
            flip();
        }

        else if (collision.gameObject.tag == "Weapon")
        {
            //AudioManager.instance.PlaySingleSound(dieSnd, 5.0f);
            PlaySound(dieSnd, 1.0f);
            FindObjectOfType<HUDManager>().AddToScore(scoreValue);
            Destroy(gameObject);
        }
    }

  void flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scaleFactor = transform.localScale;
        scaleFactor.x *= -1;
        transform.localScale = scaleFactor;
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        // Assign 'AudioClip' when function is called
        aSource.clip = clip;

        // Assign 'volume' to 'AudioSource' when function is called
        aSource.volume = volume;

        // Play assigned 'clip' through 'AudioSource'
        aSource.Play();
    }

}

