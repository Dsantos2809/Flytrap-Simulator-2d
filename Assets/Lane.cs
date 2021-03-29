using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public bool isFree = true;

    public GameObject[] insects;

    void Start()
    {
        if (isFree)
        {
            CreateNewEnemy();
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

    public void CreateNewEnemy()
    {
        int insect;
        int percentage;
        percentage = Random.Range(0, 100);
        Debug.Log(percentage);

        if(percentage <= 50)
        {
            insect = 0;
            Debug.Log("fly");
        }
        else if(percentage <= 65)
        {
            insect = 1;
            Debug.Log("bee");
        }
        else if(percentage <= 90)
        {
            insect = 2;
            Debug.Log("mosquitoe");
        }
        else
        {
            insect = 3;
            Debug.Log("beetle");
        }

        GameObject childObject = Instantiate(insects[insect], new Vector3(transform.position.x - 10, transform.position.y, 0), Quaternion.identity);
        childObject.transform.parent = transform;
        childObject.GetComponent<FlyOscillator>().insect = insect + 1;
        childObject.GetComponent<FlyOscillator>().speed = Random.Range(2.0f, 5.0f);
    }    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
