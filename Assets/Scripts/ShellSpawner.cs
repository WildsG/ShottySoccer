using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour {
    public GameObject GunShells;
    public List<GameObject> shellList;
    private float timeToSpawn = 10f;
	// Use this for initialization
	void Start () {
        shellList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        timeToSpawn -= Time.deltaTime;
		if(shellList.Count < 3 && timeToSpawn <=0)
        {
            timeToSpawn = 10f;
            shellList.Add(Instantiate(GunShells, new Vector3(Random.Range(-8,8), Random.Range(-4.5f, 4.5f), 0),Quaternion.identity));
        }
        else if(shellList.Count >= 3 && timeToSpawn <= 0)
        {
            Destroy(shellList[0]);
            shellList.RemoveAt(0);
        }

	}
}
