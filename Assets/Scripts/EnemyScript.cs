using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    //target Player
    GameObject target;
    

    //healthbar and text
    public Text Hptext;
    public HealthbarScript healthBar;

    int maxHealth = 100; // Max health
    int currentHealth; // current health

    public float deathtime = 10f; // deathTime

    
    float speed = 1f; // move speed
    Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        Hptext.GetComponent<Text>().text = (maxHealth + "/" + currentHealth);

        target = GameObject.Find("Player"); //find Player name 
        
        
    }

     void Update()
    {
        if(currentHealth == 0)
        {
            Destroy(gameObject);
        }
        
       
    }
    // Update is called once per frame
    void FixedUpdate()
    {
         //Enmey move towards the player
          Vector3 pos = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
          rb.MovePosition(pos);
          transform.LookAt(target.transform.position);
        
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

    
}
