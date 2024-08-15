using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class bossEnemyAI : MonoBehaviour
{
    public Transform player;
    public UnityEngine.AI.NavMeshAgent enemy;
    public LayerMask ground, target;
    public Animator animator; 
    public int speed;
    public float attackRange = 2.0f;
    public float health = 1f;
    public bool isAlive = true;
    public event Action OnDeath;
    public movementController pl;
    private bool alreadyAttacked = false;
    public BoxCollider boxCollider;
    public bool isFinding;
    public Slider healthbar;
    public Canvas enemyhealthbar;
    public bool playerInArea = false;
    public Vector3 startPosition;
    public bool Alive;
    public float attacktime;
    public GameObject projectileAttack;
    public Transform projectileSpawnPoint;
    public float throwRange;
    public float distanceToPlayer;

 

    void awake()
    {
        player = GameObject.Find("Player").transform;
        enemy = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>(); // Get components from engine
    }


    public void find()
    {
        enemy.SetDestination(player.position); //set destination to player
        animator.SetBool("isRunning", true); // Set isRunning to true in animator

        //check distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position); 

        if (distanceToPlayer <= attackRange && enemy.remainingDistance <= enemy.stoppingDistance)
        {
            //enemy is close enough to attack
            animator.SetBool("isRunning", false); //stop running
            animator.SetBool("isAttacking", true); //start attacking
    
        }
        else
        {
            //player is not within attack range
            
            animator.SetBool("isRunning", true); //start running
            animator.SetBool("isAttacking", false); //stop attacking
            alreadyAttacked = false;
    
        }



    }


    public void damageAfterAnimation() //called in inspector
    {
        pl.Playerhealth = pl.Playerhealth - 10;
    }


    public void disablePathfinding() //disables tracking during kick animatation
    {                                   //called in inspector at correct time
        enemy.enabled = false; 
    }

    public void ActivatePathfinding() //enables tracking after kick animatation
    {                                   //called in inspector at correct time
        enemy.enabled = true; 
    }

   



    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        startPosition = transform.position;
        Alive = true;

        
    }

    // Update is called once per frame
    void Update()
    {

        healthbar.value = health; //update health bar

        if(Alive == false)
        {
            animator.SetBool("isAlive", false); //play death when dead

        }


    }

    public void idle()
    {
        //
    }



     public void TakeDamage(float damageAmount) //hit by bullet
    {
        health -= damageAmount; //decrease health
        if (health <= 0) //when dead
        {
            boxCollider.enabled = false; //disables hit box
            enemy.enabled = false; //disables enemy tracking when dead
            enemyhealthbar.enabled = false; //removes health bar

            death(); //run death method
        }
        
    }


       public void death()
    {

        animator.SetBool("isAlive", false); //play death animation
        Alive = false; //track if dead
    }


        //starting positon behaviour 
        public void OnTriggerEnter(Collider other)
        {
        if (other.CompareTag("startpos")) // if entered starting position
        {
            animator.SetBool("isRunning", false); //stop running animation
        }


        }

        public void OnTriggerExit(Collider other) //if left starting area
        {
        if (other.CompareTag("startpos"))
        {
           
             animator.SetBool("isRunning", true); //player running animation
        }
        }


  


    public void rangedAttack()
    {
      
        if(!alreadyAttacked) //if attacking
        {
            animator.SetTrigger("throwin"); //play throw animation

            alreadyAttacked = true;
            Invoke(nameof(reset), attacktime);
        }       
    }

      public void projectile() //range attack object
    {
        //spawn projectile game object at specific position and rotation
        Rigidbody rb = Instantiate(projectileAttack, projectileSpawnPoint.position, projectileSpawnPoint.rotation).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 200f, ForceMode.Impulse); //add forward force to projectile
        rb.AddForce(transform.up * -5f, ForceMode.Impulse); //add upward force to projectile

    }

    public void reset()
    {
        alreadyAttacked = false;
    }

}
