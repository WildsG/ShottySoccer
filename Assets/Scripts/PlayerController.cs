using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rb2d;

    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    private Animator anim;
    private Gun playerGun;
    private ShellSpawner spawner;
	// Use this for initialization
	void Start () {
        spawner = GameObject.Find("PowerUpSpawner").GetComponent<ShellSpawner>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerGun = transform.GetChild(0).GetComponent<Gun>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", rb2d.velocity.magnitude);
        if (ScoreManager.scored)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0;
            return;
        }
        Movement();
        
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ammo"))
        {
            playerGun.currentAmmo = playerGun.maxAmmo;
            spawner.shellList.Remove(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void Movement()
    {
        float vertical = Input.GetAxis("Vertical");
        Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rb2d.velocity = transform.right * vertical * moveSpeed;
    }

}
