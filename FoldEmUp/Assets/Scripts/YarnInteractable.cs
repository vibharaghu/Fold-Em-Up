using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class YarnInteractable : MonoBehaviour
{
    [SerializeField] private string conversationStartNode;

    private DialogueRunner dialogueRunner;
    private bool interactable = true;
    private bool isCurrentConversation = false;

    public void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
        dialogueRunner.onDialogueComplete.AddListener(EndConversation);
    }

    public void OnTriggerEnter2D()
    {
        Debug.Log("OnTriggerEnter");
        // if this character is enabled and no conversation is already running
        if (interactable && !dialogueRunner.IsDialogueRunning)
        {
            // then run this character's conversation
            StartConversation();
        }
    }

    private void StartConversation()
    {
        isCurrentConversation = true;
        dialogueRunner.StartDialogue(conversationStartNode);
    }

    private void EndConversation()
    {
        if (isCurrentConversation)
        {
            isCurrentConversation = false;
        }
    }

    [YarnCommand("disable")]
    public void DisableConversation()
    {
        interactable = false;
    }
}
