using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAdmin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            Destroy(other.gameObject);
        }
    }
    void Update()
    {
        
    }
}
