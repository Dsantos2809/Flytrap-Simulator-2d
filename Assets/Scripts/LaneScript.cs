using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneScript : MonoBehaviour
{
    public GameObject[] insects;

    private float nextActionTime = 0.0f;
    public float period = 2.0f;

    public int insectsUnlocked = 4;
    public int quantityUnlocked = 4;
    public static int quantitySpawned = 0;


    //Create a new enemy based on the percentage
    private void Start()
    {
        quantitySpawned = 0;
    }
    public void CreateNewEnemy()
    {
        int percentage;
        percentage = Random.Range(1, insectsUnlocked + 1);
        quantitySpawned++;

        
        GameObject childObject = Instantiate(insects[percentage - 1], new Vector3(transform.position.x - 10, Random.Range(-1, 5), 0), Quaternion.identity);
        childObject.transform.parent = transform;
        childObject.GetComponent<FlyOscillator>().insect = percentage;
        childObject.GetComponent<FlyOscillator>().speed = Random.Range(2.0f, 5.0f);
    }

    void Update()
    {
        if (quantityUnlocked > quantitySpawned)
        {   
            if (Time.time > nextActionTime)
            {
                nextActionTime = Time.time + period;
                CreateNewEnemy();
            }
        }

    }
}
