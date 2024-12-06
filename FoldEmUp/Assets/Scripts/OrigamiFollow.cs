using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrigamiFollow : MonoBehaviour
{
    private Transform target;
    public float speed;
    private float speedModifier = .5f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = (target.position - gameObject.transform.position).magnitude;
        transform.position = Vector2.MoveTowards(transform.position, target.position, (speed + Mathf.Pow(speedModifier * distanceToPlayer,2)) * Time.deltaTime);
    }
}
