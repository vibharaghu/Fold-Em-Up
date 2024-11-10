using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Vector2 loc;
    public GameObject go;
    public GameObject po;
    CircleCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {


        loc = new Vector2(0, 0);
    
    }

    public void setloc(int r , int c, int scale, GameObject obj)
    {
        loc = new Vector2(r - (scale)/2.0f , c - (scale)/2.0f);
        go = Instantiate(obj);
        go.transform.parent = po.transform;
        go.transform.position = loc;
        go.name = "point_" + r + "_" + c;
        //collider = go.AddComponent<CircleCollider2D>();
        //print(go.name);
       
   

    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
