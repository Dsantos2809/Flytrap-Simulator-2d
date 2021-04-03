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

    void Start()
    {
        pitcher = Pitcher.Idle;
        timeIdle = 0.0f;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Attracted" && pitcher != Pitcher.Eating)
        {
            StartCoroutine(DestroyFly(other));
            points = other.gameObject.GetComponent<FlyOscillator>().points;
            timeToDigest = other.gameObject.GetComponent<FlyOscillator>().digestionTime + plantAddedTime;
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
        else if (pitcher == Pitcher.Eating)
        {
            DigestFly();
        }
        else
        {
            timeIdle += Time.deltaTime;
            if(timeIdle > 15.0f)
            {
                pitcher = Pitcher.Attracting;            
            }
        }
    }

    void AttractFly()
    {
        enemiesList = GameObject.FindGameObjectsWithTag("Insect");
        if(enemiesList.Length > 0)
        {
            enemiesList[0].GetComponent<FlyOscillator>().plant = gameObject;
            enemiesList[0].tag = "Attracted";
            LaneScript.quantitySpawned--;
            pitcher = Pitcher.Eating;
            Debug.Log(pitcher);
        }
    }

    private void DigestFly()
    {
        t += Time.deltaTime;
        animator.SetFloat("time", t / timeToDigest);
        if (t / timeToDigest > 1)
        {
            pitcher = Pitcher.Idle;
            t = 0.0f;
            timeIdle = 0.0f;
            animator.SetBool("isEating", false);
        }
    }

}