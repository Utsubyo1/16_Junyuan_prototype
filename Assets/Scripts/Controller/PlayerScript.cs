using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public static bool Playerdeath;
    public static  PlayerScript  Score;
    
    //movement and rotate speed
    float movespeed = 5f; // movement speed
    float rotatespeed = 3f; // rotatespeed

    //Ammo 
    public int maxAmmo = 7; // maxammo
    private int currentAmmo; // current ammo
    public float reloadtime = 10f; // time for reload
    private bool isreloading = false; 

    //Health
    public int maxHealth = 100; // max health
    int currentHealth; // current health

    //zombie Count
    float Scorecount;

    //audio
    public AudioClip[] AudioClipsArr;
    private AudioSource audiosource;

    //Text
    public Text Hptext;
    public Text Scoretext;
    public Text Ammotext;
   
    //script
    public HealthbarScript healthBar;


    //
    public Rigidbody Playerrb;
    public Animator Playeranim;

    //bullet spawn
    public GameObject bulletPrefab;
    public GameObject bulletSpawn;

    bool isdead = false;

    //Win condition
    float WinScore = 0f;
    bool GotoHeli = false;

    // Start is called before the first frame update
    void Start()
    {
        //check adn set ammo when start
        currentAmmo = maxAmmo;
        Ammotext.GetComponent<Text>().text = (maxAmmo + "/" + currentAmmo);

        audiosource = GetComponent<AudioSource>();
        //check, set and display health when start
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Hptext.GetComponent<Text>().text = (maxHealth + "/" + currentHealth);

        //set Text
        Scoretext.GetComponent<Text>().text = "Zombies Kill: " + Scorecount;

        //
        if(Score == null)
        {
            Score = this;
        }

        //Player death true or false
        Playerdeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Will see if Pause is set
        if (PauseMenuScript.GameisPaused == false)
        {
            if (isdead == false)
            { movement(); }

            if (isreloading)
                return;

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                Ammotext.GetComponent<Text>().text = (maxAmmo + "/" + currentAmmo);
                return;
            }

            //Player death
            if (currentHealth <= 0)
            {
                Playerdeath = true;
                Die();
                isdead = true;

            }
            if(WinScore == 30)
            {
                
                Debug.Log("GET TO THE CHOPPER");
                
                GotoHeli = true;
                
            }
            
        }
    }
    void movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        //W,S will move player foward/backward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * movespeed);
            Playeranim.SetBool("isrun", true);
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * Time.deltaTime * movespeed);
            Playeranim.SetBool("isrun", true);

        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            Playeranim.SetBool("isrun", false);
        }

        // A,D key will rotate Player angles
        if (Input.GetKey(KeyCode.A))
        {
            
            transform.Rotate(new Vector3(0, h * rotatespeed, 0));
            Playeranim.SetBool("isrun", true);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            
            transform.Rotate(new Vector3(0, h * rotatespeed, 0));
            Playeranim.SetBool("isrun", true);
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Playeranim.SetBool("isrun", false);
        }

        //When press increase moveseed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movespeed = 10f;
            
        }
        else
        {
            movespeed = 5f;
            
        }
        //When press and hold will Fire the Gun and spawn bullet
        if (Input.GetKeyDown(KeyCode.Space) && isreloading == false || Input.GetKeyDown(KeyCode.Mouse0) && isreloading == false )
        {
            Instantiate(bulletPrefab, bulletSpawn.transform.position, transform.rotation);
            Playeranim.SetTrigger("Fire");
            Ammotext.GetComponent<Text>().text = (maxAmmo + "/" + currentAmmo);
            currentAmmo--;
            audiosource.PlayOneShot(AudioClipsArr[0]);
            audiosource.volume = 0.5f;
        }
        //Reload
        if(Input.GetKeyDown(KeyCode.R) && isreloading == false && isdead == false && currentAmmo <= 9)
        {
            StartCoroutine(Reload());
            return;
        }

        //Cheat
        if (Input.GetKeyDown(KeyCode.C))
        {
            Scorecount += 10;
            WinScore += 10;
            Scoretext.GetComponent<Text>().text = "Zombies Kill:" + Scorecount;
        }
    }
    //Reload
    IEnumerator Reload()
    {
        isreloading = true;
        Debug.Log("Reloading . . .");

        yield return new WaitForSeconds(reloadtime);
        audiosource.PlayOneShot(AudioClipsArr[1]);
        currentAmmo = maxAmmo;
        Ammotext.GetComponent<Text>().text = (maxAmmo + "/" + currentAmmo);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Helicopter" && GotoHeli == true)
        {
            SceneManager.LoadScene("WinScene");
        }
    }
    
    void Die()
    {
        //Death state
        if (!isdead)
        {
            Playeranim.SetTrigger("Death");
            Destroy(gameObject, 4);
            isdead = true;
            SceneManager.LoadScene("LoseScene");
        }
    }
   
    public void AddScore()
    {
        //Score
        Scorecount += 1;
        WinScore++;
        Scoretext.GetComponent<Text>().text = "Zombies Kill:" + Scorecount;
    }
   
   
}
