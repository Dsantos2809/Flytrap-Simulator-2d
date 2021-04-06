using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public int score;
    public int displayScore;

    PlantAdmin plant;
    public Text PointText;

    void Start()
    {
        plant = FindObjectOfType<PlantAdmin>();
        StartCoroutine(ScoreUpdater());
    }

    private IEnumerator ScoreUpdater()
    {
        while (true)
        {
            if (displayScore < score)
            {
                displayScore++;
                PointText.text = ("Energy: " + displayScore);
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    void Update()
    {
        score = plant.totalPoints;

    }
}
