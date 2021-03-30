using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAdmin : MonoBehaviour
{
    public enum State { Eating, Attracting, Idle };
    public State statePlant;

    public GameObject lane;

    public float timeToEat = 5.0f;

    public int totalPoints = 0;
    int points;
    public float timeToDigest;
    float t;
    float timeIdle;

    void Start()
    {
        statePlant = State.Idle;
        timeIdle = 0.0f;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            StartCoroutine(DestroyFly(other));
            points = other.gameObject.GetComponent<FlyOscillator>().points;
            timeToDigest = other.gameObject.GetComponent<FlyOscillator>().digestionTime;
            statePlant = State.Eating;
            totalPoints += points;
        }
    }

    IEnumerator DestroyFly(Collider2D other)
    {
        Destroy(other.gameObject);
        other.gameObject.GetComponent<FlyOscillator>().lane.GetComponent<Lane>().isDead = true;
        yield return new WaitForSeconds(1.0f);
    }

    void Update()
    {
        if (statePlant == State.Attracting)
        {
            timeIdle += Time.deltaTime;
            if (timeIdle >= timeToEat)
            {
                AttractFly();
                timeIdle = 0.0f;
            }
        }
        if (statePlant == State.Eating)
        {
            DigestFly();
        }
    }

    private void AttractFly()
    {
        lane.GetComponentInChildren<FlyOscillator>().gameObject.tag = "Dead";
    }

    private void DigestFly()
    {
        t += Time.deltaTime;
        if (t / timeToDigest > 1)
        {
            statePlant = State.Idle;
            t = 0.0f;
            timeIdle = 0.0f;
        }
    }

}
