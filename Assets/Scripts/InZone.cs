using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InZone : MonoBehaviour {

    public bool BallinZone;
    public Vector3 ballPos;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
            BallinZone = true;
           
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
            ballPos = collision.transform.position;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
            BallinZone = false;
    }
    public Vector3 GenerateNewDestination()
    {
        Vector3 newPoint = Vector3.zero;
        float angle = Random.Range(0.0f, 1.0f) * (Mathf.PI * 2);
        float radius = Random.Range(0.0f, 1.0f) * GetComponent<CircleCollider2D>().radius;
        newPoint.x = transform.position.x + radius * Mathf.Cos(angle);
        newPoint.y = transform.position.y + radius * Mathf.Sin(angle);
        return newPoint;
    }
}
