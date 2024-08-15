using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyAI2 : MonoBehaviour
{
    public Transform player;
    public UnityEngine.AI.NavMeshAgent enemy;
    public LayerMask ground, target;
    public Animator animator; //reference to enemy animator
    public int speed;
    public float attackRange = 2.0f;
    public float health = 1f;
    public bool isAlive = true;
    public event Action OnDeath;
    public movementController pl;
    private bool alreadyAttacked = false;
    public BoxCollider boxCollider;
    public GameObject ammoDrop;
    public int randomNumber;
    public Transform ammospawnpoint;
    public int randomSpeed;



    public int randomPainSound;
    public int randomDeathSound;
    public AudioSource attack1;
    public AudioSource attack2;
    public AudioSource pain1;
    public AudioSource pain2;
    public AudioSource death1;
    public AudioSource death2;
    public AudioSource[] playerpainsounds;
    public AudioSource[] enemyAttackSounds;
  

    

    void awake()
    {
        player = GameObject.Find("Player").transform; //finds play object
        enemy = GetComponent<UnityEngine.AI.NavMeshAgent>(); //gets tracking component

        enemy = GetComponent<UnityEngine.AI.NavMeshAgent>(); 
        animator = GetComponent<Animator>(); //get the animator component

    }


    void find()
    {
        enemy.SetDestination(player.position); //move towards player
        animator.SetBool("isrun", true); //plays enemy moving animation
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && enemy.remainingDistance <= enemy.stoppingDistance)
        {
            //enemy is close enough to attack
            animator.SetBool("isrun", false); //stops moving animation
            animator.SetBool("iskick", true); //starts attacking animation
            
        }
        else //player is not within attack range
        {
            
            animator.SetBool("isrun", true); //starts moving animation
            animator.SetBool("iskick", false); //stops attack animation
            alreadyAttacked = false;
        }


    }

    public void disablePathfinding() //disables tracking during kick animatation
    {                                   //called in inspector at correct time
        enemy.enabled = false; 
    }

    public void ActivatePathfinding() //enables tracking after kick animatation
    {                                   //called in inspector at correct time
        enemy.enabled = true; 
    }


    public void dealDamage(int Amount) //called during attack animation. Called in inspector
    {
        pl.Playerhealth = pl.Playerhealth - Amount;
    }


    void Start()
    {
        boxCollider = GetComponent<BoxCollider>(); //enemy collider - hit box
        randomnum(); // generates the random numbers neededed for speed and drops

        enemy.speed = randomSpeed; //assign random speed to enemy 


    }

    // Update is called once per frame
    void Update()
    {

        find(); //path find to player

        if (enemy.remainingDistance <= enemy.stoppingDistance)
        {
            animator.SetBool("isrun", false); //stops moving animation when next to player
        }
        
    }

     public void TakeDamage(float damageAmount) //enemy takes damage from gun
    {

        health -= damageAmount; //reduce enemy health
        Debug.Log("Damage taken: " + damageAmount + ", Current Health: " + health);
        if (health <= 0)
        {
            boxCollider.enabled = false; //disables hit box
            enemy.enabled = false; //disables pathfinding

            death(); //destroys enemy object
        }
    }


       public void death()
    {
        animator.SetBool("isalive", false); //plays death animation
        OnDeath?.Invoke(); //tells wave script enemy is dead
    }


    public void DestroyAfterAnimation() //called in inspector
    {
        Destroy(gameObject); //removes enemy after they are dead
    }


    void randomnum() //generates random numbers
    {
    randomNumber = UnityEngine.Random.Range(0, 100); //random drop chance
    randomSpeed = UnityEngine.Random.Range(1, 4); //random speed
    }






       public void randomPlayerPainSound() //method is called in inspector during a specific animation time
    {
        int index = UnityEngine.Random.Range(0, playerpainsounds.Length); //picks random sound in playerpainsounds array
        playerpainsounds[index].Play(); //plays random pain sound when player is attacked. 
        
    }

        public void randomAttackSound() //method is called in inspector during a specific animation time
    {
        int index = UnityEngine.Random.Range(0, enemyAttackSounds.Length); //picks random sound in playerpainsounds array
        enemyAttackSounds[index].Play(); //plays random pain sound when player is attacked. 
    }



}
