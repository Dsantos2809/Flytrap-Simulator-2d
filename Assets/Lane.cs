using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public bool isFree;

    public GameObject newInsect;

    void Start()
    {
        CreateNewEnemy();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Insect")
        {
            isFree = false;
            Debug.Log("false");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Insect")
        {
            isFree = true;
            Debug.Log("true");    
        }
    }

    void OnDestroy()
    {
        CreateNewEnemy();
        Debug.Log("OnDestroy1");
    }

    private void CreateNewEnemy()
    {
        GameObject childObject = Instantiate(newInsect, new Vector3(transform.position.x - 10, transform.position.y, 0), Quaternion.identity);
        childObject.transform.parent = transform;
        childObject.GetComponent<FlyOscillator>().speed = Random.Range(2.0f, 5.0f);
    }    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
