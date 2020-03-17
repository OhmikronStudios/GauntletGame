using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] public int scoreValue = 100;

    public float fireRate = 3f;
    public float fireRange = 1f;
    public Transform leftSpawnPoint;
    public Transform rightSpawnPoint;
    public bool shootLeft;
    public GameObject target = null;
    public GameObject plantShot;
    float timeSinceLastFire = 0;
    float distance;
    Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootDirectionCheck();

        distance = Vector2.Distance(transform.position, target.transform.position);
        
            if (distance <= fireRange)
            {
                
                timeSinceLastFire += Time.deltaTime;
                if (timeSinceLastFire >= fireRate)
                {
                     timeSinceLastFire = 0;
                     anim.SetTrigger("Shoot");
                }
            }

    }

    void fire()
    {
        //anim.SetTrigger("Shoot");
        if (shootLeft)
        {
            Instantiate(plantShot, leftSpawnPoint.position, leftSpawnPoint.rotation);
        }
        else
        {
            Instantiate(plantShot, rightSpawnPoint.position, rightSpawnPoint.rotation);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("plant struck");
        if (collision.gameObject.tag == "Weapon")
        {
            Debug.Log("plant struck by sword");
            FindObjectOfType<HUDManager>().AddToScore(scoreValue);
            Destroy(gameObject);
        }
    }

    void shootDirectionCheck()
    {
        if (target.transform.position.x < transform.position.x)
        {
            shootLeft = true;
        }
        else
        {
            shootLeft = false;
        }
    }

   
}

