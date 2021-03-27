using System.Collections;
using UnityEngine;

public class FlyOscillator : MonoBehaviour
{
    float max = 7.0f;
    float min = -7.0f;
    public float speed = 5.0f;

    float laneNumber;
    public GameObject lane;

    float t = 0.0f;

    public SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        lane = transform.parent.gameObject;
        laneNumber = lane.transform.position.y;
    }

    void Update()
    {
        if(gameObject.tag == "Insect")
        {
            MoveTheFly();
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
            transform.position = new Vector3(Mathf.Lerp(min, max, t), laneNumber, 0);
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
}
