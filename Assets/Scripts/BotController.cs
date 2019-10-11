using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour {
    public CircleCollider2D coll;
    public float speed = 5f;

    private float zoneRadius;
    private bool isBallInZone;
    private InZone botZone;

    Vector3 Destination = Vector3.zero;
    // Use this for initialization
    void Start () {
        botZone = coll.GetComponent<InZone>();
        Destination = botZone.GenerateNewDestination();
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Animator>().SetBool("Scored", ScoreManager.scored);

        if (ScoreManager.scored)
            return;
            

        if (botZone.BallinZone)
        {
            MoveToDestination(botZone.ballPos);
        }
        else
        {
            if (transform.position != Destination)
                MoveToDestination(Destination);
            else
                Destination = botZone.GenerateNewDestination();
        }

            
	}
    private void MoveToDestination(Vector3 destination)
    {
        Vector2 dir = destination - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0, 0, angle),0.1f);
        transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime * speed);
    }
  
}
