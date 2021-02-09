using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    public static bool nodmg = false;
    //target Player
    GameObject target;
    //find Hpbar canvas
    public GameObject hpbar;
    //healthbar and text
    public Text Hptext;
    public HealthbarScript healthBar;

    int maxHealth = 50; // Max health
    int currentHealth; // current health

    public float deathtime = 10f; // deathTime

    
    float speed = 5f; // move speed
    Rigidbody rb;
    Animator enemyanim;

    bool isdead = false;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        enemyanim = this.GetComponent<Animator>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Hptext.GetComponent<Text>().text = (maxHealth + "/" + currentHealth);

        target = GameObject.Find("Player"); //find Player name 

        PlayerScript.Playerdeath = false;

    }

     void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
            
            isdead = true;

        }
       

        if (PlayerScript.Playerdeath == true)
        {
            Destroy(this);
        }
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerScript.Playerdeath == false && isdead == false && PauseMenuScript.GameisPaused == false)
        {
            //Enmey move towards the player
            Vector3 pos = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
            rb.MovePosition(pos);
            transform.LookAt(target.transform.position);
        }
    }

    void takeDamage(int damage)
    {
        //Do damage 
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            takeDamage(10);
            Destroy(collision.gameObject);
            Hptext.GetComponent<Text>().text = (maxHealth + "/" + currentHealth);
            //Debug.Log(maxHealth + "/" + currentHealth);
        }
    }

    void Die()
    {
        if (!isdead)
        {
            PlayerScript.Score.AddScore();

            //when enemy lose all Hp destroy Hp text first then destory gameobject
            enemyanim.SetTrigger("Death");
            Destroy(hpbar);
            nodmg = true;
            Destroy(gameObject, 2);
            isdead = true;
            nodmg = false;
        }
    }
}
