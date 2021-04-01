﻿using System;
using System.Collections;
using UnityEngine;

public class FlyOscillator : MonoBehaviour
{
    float max = 7.0f;
    float min = -7.0f;
    public float speed = 5.0f;
    public int insect;
    Vector3 newPosition;
    static Vector3 newScale = new Vector3(0.0f, 0.0f, 1.0f);
    Vector3 startScale;

    public int points;
    public float digestionTime;

    public GameObject lane;

    float t = 0.0f;
    float tH = 0.0f;

    public GameObject plant;

    SpriteRenderer sprite;

    Transform mychildtransform;

    float timeCounter = 0f;
    public float timeForKill;

    void OnMouseEnter()
    {
        if(gameObject.tag != "Dead" && Time.timeScale != 0 && !PlantAdmin.isEating)
        {
            mychildtransform.gameObject.SetActive(true);
            Transform nowPosition = transform;
            gameObject.tag = "Stopped";
            StartCoroutine(ChangeTag());
            transform.position = nowPosition.position;
        }
    }

    IEnumerator ChangeTag()
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.tag = "Insect";
    }


    void OnMouseExit()
    {
        mychildtransform.gameObject.SetActive(false);
    }

     void OnMouseOver()
    {
        if (!PlantAdmin.isEating)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= timeForKill)
            {
                gameObject.tag = "Dead";
                PlantAdmin.isEating = true;
            }
            else
            {
                mychildtransform.localScale = Vector3.Lerp(startScale, newScale, timeCounter / timeForKill);
                Debug.Log(timeCounter);
            }
        }
        
    }
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        lane = transform.parent.gameObject;
        PositionChange();

        mychildtransform = gameObject.transform.Find("Aura");
        startScale = mychildtransform.localScale;
    }

    void Update()
    {
        if(gameObject.tag == "Dead")
        {
            FlyAttractor();
        }
        else if(gameObject.tag == "Insect")
        {
            switch (insect)
            {
                case 1:
                    MoveTheFly();
                    break;
                case 2:
                    MoveTheBee();
                    break;
                case 3:
                    MoveTheMosquitoe();
                    break;
                case 4:
                    MoveTheBettle();
                    break;
                default:
                    throw new Exception();
            }
        }
        else
        {
            Vector3 pos = transform.position;
            transform.position = pos + UnityEngine.Random.insideUnitSphere * Time.deltaTime;
        }
    }


    private void MoveTheMosquitoe()
    {
        if (transform.position.x < -7)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            if (Vector2.Distance(transform.position, newPosition) < 1)
            {
                PositionChange();
                if (UnityEngine.Random.Range(-1.0f, 1.0f) > 0)
                {
                    sprite.flipX = !sprite.flipX;
                }
                sprite.flipX = !sprite.flipX;
            }
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed/2);
        }
    }

    private void PositionChange()
    {
        newPosition = new Vector2(UnityEngine.Random.Range(-7.0f, 7.0f), UnityEngine.Random.Range(0.0f, 4.0f));
    }

    private void MoveTheBettle()
    {
        if (transform.position.x < -7)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(Mathf.Lerp(min, max, t), transform.position.y, 0);
            t += speed / 20 * Time.deltaTime;
            if (t > 1.0f)
            {
                float temp = max;
                max = min;
                min = temp;
                t = 0.0f;
                sprite.flipX = !sprite.flipX;
            }
        }
    }

    private void MoveTheBee()
    {
        float y = 0;
        if (transform.position.x < -7)
        {
            transform.position += transform.right * speed * Time.deltaTime;
            y = transform.position.y;
        }
        else
        {
            transform.position = new Vector3(Mathf.Lerp(min, max, t), transform.position.y, 0);
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed/2, 2) + y, 0);
            t += speed / 10 * Time.deltaTime;
            if (t > 1.0f)
            {
                float temp = max;
                max = min;
                min = temp;
                t = 0.0f;
                sprite.flipX = !sprite.flipX;
            }
        }
    }

    private void MoveTheFly()
    {
        if(transform.position.x < -7)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(Mathf.Lerp(min, max, t), transform.position.y, 0);
            t += speed / 10 * Time.deltaTime;
            if (t > 1.0f)
            {
                float temp = max;
                max = min;
                min = temp;
                t = 0.0f;
                sprite.flipX = !sprite.flipX;
            }
        }
    }

    private void FlyAttractor()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, plant.transform.position.x, tH), Mathf.Lerp(transform.position.y, plant.transform.position.y + 1, tH), 0);
        tH += speed / 1000 * Time.deltaTime;
    }
}