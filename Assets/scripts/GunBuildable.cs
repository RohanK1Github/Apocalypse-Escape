using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunBuildable : MonoBehaviour
{

    public Camera fpsCam;
    public int totalAmmo;
    public int reserveAmmoAR;
    public int clipSizeAR = 30;
    public float damage = 25f; 
    public float range = 100f;
    public ParticleSystem flash;
    public AudioSource gunshotAudio;
    public TextMeshProUGUI clip;
    public TextMeshProUGUI reserve;
    public TextMeshProUGUI slash;

    public AudioSource emptyGun;
    public AudioSource reloadSound;
    public GameObject impact;
    public GameObject enemyImpactEffect;
    public Transform gunTransform; 
    private Vector3 originalGunPosition;
    public float bobbingAmount = 0.05f;
    public float bobbingSpeed = 10f;
    private float bobbingTimer = 0f;
    public float smoothReturnSpeed = 2f; //return speed
    private bool isPlayerMoving = false;
    public float reloadDipAmount = 0.2f; //dip ammount when moving
    public float reloadDipDuration = 0.5f; //duration
    private Vector3 originalPosition;
    public TextMeshProUGUI pointText;
    public int fireRate = 5;
    public float nextfire = 0f;
    public points pc; //reference to point script


    public Image GunBar;
    public float gunVal, maxGun, Rcost, regenRate; //chargerate
    public Coroutine Regen; //recharge

    public GameObject scopeImage;
    public GameObject scope;
    public GameObject crosshair;


    // Start is called before the first frame update
    void Start()
    {

    originalGunPosition = gunTransform.localPosition;
    originalPosition = transform.localPosition;
    clip.text = ""; //update ammo text
    reserve.text = "";
    slash.text = "";
    scopeImage.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

         if (Input.GetMouseButton(1))
         {
            scope.gameObject.SetActive(true);
            scopeImage.SetActive(true);
            crosshair.SetActive(false);
         }
         else
         {
            scope.gameObject.SetActive(false);
            scopeImage.SetActive(false);
            crosshair.SetActive(true);
         }


        if (isReloading)
            return;

        if (Input.GetButton("Fire1") && Time.time >= nextfire) //if mouse 1 is pressed fire

        {   nextfire = Time.time + 1f/fireRate; //stops spam fire
             shoot(); //fires
        }

 

        clip.text = ""; //update ammo text
        reserve.text = "";
        slash.text = "";

        //if player is moving move gun 
         isPlayerMoving = Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

        if (isPlayerMoving)
        {
            BobbingMovement();
        }
        else
        {
            SmoothReturn();
        }

        

    }


      void BobbingMovement() //bounce gun up and down
    {
        bobbingTimer += Time.deltaTime * bobbingSpeed;
        float bobbingOffset = Mathf.Sin(bobbingTimer) * bobbingAmount;
        transform.localPosition = originalGunPosition + new Vector3(0f, bobbingOffset, 0f);
    }

    void SmoothReturn() //return gun to orginal positon
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalGunPosition, smoothReturnSpeed * Time.deltaTime);
        bobbingTimer = 0;
    }


    void rechargegun()
    {

      
            if(gunVal <= maxGun)
            {
            if(Regen != null)
            {
                StopCoroutine(Regen); // Stop any ongoing regen
            }
            Regen = StartCoroutine(regenGun()); // Start regenerating gun stamina
            }
            GunBar.fillAmount = gunVal/maxGun; // Update the UI stamina bar
       

    }



    public void shoot()
    {

           if(gunVal <= maxGun)
            {
            if(Regen != null)
            {
                StopCoroutine(Regen); // Stop any ongoing regen
            }
            Regen = StartCoroutine(regenGun()); // Start regenerating gun stamina
            }
            GunBar.fillAmount = gunVal/maxGun; // Update the UI stamina bar



        if (clipSizeAR > 0 && gunVal > 0) //if has ammo in clip
        {
             gunVal -= 1;
         



            flash.Play(); //gun flash viual is played
            gunshotAudio.Play(); //shot audio is played
            RaycastHit hit; //raycast (bullet) is shot
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {

            //enemy ai component references
            EnemyAI enemyAI = hit.transform.GetComponent<EnemyAI>();
            bossEnemyAI BossEnemyAI = hit.transform.GetComponent<bossEnemyAI>();
            EnemyAI2 enemyAI2 = hit.transform.GetComponent<EnemyAI2>();

            //checks if enemies are hit and deals damage and instanstiates hit effect objects
            if (enemyAI != null)
            {
                Instantiate(enemyImpactEffect, hit.point, Quaternion.LookRotation(hit.normal)); //enemy is hit and instantiates hit effect
                enemyAI.TakeDamage(damage); //deals damage
                pc.pointCounter += 30; //add points to point counter from point script
                pc.totalPoints += 30;
            }

                 else if (BossEnemyAI != null) //same as above but for different enemy type
            {
                Instantiate(enemyImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                BossEnemyAI.TakeDamage(damage);
                pc.pointCounter += 30;
                pc.totalPoints += 30;


            }

              else if (enemyAI2 != null) //same as above but for different enemy type
            {
                Instantiate(enemyImpactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                enemyAI2.TakeDamage(damage);
                pc.pointCounter += 30;
                pc.totalPoints += 30;

            }




            else //not an enemy
            {
                Instantiate(impact, hit.point, Quaternion.LookRotation(hit.normal)); //different hit effect
            }
            }

        }

        
    }

    private IEnumerator regenGun() //regen stamina
    {
        yield return new WaitForSeconds(5f); //regen stamina after 5 seconds 
        
        while(gunVal < maxGun) //if stamina is less than max stamina it should regen
        {
            gunVal += regenRate / 10f;
            if (gunVal > maxGun)
            {
                gunVal = maxGun;
            }
            yield return new WaitForSeconds(0.1f); 
            GunBar.fillAmount = gunVal/maxGun; //updates stamina bar on screen
        }
    }


    public bool isReloading = false;  //not reloading


}
