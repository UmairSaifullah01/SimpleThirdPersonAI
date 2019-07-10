using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectSpawn;
    private float timeSpawnTemp = 0;
	public float TimeSpawn = 20;
    public float ObjectCount = 0;
    public int Radiun;

    private void Start()
    {
        if (GetComponent<Renderer>())
            GetComponent<Renderer>().enabled = false;

    }

    private void Update()
    {
		if(!ObjectSpawn)
			return;
		
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

        if (gos.Length < ObjectCount)
        {
            if (Time.time >= timeSpawnTemp + TimeSpawn)
            {
                GameObject enemyCreated = (GameObject)Instantiate(ObjectSpawn,transform.position +new Vector3(Random.Range(-Radiun, Radiun), 20, Random.Range(-Radiun, Radiun)),Quaternion.identity);
                float scale = Random.Range(5, 20);
				enemyCreated.transform.localScale = new Vector3(scale, scale, scale);

                timeSpawnTemp = Time.time;

            }
        }

    }

}
