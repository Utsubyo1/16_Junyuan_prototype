﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public static bool Playerdeath;
    
    //movement and rotate speed
    float movespeed = 5f; // movement speed
    float rotatespeed = 2f; // rotatespeed

    //Ammo count
    public int maxAmmo = 7; // maxammo
    private int currentAmmo; // current ammo
    public float reloadtime = 10f; // time for reload
    private bool isreloading = false; 

    public int maxHealth = 100; // max health
    int currentHealth; // current health

    
    public AudioClip[] AudioClipsArr;
    private AudioSource audiosource;
    public Text Hptext;
    public HealthbarScript healthBar;
    public Rigidbody Playerrb;
    public Animator Playeranim;

    //bullet spawn
    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    bool isdead = false;
    

    // Start is called before the first frame update
    void Start()
    {
        //check adn set ammo when start
        currentAmmo = maxAmmo;

        audiosource = GetComponent<AudioSource>();
        //check, set and display health when start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Hptext.GetComponent<Text>().text = (maxHealth + "/" + currentHealth);

        Playerdeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isdead == false)
        { movement(); }

        if (isreloading)
            return;

        if(currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        //Player death
        if(currentHealth <= 0)
        {
            Playerdeath = true;
            Die();
            isdead = true;
            
        }
    }
    void movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.W) && isdead == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movespeed);
            //transform.rotation = Quaternion.Euler(0, 0, 0);
            Playeranim.SetBool("isrun", true);
            
        }
        else if (Input.GetKey(KeyCode.S) && isdead == false)
        {
            transform.Translate(Vector3.back * Time.deltaTime * movespeed);
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            Playeranim.SetBool("isrun", true);

        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            Playeranim.SetBool("isrun", false);
        }

        if (Input.GetKey(KeyCode.A) && isdead == false)
        {
            //transform.Translate(Vector3.left * Time.deltaTime * movespeed);
            //transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.Rotate(new Vector3(0, h * rotatespeed, 0));
            Playeranim.SetBool("isrun", true);
        }
        else if (Input.GetKey(KeyCode.D) && isdead == false)
        {
            //transform.Translate(Vector3.right * Time.deltaTime * movespeed);
            //transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.Rotate(new Vector3(0, h * rotatespeed, 0));
            Playeranim.SetBool("isrun", true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Playeranim.SetBool("isrun", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) && isreloading == false )
        {
            Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
            Playeranim.SetTrigger("Fire");
            currentAmmo--;
            audiosource.PlayOneShot(AudioClipsArr[0]);
            audiosource.volume = 0.5f;
        }

        if(Input.GetKeyDown(KeyCode.R) && isreloading == false && isdead == false && currentAmmo <= 9)
        {
            StartCoroutine(Reload());
            return;
        }
    }

    IEnumerator Reload()
    {
        isreloading = true;
        Debug.Log("Reloading . . .");

        yield return new WaitForSeconds(reloadtime);
        audiosource.PlayOneShot(AudioClipsArr[1]);
        currentAmmo = maxAmmo;
        isreloading = false;
    }
   void takeDamage(int damage)
    {
        //Do damage 
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            takeDamage(10);
            Hptext.GetComponent<Text>().text = (maxHealth + "/" + currentHealth);
        }
    }
    void Die()
    {
        if (!isdead)
        {
            Playeranim.SetTrigger("Death");
            Destroy(gameObject, 10);
            isdead = true;
        }
    }
   
}
