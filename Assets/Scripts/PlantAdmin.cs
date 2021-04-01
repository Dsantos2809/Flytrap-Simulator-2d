using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAdmin : MonoBehaviour
{
    public enum State { Eating, Attracting, Idle };
    public State statePlant;

    public Animator animator;

    GameObject[] enemiesList;

    public static bool isEating = false;

    public float timeToEat = 5.0f;

    public int totalPoints = 0;
    int points;
    float timeToDigest;
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
            isEating = true;
            totalPoints += points;
            animator.SetBool("IsEating", true);
            animator.SetInteger("Insect", other.gameObject.GetComponent<FlyOscillator>().insect);
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

    Transform GetClosestEnemy(GameObject[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }

        return bestTarget;
    }

    void AttractFly()
    {
        enemiesList = GameObject.FindGameObjectsWithTag("Insect");
        Transform insect = GetClosestEnemy(enemiesList);
        insect.gameObject.GetComponent<FlyOscillator>().plant = gameObject;
        insect.gameObject.tag = "Dead";
        LaneScript.quantitySpawned--;
        isEating = true;
    }

    private void DigestFly()
    {
        t += Time.deltaTime;
        animator.SetFloat("time", t / timeToDigest);
        if (t / timeToDigest > 1)
        {
            isEating = false;
            statePlant = State.Idle;
            t = 0.0f;
            timeIdle = 0.0f;
            animator.SetBool("IsEating", false);
        }
    }

}
