using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFold : MonoBehaviour
{
    int scale = 0;
    GameObject[][] grid;
    GameObject[] corners;
    bool[] fold;
    float timer = 0f;
    bool initialized = false;
    bool selected = false;
    public Sprite folded_one;
    public Sprite folded_two_adjacent;
    public Sprite folded_two_opposite;
    public Sprite folded_three;
    public Sprite folded_four;
    bool clear = false;
    // Start is called before the first frame update
    void Start()
    {

        
    }

    public void init(GameObject[][] grid, int scale)
    {
        this.scale = scale;
        this.grid = grid;
        fold = new bool[4];
        fold[0] = false;
        fold[1] = false;
        fold[2] = false;
        fold[3] = false;


        corners = new GameObject[4];
        corners[0] = grid[0][0];
        corners[1] = grid[0][scale];
        corners[2] = grid[scale][scale];
        corners[3] = grid[scale][0];

        initialized = true;
        
    }

    void setScale(int scale)
    {
        this.scale = scale;
    }


    int curr_folds() {

        int count = 0;
        for (int i = 0; i < 4; i++)
        {
           
            if (fold[i] == true)
            {
                count += 1;
            }

        }

        return count;
    }



    // Update is called once per frame
    void Update()
    {

        
        if (initialized)
        {

            int count = any_selected();
           
            if (count == 0) {
                flickering_one();
                 
            } else if (count == 1)
            {
                if (!clear)
                {
                    clear_flashing();
                    clear = true;
                }
                
                flickering_two();
            } else
            {
                fold_paper();
                hide_fold_dots();
                clear_selected();
            }
            

        }


    }


    void fold_paper()
    {
        ArrayList fold_count = new ArrayList(); 
        for (int i = 0; i < 4; i++)
        {
            if (fold[i])
            {
                fold_count.Add(i);
                //print("Folded " + i);
            }
        }

        int corner_selected = -1;
        for (int i = 0; i < 4; i++)
        {
            if (corners[i].GetComponent<pointSelect>().selected)
            {
                corner_selected = i;
            }
        }

        if (fold_count.Count == 0)
        {
            GameObject paper = GameObject.Find("Paper Mesh");
            paper.GetComponent<SpriteRenderer>().sprite = folded_one;
            paper.GetComponent<SpriteRenderer>().sortingOrder = -1;

            fold[corner_selected] = true;   
            paper.transform.Rotate(0, 0, 90 + (-90 * corner_selected));

        } else if (fold_count.Count == 1)
        {
            if (((int) fold_count[0] + 1) % 4 == corner_selected || ((int)fold_count[0] - 1) % 4 == corner_selected)
            {
                GameObject paper = GameObject.Find("Paper Mesh");
                paper.GetComponent<SpriteRenderer>().sprite = folded_two_adjacent;
                paper.GetComponent<SpriteRenderer>().sortingOrder = -1;
                fold[corner_selected] = true;

                if (((int)fold_count[0] - 1) % 4 == corner_selected)
                {
                    paper.transform.Rotate(0, 0, 90);
                }

            } else {

                GameObject paper = GameObject.Find("Paper Mesh");
                paper.GetComponent<SpriteRenderer>().sprite = folded_two_opposite;
                paper.GetComponent<SpriteRenderer>().sortingOrder = -1;
                fold[corner_selected] = true;

            }

        } else if (fold_count.Count == 2)
        {
            GameObject paper = GameObject.Find("Paper Mesh");
            paper.GetComponent<SpriteRenderer>().sprite = folded_three;
            paper.GetComponent<SpriteRenderer>().sortingOrder = -1;
            
            paper.transform.rotation = Quaternion.identity;
            int not_folded = -1;
            for (int i = 0; i < 4; i++)
            {
                if (!fold[i] && i != corner_selected)
                {
                    not_folded = i;
                    //print("not "+ not_folded);
                }
            }

            paper.transform.Rotate(0, 0, (-90 * not_folded));
            fold[corner_selected] = true;
        } else if (fold_count.Count == 3)
        {
            GameObject paper = GameObject.Find("Paper Mesh");
            paper.GetComponent<SpriteRenderer>().sprite = folded_four;
            paper.GetComponent<SpriteRenderer>().sortingOrder = -1;
            fold[corner_selected] = true;
        }

        
    }


    void hide_fold_dots()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4 - r; c++)
            {
                if (fold[0])
                {
                    GameObject point = grid[r][c];
                    point.GetComponent<pointSelect>().hide_dot();
                }

                if (fold[1])
                {
                    GameObject point = grid[r][scale - c];
                    point.GetComponent<pointSelect>().hide_dot();
                }

                if (fold[2])
                {
                    GameObject point = grid[scale - r][scale - c];
                    point.GetComponent<pointSelect>().hide_dot();
                }

                if (fold[3])
                {
                    GameObject point = grid[scale - r][c];
                    point.GetComponent<pointSelect>().hide_dot();
                }
            }
        }
    }


        
       

        

        
    


    void flickering_two()
    {
        
        if (selected)
        {
           
            timer += Time.deltaTime;
            if (timer > 0 && timer < 0.3f)
            {
                GameObject p = grid[scale / 2][scale / 2];
                pointSelect ps = p.GetComponent<pointSelect>();
                if (ps.entered == false && ps.selected == false)
                {
                    p.GetComponent<SpriteRenderer>().color = Color.yellow;
                } 
            }
            else if (timer < 1)
            {

                GameObject p = grid[scale / 2][scale / 2];
                pointSelect ps = p.GetComponent<pointSelect>();
                if (ps.entered == false && ps.selected == false)
                {
                    p.GetComponent<SpriteRenderer>().color = Color.gray;
                }

            }
            else
            {
                timer = 0;
            }
           
        }
    }

   

    void flickering_one()
    {

            
                 
        timer += Time.deltaTime;
        if (timer > 0 && timer < 0.3f && !selected)
        {
            for (int i = 0; i < 4; i++)
            {
                if (!fold[i])
                {
                    GameObject p = corners[i];
                    pointSelect ps = p.GetComponent<pointSelect>();
                    if (ps.entered == false && ps.selected == false)
                    {
                        p.GetComponent<SpriteRenderer>().color = Color.yellow;
                        //print("Blinking " + p.name);
                    }

                }
                else
                {
                    //print(i);
                    GameObject p = corners[i];
                    //print(p.name);
                }



            }
        }
        else if (timer < 1 && !selected)
        {
            for (int i = 0; i < 4; i++)
            {
                if (!fold[i])
                {
                    GameObject p = corners[i];
                    pointSelect ps = p.GetComponent<pointSelect>();
                    if (ps.entered == false && ps.selected == false)
                    {
                        p.GetComponent<SpriteRenderer>().color = Color.gray;
                    }
                }


            }

        }
        else if (!selected)
        {
            timer = 0;
        } 
       
            


    }

    int any_selected()
    {
        int count = 0; 
        for (int i = 0; i <= scale; i++)
        {
            for (int j = 0; j <= scale; j++)
            {
                GameObject p = grid[i][j];
                pointSelect ps = p.GetComponent<pointSelect>();
                print("Corner " + p.name);
                if (ps.selected == true)
                {
                    print(p.name + " selected");
                    selected = true;
                    count += 1;
                }
            }
            

        }
   
        return count;
    }

    void clear_flashing()
    {
     
        for (int i = 0; i <= scale; i++)
        {
            for (int j = 0; j <= scale; j++)
            {
                GameObject p = grid[i][j];
                pointSelect ps = p.GetComponent<pointSelect>();
                if (!ps.selected)
                {
                    p.GetComponent<SpriteRenderer>().color = Color.grey;
                }
            }


        }
    }

    void clear_selected()
    {
        selected = false;
        for (int i = 0; i <= scale; i++)
        {
            for (int j = 0; j <= scale; j++)
            {
                GameObject p = grid[i][j];
                pointSelect ps = p.GetComponent<pointSelect>();
                ps.selected = false;
                p.GetComponent<SpriteRenderer>().color = Color.grey;
            }


        }
    }
}


