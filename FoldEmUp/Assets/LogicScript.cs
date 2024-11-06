using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public void EnterWizardTower()
    {
        SceneManager.LoadScene("Wizard Tower");
    }
    public void ReturnToVillage()
    {
        SceneManager.LoadScene("StartTownScene");
    }
}
