using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToVillageScript : MonoBehaviour
{
    [Header("Components")]
    public LogicScript logic;
    void Awake()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.ReturnToVillage();
        }
    }
}
