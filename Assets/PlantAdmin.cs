using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantAdmin : MonoBehaviour
{

    private IEnumerator coroutineCreate;
    private IEnumerator coroutineDestroy;
    public enum State { Eating, Attracting, Idle };
    public State statePlant;
    // Start is called before the first frame update
    void Start()
    {
        statePlant = State.Idle;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            statePlant = State.Eating;
            StartCoroutine(CreateNewFly(other));
            StartCoroutine(DestroyFly(other));
        }
    }

    IEnumerator CreateNewFly(Collider2D other)
    {
        other.gameObject.GetComponent<FlyOscillator>().lane.GetComponent<Lane>().CreateNewEnemy();
        yield return new WaitForSeconds(2.0f);
    }

    IEnumerator DestroyFly(Collider2D other)
    {
        Destroy(other.gameObject);
        yield return new WaitForSeconds(2.0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Dead")
        {
            statePlant = State.Idle;
        }
    }
    void Update()
    {
        
    }


}
