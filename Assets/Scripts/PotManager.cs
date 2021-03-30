using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotManager : MonoBehaviour
{
    public GameObject[] pots;

    void UnlockPot()
    {
        foreach(GameObject pot in pots)
        {
            if (!pot.GetComponent<PotScript>().isUnlocked)
            {
                pot.GetComponent<PotScript>().isUnlocked = true;
                return;
            }
        }
    }

    void CreatePlant()
    {
        foreach (GameObject pot in pots)
        {
            if (!pot.GetComponent<PotScript>().isOccupied)
            {
                pot.GetComponent<PotScript>().CreateNewMouth();
                return;
            }
        }
    }

}
