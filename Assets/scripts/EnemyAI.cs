using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

// [System.Serializable]
public class EnemyAI : MonoBehaviour
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

    public movementController isPlayerDead;

    


    public int randomPainSound;
    public int randomDeathSound;
    public int randomAttackSound;

    public AudioSource attack1;
    public AudioSource attack2;


    public AudioSource pain1;
    public AudioSource pain2;



    public AudioSource death1;
    public AudioSource death2;
    
    
    public AudioSource[] playerpainsounds;
    // public AudioSource[] playerAttackSounds;
    // public AudioSource[] playerDeathSounds;





    

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
        animator.SetBool("isMoving", true); //plays enemy moving animation
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && enemy.remainingDistance <= enemy.stoppingDistance)
        {
            //enemy is close enough to attack
            animator.SetBool("isMoving", false); //stops moving animation
            animator.SetBool("isAttacking", true); //starts attacking animation

            if(!alreadyAttacked) //stops constant damage from occuring
            {
                StartCoroutine(damageAfterAnimation()); //deals damage after attack animation plays
                 alreadyAttacked = true;
            }

            
        }
        else //player is not within attack range
        {
            
            animator.SetBool("isMoving", true); //starts moving animation
            animator.SetBool("isAttacking", false); //stops attack animation
            alreadyAttacked = false;
        }


    }


    IEnumerator damageAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); //wait for animation to end
         pl.Playerhealth = pl.Playerhealth - 10; //deal damage
        alreadyAttacked = false;

    }

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>(); //enemy collider - hit box
        randomnum();

        enemy.speed = randomSpeed; //assign random speed to enemy 


    }

    // Update is called once per frame
    void Update()
    {

        find(); //path find to player

        if (enemy.remainingDistance <= enemy.stoppingDistance)
        {
            animator.SetBool("isMoving", false); //stops moving animation when next to player
        }

        playPlayerDeathSound();
        
    }

     public void TakeDamage(float damageAmount) //enemy takes damage from gun
    {
        painsound();
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
        if(randomNumber <= 40)
        {
            Rigidbody rb = Instantiate(ammoDrop, ammospawnpoint.position, ammospawnpoint.rotation).GetComponent<Rigidbody>();
        }
        
        animator.SetBool("isAlive", false); //plays death animation
        deathSound();
        StartCoroutine(DestroyAfterAnimation()); //removes gameobject after animation
        OnDeath?.Invoke(); //tells spawn controller enemy is dead
        // Destroy(attack1);
        // Destroy(attack2);

    }


    IEnumerator DestroyAfterAnimation()
{
    yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length); //wait for current animation
    Destroy(gameObject); //remove game object from game
}

    void randomnum() 
    {
    
    //generates random number to decide speed and if enemy will drop item
    randomNumber = UnityEngine.Random.Range(0, 100);
    randomSpeed = UnityEngine.Random.Range(3, 7);

    //generates random numbers to decide which sounds to player
    randomPainSound = UnityEngine.Random.Range(1, 3);
    randomDeathSound = UnityEngine.Random.Range(1, 3);
    randomAttackSound = UnityEngine.Random.Range(1, 3);


    Debug.Log(randomNumber);

    }


    void painsound()  //decides which enemy pain sound to player based on which number was generated (1 or 2)
    {

        if(randomPainSound == 1)
        {
            pain1.Play();
        }

        else
        {
            pain2.Play();

        }

    }

    void deathSound() //decides which enemy death sound to player based on which number was generated (1 or 2)
    {
        if(randomDeathSound ==1)
        {
            death1.Play();
        }
        else
        {
            death2.Play();
        }


    }

    void attackSound() //decides which attack sound to player based on which number was generated (1 or 2)
    {                               //method is called in inspector during a specific animation time
        if(randomAttackSound ==1)
        {
            attack1.Play();
        }
        else
        {
            attack2.Play();
        }


    }

    void playPlayerDeathSound() //removes all enemies from map once player is dead. need so that the audio doesnt constantly play after death
    {
        if(isPlayerDead.isdead == true)
        {
            isPlayerDead.randomdeathSound();
            Destroy(gameObject); 
        }

    }





    public void randomPlayerPainSound() //method is called in inspector during a specific animation time
    {
        int index = UnityEngine.Random.Range(0, playerpainsounds.Length); //picks random sound in playerpainsounds array
        playerpainsounds[index].Play(); //plays random pain sound when player is attacked. 

    }



}
