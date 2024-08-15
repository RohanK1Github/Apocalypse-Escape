using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class movementController : MonoBehaviour //player controller
{

    public CharacterController controller; //reference to character controller

    //rotation
    public float mouseSensitivity = 500f;
    public Transform playerBody;
    float yRotate = 0f;
    float xRotate = 0f;
    // public mouseSensitivity sen;

    // public Slider SensSlider;


    //movemnet
    public float speed;
    public float gravity = -9.81f;
    public float jump = 2f;
    public Transform groundCheck;
    public float ground = 0.4f; //ground distance
    public LayerMask mask; //ground mask
    Vector3 velocity;
    bool onGround;
    bool isMoving;
    Vector3 position = new Vector3(0f,0f,0f);
    public float crouchSpeed = 5f;
    public float standHeight = 2f;
    public float crouchHeight = 1f;
    private bool isCrouching = false;
    private Rigidbody rb;
     public float groundDistance = 0.4f; //ground distance
     private bool isGrounded;
    public LayerMask groundMask; //ground mask
    public float jumpForce = 5f;
    public float pushPower = 2.0f;
    public Transform player;
    public Animator animator; //reference to the Animator component

    //health and stamina
    public TextMeshProUGUI healthDisplay;
    public Image StaminaBar;
    public float staminaVal, maxStamina, Rcost, regenRate; //chargerate
    public Coroutine Regen; //recharge
    public Image healthBar;      
    public float Playerhealth;
    public float maxHealth;
    public float Dcost, healthregenRate; //chargerate
    public Coroutine healthRegenC; //recharge
    private float lastDamageTime;
    private float healthRegenDelay = 5f;


    //death objects
    public GameObject DeathCam;
    public GameObject DeathCanvas;
    public GameObject gameCanvas;
    public bool isSprinting = false;
    public bool isdead = false;

    public int randomDeathNoise;

    public AudioSource sprintingSound;
    public AudioSource death1;
    public AudioSource death2;
    public AudioSource death3;

    public EnemyAI e;


    public void playerdeath() //if health is 0 destroy player game object
    {
        if (Playerhealth <= 0){
            isdead = true;
            // randomdeathSound();
            Destroy(gameObject);
            DeathCam.SetActive(true);
            DeathCanvas.SetActive(true);
            gameCanvas.SetActive(false);
            Cursor.lockState = CursorLockMode.None; //adds curser back
        }
    }

    void Start() //runs when game starts
    {
        Cursor.lockState = CursorLockMode.Locked; //removes curser
        controller = GetComponent<CharacterController>(); //get character controller from player
        animator = GetComponent<Animator>();
        DeathCam.SetActive(false);
        randomnum();

    }



    void Update() //runs every frame
    {

        movement();
        rotation();
        crouch();
        healthDisplay.text = "health: " + Playerhealth; //updates health text
        playerdeath();

        

        healthBar.fillAmount = Playerhealth / maxHealth; //update health bar

        if (healthRegenC == null && Playerhealth < maxHealth)
        {
            healthRegenC = StartCoroutine(regenHealth()); //runs health regen after time
        }



    }




    void movement()
    {

        onGround = Physics.CheckSphere(groundCheck.position, ground, mask);

        if(onGround && velocity.y < 0)
        {
            velocity.y= -2f;
        }

        float x = Input.GetAxis("Horizontal"); 
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);


        if (Input.GetButtonDown("Jump") && onGround)
        {
            velocity.y = Mathf.Sqrt(jump * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (position != gameObject.transform.position && onGround == true)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        position = gameObject.transform.position;

        stamina(); //calls stamina

    }




    void stamina() 
    {

        if (Input.GetKey(KeyCode.LeftShift) && staminaVal > 0)  //if button and stamina is pressed and more than 0 - sprint

        {   
            speed = 25; //increase speed
            // sprintingSound.Play();
            isSprinting = true;
            // while(isSprinting == true)
            // {
            //     sprintingSound.Play();
            // }


            staminaVal -= Rcost * Time.deltaTime; //reduce stamina
            if (staminaVal < 0)
            {
                staminaVal = 0; //doesnt let stamina val go less than 0. stops sprinting when stamina is 0
                sprintingSound.Stop();

            }
            StaminaBar.fillAmount = staminaVal/maxStamina; //updates stamina bar on screen
            if(Regen != null)
            {
                StopCoroutine(Regen); //stop regen
                
            }
            Regen = StartCoroutine(regenStamina()); //call regenstamina after time

            


        }
        else
        {
            speed = 17; //set back to normal speed when condtion is not met
            isSprinting = false;
        }


        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("1");
            sprintingSound.Play();
        }

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprintingSound.Stop();
        }
  
       

      



        
    }

    private IEnumerator regenStamina() //regen stamina
    {
        yield return new WaitForSeconds(5f); //regen stamina after 5 seconds 
        
        while(staminaVal < maxStamina) //if stamina is less than max stamina it should regen
        {
            staminaVal += regenRate / 10f;
            if (staminaVal > maxStamina)
            {
                staminaVal = maxStamina;
            }
            yield return new WaitForSeconds(0.1f); 
            StaminaBar.fillAmount = staminaVal/maxStamina; //updates stamina bar on screen
        }
    }


       void healthregen()
    {
   

        
    }







public void TakeDamage(float damageAmount)
{
    Playerhealth -= damageAmount; //decrese health
    healthBar.fillAmount = Playerhealth / maxHealth; //update health bar
    
}

private IEnumerator regenHealth() //similar to stamina regen
{
      yield return new WaitForSeconds(10f); //wait time
    
    while (Playerhealth < maxHealth) //if health is less than max health start health regen
    {
        Playerhealth += healthregenRate * Time.deltaTime;
        healthBar.fillAmount = Playerhealth / maxHealth; //update health bar

      
        if (Playerhealth >= maxHealth)   //check if health has reached max health
        {
            Playerhealth = maxHealth;
            break; //stop the coroutine if health is full
        }

        yield return new WaitForSeconds(0.1f);
    }

    healthRegenC = null;

}






    void rotation() //mouse rotation
    {

    float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //move mouse x rotation
    float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //move mouse y rotation

    
    yRotate += mouseX; //rotate the player model left and right
    transform.Rotate(Vector3.up * yRotate); 

    
    xRotate -= mouseY; //rotate the camera up and down
    xRotate = Mathf.Clamp(xRotate, -90f, 90f); //stops player from rotating 360 degrees. 
    playerBody.localRotation = Quaternion.Euler(xRotate, 0f, 0f);
    yRotate = 0;

    }




    void crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl)) //crouch key 
        {
            isCrouching = true;
            controller.height = crouchHeight; //sets new height
            speed = 2; //reduce speed
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) //when let go uncrouch
        {
            isCrouching = false;
            controller.height = standHeight;
            speed = 17; //reset speed
        }

    }







    void randomnum()
    {
    randomDeathNoise = UnityEngine.Random.Range(1, 4); //pick death sound to play
    }



    public void randomdeathSound() //play death sound
    {

        if(randomDeathNoise == 1)
        {
            death1.Play();
        }
        else if(randomDeathNoise == 2)
        {
            death2.Play();
        }
        else
        {
            death3.Play();
        }


    }


    void OnCollisionEnter(Collision collison)  //checks if hit by range attack by enemy
    {
        if(collison.gameObject.tag == "sphere") //if hit
        {
            Playerhealth -= 5; //take damage
        }
    }

  







}
