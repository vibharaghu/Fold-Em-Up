using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointSelect : MonoBehaviour
{
    public SpriteRenderer sprend;
    public bool entered;
    public bool selected;
    public Vector2 loc;
    int scale = 0;
    // Start is called before the first frame update
    void Start()
    {
        sprend = GetComponent<SpriteRenderer>();
        sprend.color = Color.gray;
        entered = false;
    }

    public void setloc(int r, int c, int scale, GameObject po)
    {
        loc = new Vector2(r - (scale) / 2.0f, c - (scale) / 2.0f);
        this.scale = scale;
        transform.parent = po.transform;
        transform.position = loc;
        gameObject.name = "point_" + r + "_" + c;
        //collider = go.AddComponent<CircleCollider2D>();
        //print(go.name);



    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUp()
    {
        if (selected == false)
        {
            selected = true;

            
            

        } else {
            selected = false;
            
        }
    }



    private void OnMouseEnter()
    {

        if (!selected)
        {

            //print("entered" + gameObject.name);
            sprend.color = Color.black;
            entered = true;
        }

        
    }

    public void hide_dot()
    {
        gameObject.SetActive(false);

    }


    private void OnMouseExit()
    {
        if (!selected) {

            //print("exited" + gameObject.name);
            sprend.color = Color.gray;
            entered = false;


        }

       
    }
}
