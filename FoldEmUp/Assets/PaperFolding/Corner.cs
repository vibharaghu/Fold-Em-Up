using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        CircleCollider2D collider  = GetComponent<CircleCollider2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    private void OnMouseEnter()
    {
        print("entered");
    }


    private void OnMouseExit()
    {
        print("exited");
    }


}
