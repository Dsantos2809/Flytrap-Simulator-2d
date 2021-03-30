using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    PlantAdmin plant;
    public Text PointText;

    void Start()
    {
        plant = FindObjectOfType<PlantAdmin>();
    }

    void Update()
    {
        PointText.text = ("Energy: " + plant.totalPoints);
    }
}
