using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public static GameObject playerInstance { get; private set; }
    //[Header("Spawn")]
    //public Vector2 SpawnLocation;
    [Header("Movement")]
    public float movespeed = 8f;
    public Vector2 movement;
    public bool movementEnabled;
    public bool facingRight = true;
    [Header("Components")]
    public Rigidbody2D myRigidBody2D;
    public Animator animator;
    public AudioSource footSteps;

    private void Awake()
    {
        if (playerInstance != null && playerInstance != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            playerInstance = gameObject;
        }
        myRigidBody2D = GetComponent<Rigidbody2D>();
        if (SceneManager.GetActiveScene().name != "BattleSequence")
        {
            movementEnabled = true;
        }
        else
        {
            movementEnabled = false;
        }
        animator = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            StartCoroutine(TurnInactive());
        }
        //transform.position = SpawnLocation;
    }

    IEnumerator TurnInactive()
    {
        yield return new WaitForSeconds(.01f);
        gameObject.SetActive(false);
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    private void FixedUpdate()
    {
        handleMovement();
    }
    private void handleMovement()
    {
        if (movementEnabled)
        {
            myRigidBody2D.MovePosition(myRigidBody2D.position + movement * movespeed * Time.fixedDeltaTime);
            float absoluteSpeed = Mathf.Abs(movement.x) + Mathf.Abs(movement.y);
            if (absoluteSpeed > .01)
            {
                if (!footSteps.isPlaying) footSteps.Play();
            }
            //else
            //{
            //    footSteps.Stop();
            //}

            float horizontalSpeed = movement.x;
            if ((horizontalSpeed > 0 && !facingRight) || (horizontalSpeed < 0 && facingRight))
            {
                Flip();
            }
            animator.SetFloat("Speed", absoluteSpeed);
        }
        else animator.SetFloat("Speed", 0);
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 CurrScale = transform.localScale;
        CurrScale.x *= -1f;
        transform.localScale = CurrScale;
        //transform.Rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }
    public void setMoveSpeed(float speed)
    {
        movespeed = speed;
    }

    [YarnCommand("canMove")]
    public void canMove(bool canMove)
    {
        movementEnabled = canMove;
    }
}
