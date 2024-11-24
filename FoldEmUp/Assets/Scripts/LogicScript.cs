using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class LogicScript : MonoBehaviour
{
    [SerializeField] public LevelLoader levelLoader;
    private void Awake()
    {
        levelLoader = GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LevelLoader>();
    }
    public void EnterWizardTower(Vector3 targetVector)
    {
        levelLoader.LoadGivenLevel("Wizard Tower", targetVector, 4);
    }
    public void EnterHouse1()
    {
        levelLoader.LoadGivenLevel("House1Scene", new Vector3(4.65f, -6.4f, -2f), 4);
    }
    public void EnterHouse2()
    {
        levelLoader.LoadGivenLevel("House2Scene", new Vector3(4.65f, -6.4f, -2f), 4);
    }
    public void EnterHouse3()
    {
        levelLoader.LoadGivenLevel("House3Scene", new Vector3(4.65f, -6.4f, -2f), 4);
    }
    public void ReturnToVillage(Vector3 targetVector)
    {
        levelLoader.LoadGivenLevel("StartTownScene", targetVector, 6);
    }

    [YarnCommand("goToPaperFolding")]
    public void GoToPaperFolding()
    {
        levelLoader.LoadGivenLevel("SimpleFold", Vector3.zero, 4);
    }

}
