using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static GameObject levelLoaderInstance { get; private set; }
    public Animator transition;
    public float transitionTime = 1f;
    public GameObject player;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Awake()
    {
        if (levelLoaderInstance != null && levelLoaderInstance != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            levelLoaderInstance = gameObject;
        }
    }
    public void LoadGivenLevel(string sceneName,Vector3 targetPosition, float speed)
    {
        StartCoroutine(LoadLevel(sceneName, targetPosition,speed));
    }

    IEnumerator LoadLevel(string sceneName, Vector3 targetPosition, float speed)
    {
        PlayerScript playerScript = player.GetComponent<PlayerScript>();
        playerScript.canMove(false);
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
        transition.SetTrigger("End");
        if (targetPosition != Vector3.zero)
        {
            player.transform.position = targetPosition;
        }
        playerScript.canMove(true);
        playerScript.setMoveSpeed(speed);
    }
}