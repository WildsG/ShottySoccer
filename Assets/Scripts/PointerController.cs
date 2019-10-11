using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerController : MonoBehaviour {
    public Transform[] pointerPos;
    private int index = 1;
    public Rigidbody2D ballrb;
    public bool didShoot = false;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1f;
        didShoot = false;
	}

    // Update is called once per frame
    void Update() {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) && index > 0)
            index--;
        else if ((Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) && index < 2)
            index++;

        for (int i = 0; i < pointerPos.Length; i++)
        {
            if (i != index)
                pointerPos[i].GetComponent<SpriteRenderer>().enabled = false;
            else
                pointerPos[i].GetComponent<SpriteRenderer>().enabled = true;
        }

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !didShoot)
        {
            ballrb.AddForce(pointerPos[index].transform.right * 1000f);
            didShoot = true;
        }
	}
    public void resetBall()
    {
        ballrb.transform.position = new Vector3(-0.27f, -4.35f, 0);
        ballrb.velocity = Vector3.zero;
        ballrb.angularVelocity = 0f;
        didShoot = false;
    }

}
