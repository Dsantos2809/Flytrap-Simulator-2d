using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    public GameObject plant;
    public bool isOccupied;
    public bool isUnlocked;

    // Start is called before the first frame update
    void Start()
    {

    }
    public void CreateNewMouth()
    {
        if (!isOccupied)
        {
            GameObject childObject = Instantiate(plant, new Vector3(transform.position.x, transform.position.y + 1, 0), Quaternion.identity);
            childObject.transform.parent = transform;
        }
    }
    void Update()
    {
        if (transform.Find("Venus Flytrap"))
        {
            isOccupied = true;
        }
        else isOccupied = false;
    }
}
