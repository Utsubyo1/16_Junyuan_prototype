using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    float movespeed = 5f;
    float rotatespeed = 2f;

    public int maxHealth = 100;
    public int currentHealth;

    public HealthbarScript healthBar;
    public Rigidbody Playerrb;
    public Animator Playeranim;

    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        
    }
    void movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movespeed);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            Playeranim.SetBool("isrun", true);
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * movespeed);
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            Playeranim.SetBool("isrun", true);

        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            Playeranim.SetBool("isrun", false);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * movespeed);
            //transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.Rotate(new Vector3(0, h * rotatespeed, 0));
            Playeranim.SetBool("isrun", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(Vector3.forward * Time.deltaTime * movespeed);
            //transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.Rotate(new Vector3(0, h * rotatespeed, 0));
            Playeranim.SetBool("isrun", true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Playeranim.SetBool("isrun", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Playeranim.SetTrigger("Fire");
            takeDamage(20);
            
        }
    }

   void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }
}
