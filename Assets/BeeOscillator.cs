using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeOscillator : MonoBehaviour
{
    float max = 7.0f;
    float min = -7.0f;
    public float speed = 5.0f;

    float laneNumber;
    public GameObject lane;

    float t = 0.0f;
    float tH = 0.0f;

    public SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        lane = transform.parent.gameObject;
        laneNumber = lane.transform.position.y;
    }

    void Update()
    {
        MoveTheBee();
    }

    private void MoveTheBee()
    {
        if (transform.position.x < -7)
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else
        {
            transform.position = new Vector3(Mathf.Lerp(min, max, t), transform.position.y, 0);
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(tH, 1) + laneNumber, 0);
            t += speed / 10 * Time.deltaTime;
            tH += speed / 2 * Time.deltaTime;
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
}
