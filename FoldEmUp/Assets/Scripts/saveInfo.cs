using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class saveInfo : MonoBehaviour
{
    public bool foldTutorialComplete;
    public bool battleComplete;
    public GameObject shield;
    public bool instruct;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        foldTutorialComplete = false;
        instruct = true;
    }

    void OnSceneLoaded(Scene sc, LoadSceneMode mode)
    {
       
        if (sc.name == "Wizard Tower")
        {
            if (foldTutorialComplete)
            {
                //turn off the intro dialogue
                GameObject.Find("WizardIntroSpeech").GetComponent<YarnInteractable>().DisableConversation();
                //spawn the little shield
                Vector3 spawnPosition = GameObject.Find("Player").transform.position;
                Instantiate(shield, spawnPosition, Quaternion.identity);
            }
        }
        else if (sc.name == "StartTownScene")
        {
            if (!instruct)
            { 
                    GameObject.Find("Instructions").GetComponent<YarnScript>().start = false;
                
            }
            GameObject enemy = GameObject.Find("Enemy");
            if (foldTutorialComplete)
            {
                //turn off the town crier's intro dialogue
                GameObject.Find("TownCrier").GetComponentInChildren<YarnInteractable>().DisableConversation();
                //turn off the enter the wizard tower dialogue
                GameObject.Find("TowerDoorDialogue").GetComponent<YarnInteractable>().DisableConversation();

                //spawn the little shield
                Vector3 spawnPosition = GameObject.Find("Player").transform.position;
                Instantiate(shield, spawnPosition, Quaternion.identity);

                

                //enable the enemy so that the turn-based battle section can start
                enemy.SetActive(true);
                if (battleComplete)
                {
                    enemy.GetComponentInChildren<YarnInteractable>().conversationStartNode = "BattleFinished";
                }
            }
            else
            {
                enemy.SetActive(false);
            }
        }
        else if (sc.name == "SimpleFold")
        {
            foldTutorialComplete = true;
        }
        else if (sc.name == "BattleSequence")
        {
            battleComplete = true;
            GameObject.Find("Player").GetComponent<PlayerScript>().movementEnabled = false;
        }

        if (instruct)
        {
            instruct = false;
        }

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
