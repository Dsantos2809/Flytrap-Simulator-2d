using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotScript : MonoBehaviour
{
    public GameObject plant;
    public bool isOccupied = false;
    public bool isUnlocked = false;

    public void CreateNewMouth()
    {
        plant.SetActive(true);
        isOccupied = true;
    }
    void Update()
    {

    }
}
