using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnScript : MonoBehaviour
{

    [SerializeField] private string conversationStartNode;

    private DialogueRunner dialogueRunner;
    public bool start = true;
    private bool isCurrentConversation = false;
    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
        StartConversation();
    }

    private void StartConversation()
    {
        if (start) {

            isCurrentConversation = true;
            dialogueRunner.StartDialogue(conversationStartNode);
        }

        
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            if (this.transform.childCount > 0)
            {
                //turn off the dialogue indicator
                this.transform.GetChild(0).gameObject.SetActive(false);
            }
            isCurrentConversation = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
