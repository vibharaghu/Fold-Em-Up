using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleFold : MonoBehaviour
{
    [SerializeField] public LevelLoader levelLoader;
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
    List<pointSelect> prev_selec = null;
    bool clear = false;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
        player = GameObject.FindGameObjectWithTag("Player");
        player.SetActive(false);
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

        GameObject paper = GameObject.Find("Paper Mesh");
        paper.GetComponent<SpriteRenderer>().sortingOrder = -1;

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
            
            pointSelect four_count = four_selected();
            int middle_count = middle_selected();
            List<pointSelect> any_selec = any_selected();
            
            if (four_count == null) {
                flickering_one();
                if (any_selec.Count > 0)
                {
                    flash_red(any_selec[0]);
                    
                }

                if (prev_selec != null)
                {
                    clear_flashing();
                    prev_selec[0].selected = false;
                    selected = false;
                    prev_selec = null;
                    clear = false;
                }
                
                 
            } else if (four_count != null)
            {
                
                if (!clear)
                {
                    clear_flashing();
                    clear = true;
                    four_count.sprend.color = Color.green;
                }
                
                flickering_two();

                if (middle_count == 1)
                {
                    grid[scale / 2][scale / 2].GetComponent<pointSelect>().sprend.color = Color.green;
                    fold_paper();
                    hide_fold_dots();
                    clear_selected();
                    clear = false;
                }
                else if (any_selec.Count == 2)
                {
                    print(prev_selec[0]);
                    print(any_selec[0]);
                    print(any_selec[1]);
                    if (prev_selec[0].name != any_selec[0].name)
                    {

                        flash_red(any_selec[0]);
                        any_selec[0].selected = false;
                    }
                    else
                    {
                        flash_red(any_selec[1]);
                        any_selec[1].selected = false;
                    }

                } else
                {
                    prev_selec = any_selec;
                }
               
                
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

            ///Once all four folds are made, go back to the wizard tower

            //add in a delay/congrats message here before scene change?
            levelLoader.LoadGivenLevel("Wizard Tower", new Vector3(0.897f, -4f, -2f),4);
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


        
       

        
    void flash_red(pointSelect ps)
    {
        
        if (timer > 0 && timer < 1f)
        {
           
            ps.sprend.color = Color.red;
            
        }
        else
        {
            
            ps.selected = false;
            ps.sprend.color = Color.grey;
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

    

    List<pointSelect> any_selected()
    {
        
        List<pointSelect> points = new List<pointSelect>();
        for (int i = 0; i <= scale; i++)
        {
            for (int j = 0; j <= scale; j++)
            {
                GameObject p = grid[i][j];
                pointSelect ps = p.GetComponent<pointSelect>();
                
                if (ps.selected == true)
                {

                    points.Add(ps);
                    
                }
            }
            

        }
   
        return points;
    }

    pointSelect four_selected()
    {
        int count = 0;
        if(corners[0].GetComponent<pointSelect>().selected)
        {
            selected = true;
            return corners[0].GetComponent<pointSelect>();
        }
        if (corners[1].GetComponent<pointSelect>().selected)
        {
            selected = true;
            return corners[1].GetComponent<pointSelect>();
        }
        if (corners[2].GetComponent<pointSelect>().selected)
        {
            selected = true;
            return corners[2].GetComponent<pointSelect>();
        }
        if (corners[3].GetComponent<pointSelect>().selected)
        {
            selected = true;
            return corners[3].GetComponent<pointSelect>();
        }
        selected = false;
        return null;
       
    }

    int middle_selected()
    {
        if (grid[scale / 2][scale/2].GetComponent<pointSelect>().selected)
        {
            selected = true;
            return 1;
        }
        return 0;
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


