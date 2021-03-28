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
    float tH = 0.0f;

    public GameObject plant;

    public SpriteRenderer sprite;

    public bool isDead = false;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        lane = transform.parent.gameObject;
        laneNumber = lane.transform.position.y;
    }

    void Update()
    {
        if (isDead)
        {
            gameObject.tag = "Dead";
        }
        if(gameObject.tag == "Dead")
        {
            FlyAttractor();
        }
        else if(gameObject.tag == "Insect")
        {
            MoveTheFly();
        }    
    }

    private void FlyAttractor()
    {
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, plant.transform.position.x, tH), Mathf.Lerp(transform.position.y, plant.transform.position.y, tH), 0);
        tH += speed / 1000 * Time.deltaTime;
        Debug.Log(tH);
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
