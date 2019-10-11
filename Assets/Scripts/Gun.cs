using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    public bool isBot = true;
    public int currentAmmo = 0;
    public int maxAmmo = 20;
    public GameObject particles;
    public TMP_Text ammoText;
    bool inZone = false;

    private float timeToWait = 0.4f;
    private float timeAfterShot = 0f;
    private AudioSource shootsound;
    // Use this for initialization
    void Start()
    {   
        shootsound = transform.parent.GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Volume"))
            shootsound.volume = PlayerPrefs.GetFloat("Volume");
        if (!isBot && ammoText == null)
            Debug.LogError("No text object attach to the gun");
        currentAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.scored)
            return;

        if (!isBot)
        {
            ammoText.text = "Bullets " + currentAmmo.ToString() + "/" + maxAmmo.ToString();  
            if(currentAmmo > 0)
            {
                if (ammoText.transform.GetComponent<Animator>().enabled == true)
                {
                    ammoText.transform.GetComponent<Animator>().enabled = false;
                    ammoText.color = Color.white;
                }

            }
            else if (currentAmmo <= 0)
            {
                ammoText.color = Color.red;
                ammoText.transform.GetComponent<Animator>().enabled = true;
            }
            if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
            {   
                Shoot();
            }
            
        }
        else
        {
            if (timeAfterShot >= timeToWait && inZone)
            {
                Shoot();
                timeAfterShot = 0f;
            }
            else
                timeAfterShot += Time.deltaTime;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
            inZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (isBot)
            if (collision.gameObject.CompareTag("Ball"))
                inZone = false;
    }

    private void Shoot()
    {
        shootsound.Play();
        currentAmmo--;
        GameObject particl = Instantiate(particles, transform.position, Quaternion.Euler(new Vector3(180 + transform.parent.eulerAngles.z, -90, 0)));
        Destroy(particl, 1f);
    }
}
