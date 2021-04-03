using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAdmin : MonoBehaviour
{
    public enum State { Eating, Attracting, Idle };
    public State statePlant;

    public Animator animator;


    public static bool isEating = false;

    public float plantAddedTime = 6.0f;

    public int totalPoints = 0;
    int points;
    float timeToDigest;
    float t;

    void Start()
    {
        statePlant = State.Idle;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            StartCoroutine(DestroyFly(other));
            points = other.gameObject.GetComponent<FlyOscillator>().points;
            Debug.Log(plantAddedTime);
            timeToDigest = other.gameObject.GetComponent<FlyOscillator>().digestionTime * plantAddedTime;
            statePlant = State.Eating;
            isEating = true;
            totalPoints += points;
            animator.SetBool("IsEating", true);
            animator.SetInteger("Insect", other.gameObject.GetComponent<FlyOscillator>().insect);
            FindObjectOfType<AudioManager>().Play("Munch"); 
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
        if (statePlant == State.Eating)
        {
            DigestFly();
        }
    }

    private void DigestFly()
    {
        t += Time.deltaTime;
        Debug.Log(timeToDigest);
        animator.SetFloat("time", t / timeToDigest);
        if (t / timeToDigest > 1)
        {
            isEating = false;
            statePlant = State.Idle;
            t = 0.0f;
            animator.SetBool("IsEating", false);
        }
    }

}
