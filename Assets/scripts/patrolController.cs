using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrolController : MonoBehaviour
{

    public bool playerInArea = false;
    public bossEnemyAI be; //"be = boss enemy"
    public Animator animator;
    public bool isRangedAttackRoutineRunning = false;


    // Start is called before the first frame update
    void Start()
    {
        be.startPosition = transform.position; //saves starting location of boss enemy
    }

    // Update is called once per frame
    void Update()
    {

        if(playerInArea == true) //if player is in area
        {
            be.find(); //call find method from boss enemy script

            if(!isRangedAttackRoutineRunning)
            {
                StartCoroutine(RangedAttackRoutine()); //start range attack
                isRangedAttackRoutineRunning = true;
            }
        }
        else
        {
            if(isRangedAttackRoutineRunning)
            {
                StopAllCoroutines(); //stops range attack
                isRangedAttackRoutineRunning = false;
            }
            be.idle();
   
        }

    }


    IEnumerator RangedAttackRoutine()
    {
    
    while (playerInArea) //if player in area
    {
        be.rangedAttack(); //range attack from boss script
        yield return new WaitForSeconds(5f); // wait for 5 seconds before the next attack
    }
    isRangedAttackRoutineRunning = false; 
    
    }


      public void OnTriggerEnter(Collider other) //tracks if player enters patrol area
    {
        if (other.CompareTag("Player")) 
        {
            playerInArea = true;
        }

    }

    public void OnTriggerExit(Collider other) //tracks if player exits patrol area
    {
        if (other.CompareTag("Player"))
        {
            playerInArea = false;
            be.enemy.SetDestination(be.startPosition);
            
        }
      
    }

}
