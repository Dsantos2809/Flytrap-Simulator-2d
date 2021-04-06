using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherAdmin : MonoBehaviour
{
    public enum Pitcher { Eating, Attracting, Idle };
    public Pitcher pitcher;

    public GameObject mainPlant;

    public Animator animator;

    GameObject[] enemiesList;

    public float plantAddedTime = 10.0f;

    public int totalPoints = 0;
    int points;
    float timeToDigest;
    float t;
    float timeIdle;

    bool oneTime = false;

    void Start()
    {

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attracted")
        {
            StartCoroutine(DestroyFly(other));
            points = other.gameObject.GetComponent<FlyOscillator>().points;
            timeToDigest = other.gameObject.GetComponent<FlyOscillator>().digestionTime * plantAddedTime;
            pitcher = Pitcher.Eating;
            totalPoints += points;
            mainPlant.GetComponent<PlantAdmin>().totalPoints += totalPoints;
            animator.SetBool("isEating", true);
        }
    }

    IEnumerator DestroyFly(Collider2D other)
    {
        Destroy(other.gameObject);
        LaneScript.quantitySpawned--;
        yield return new WaitForSeconds(1.0f);
    }

    void Update()
    {
        if(pitcher == Pitcher.Attracting)
        {
            AttractFly();
        }
        if (pitcher == Pitcher.Eating)
        {
            DigestFly();
        }
        if(pitcher == Pitcher.Idle)
        {
            timeIdle += Time.deltaTime;
            if(timeIdle > 15.0f)
            {
                pitcher = Pitcher.Attracting;
                oneTime = false;
            }
        }
    }

    void AttractFly()
    {
        if (!oneTime)
        {
            oneTime = true;
            enemiesList = GameObject.FindGameObjectsWithTag("Insect");
            int insect = UnityEngine.Random.Range(0, enemiesList.Length);
            if (enemiesList.Length > 0)
            {
                enemiesList[insect].GetComponent<FlyOscillator>().plant = gameObject;
                enemiesList[insect].tag = "Attracted";
            }
        }
    }

    private void DigestFly()
    {
        t += Time.deltaTime;
        if (t / timeToDigest > 1)
        {
            pitcher = Pitcher.Idle;
            t = 0.0f;
            timeIdle = 0.0f;
            animator.SetBool("isEating", false);
        }
    }

}