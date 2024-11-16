using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
    [Header("Components")]
    public LogicScript logic;
    public int house; //0 for wizard tower, 3 for house 3
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
            if(house == 3) 
            {
                logic.EnterHouse3();
            }
            else
            {
                logic.EnterWizardTower();

            }
        }
    }
}
