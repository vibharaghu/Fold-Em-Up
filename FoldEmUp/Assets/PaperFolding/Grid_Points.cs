using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid_Points : MonoBehaviour
{
    public int scale = 8;
    public GameObject point;
    GameObject[][] grid;
    GameObject go;
    // Start is called before the first frame update
    void Start()
    {
        go = gameObject;
        grid = new GameObject[scale + 1][];

        transform.localScale = new Vector3(scale/10.0f, scale/10.0f, 0 );
        for (int r = 0; r < scale + 1; r++)
        {
            grid[r] = new GameObject[scale + 1];
            for (int c = 0; c < scale + 1; c++)
            {
                GameObject obj = Instantiate(point);
                obj.GetComponent<pointSelect>().setloc(r, c, scale, go);
                grid[r][c] = obj;
                print(grid[r][c]);
           
             
            }
                
        }

     
       GetComponent<SimpleFold>().init(grid, scale);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void make_grid()
    {

    }
}
