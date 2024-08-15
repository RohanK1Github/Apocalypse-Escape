using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunAR : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {

    originalGunPosition = gunTransform.localPosition;
    originalPosition = transform.localPosition;
    clip.text = clipSizeAR.ToString(); //update ammo text
    reserve.text = reserveAmmoAR.ToString();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isReloading)
            return;

        if (Input.GetButton("Fire1") && Time.time >= nextfire) //if mouse 1 is pressed fire

        {   nextfire = Time.time + 1f/fireRate; //stops spam fire
             shoot(); //fires
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading) //reload when r is pressed and is not already reloading
        {
            StartCoroutine(reload()); //reload 
        }

        clip.text = clipSizeAR.ToString(); //update ammo text
        reserve.text = reserveAmmoAR.ToString();

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

    void shoot()
    {

        if (clipSizeAR > 0) //if has ammo in clip
        {

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
               clipSizeAR -= 1; //reduce ammo

        }

             if (clipSizeAR == 0) //if no ammo
        {           
            emptyGun.Play();    //play no ammo sound
        }
        
    }

    public bool isReloading = false;  //not reloading


    IEnumerator reload() //reload method
    {

        isReloading = true; //is reloading
        Vector3 dipPosition = originalPosition - new Vector3(0, reloadDipAmount, 0); //dip gun down so it looks like player is reloading
        float timer = 0;
        while (timer < reloadDipDuration)
        {
            transform.localPosition = Vector3.Lerp(originalPosition, dipPosition, timer / reloadDipDuration);
            timer += Time.deltaTime;
            yield return null;
        }


            if (reserveAmmoAR != 0) //if reloading play reload sound
            {
                reloadSound.Play();

            }

        yield return new WaitForSeconds(2); //wait for time

        int ammoNeeded = 30 - clipSizeAR;  // Calculate ammo needed to fill the clip
        if (reserveAmmoAR >= ammoNeeded) //enough reserve ammo
        {
            reserveAmmoAR -= ammoNeeded;
            clipSizeAR += ammoNeeded;
        }
        else //not enough ammo use all remaining reserve ammo
        {
            clipSizeAR += reserveAmmoAR;
            reserveAmmoAR = 0;
        }


        timer = 0;
        while (timer < reloadDipDuration) //return gun after reload
        {
            transform.localPosition = Vector3.Lerp(dipPosition, originalPosition, timer / reloadDipDuration);
            timer += Time.deltaTime;
            yield return null;
        }

    
        isReloading = false;
        Debug.Log("Reloaded. Clip: " + clipSizeAR + ", Reserve: " + reserveAmmoAR);


    }


}
