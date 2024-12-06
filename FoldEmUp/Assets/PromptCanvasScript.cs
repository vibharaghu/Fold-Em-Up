using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PromptCanvasScript : MonoBehaviour
{
    public static GameObject PromptCanvasInstance { get; private set; }
    public GameObject player;
    [Header("Flags")]
    private bool triggerMovementFlag = true;
    private bool triggerClickFlag = true;
    [Header("Components")]
    public GameObject MovementPrompt;
    public GameObject ClickPrompt;
    // Start is called before the first frame update
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Awake()
    {
        if (PromptCanvasInstance != null && PromptCanvasInstance != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            PromptCanvasInstance = gameObject;
            OnLoading(SceneManager.GetActiveScene().name);
        }
    }
    public void OnLoading(string sceneName)
    {
        Debug.Log(sceneName);
        if (sceneName == "SimpleFold")
        {
            if (triggerClickFlag)
            {
                ClickPrompt.SetActive(true);
            }
        }
        else
        {
            if (triggerMovementFlag)
            {
                MovementPrompt.SetActive(true);
            }
        }
    }
    public void DisableMovementPrompt()
    {
        MovementPrompt.SetActive(false);
        triggerMovementFlag = false;
    }
    public void DisableClickPrompt()
    {
        ClickPrompt.SetActive(false);
        triggerClickFlag = false;
    }
    private void Update()
    {
        if (triggerMovementFlag)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) > .1 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > .1)
            {
                DisableMovementPrompt();
            }
        }
    }
}
