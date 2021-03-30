using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public bool isFree = true;
    public bool isDead = false;

    public GameObject[] insects;

    public int insectsUnlocked = 4;

    public int lanesUnlocked = 3;

    void Start()
    {
        if (isFree)
        {
            CreateNewEnemy();
            isDead = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Insect")
        {
            isFree = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            isFree = true;
        }
    }

    //Create a new enemy based on the percentage
    public void CreateNewEnemy()
    {
        int insect;
        int percentage;
        percentage = Random.Range(1, insectsUnlocked);

        GameObject childObject = Instantiate(insects[percentage - 1], new Vector3(transform.position.x - 10, transform.position.y, 0), Quaternion.identity);
        childObject.transform.parent = transform;
        childObject.GetComponent<FlyOscillator>().insect = percentage;
        childObject.GetComponent<FlyOscillator>().speed = Random.Range(2.0f, 5.0f);
    }

    void Update()
    {
        if (isDead)
        {
            CreateNewEnemy();
            isDead = false;
        }
    }
}
