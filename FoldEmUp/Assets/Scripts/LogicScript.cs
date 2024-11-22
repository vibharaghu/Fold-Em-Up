using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class LogicScript : MonoBehaviour
{
    [SerializeField] public LevelLoader levelLoader;
    [Header("Components")]
    private PlayerScript playerScript;
    private void Awake()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }
    public void EnterWizardTower()
    {
        playerScript.canMove(false);
        levelLoader.LoadGivenLevel("Wizard Tower");
    }
    public void EnterHouse1()
    {
        playerScript.canMove(false);
        levelLoader.LoadGivenLevel("House1Scene");
    }
    public void EnterHouse2()
    {
        playerScript.canMove(false);
        levelLoader.LoadGivenLevel("House2Scene");
    }
    public void EnterHouse3()
    {
        playerScript.canMove(false);
        levelLoader.LoadGivenLevel("House3Scene");
    }
    public void ReturnToVillage()
    {
        playerScript.canMove(false);
        levelLoader.LoadGivenLevel("StartTownScene");
    }

    [YarnCommand("goToPaperFolding")]
    public void GoToPaperFolding()
    {
        levelLoader.LoadGivenLevel("SimpleFold");
    }

}
