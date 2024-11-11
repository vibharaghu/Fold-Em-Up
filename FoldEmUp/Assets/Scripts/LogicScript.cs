using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class LogicScript : MonoBehaviour
{
    [SerializeField] public LevelLoader levelLoader;

    public void EnterWizardTower()
    {
        levelLoader.LoadGivenLevel("Wizard Tower");
    }
    public void ReturnToVillage()
    {
        levelLoader.LoadGivenLevel("StartTownScene");
    }

    [YarnCommand("goToPaperFolding")]
    public void GoToPaperFolding()
    {
        levelLoader.LoadGivenLevel("SimpleFold");
    }

}
