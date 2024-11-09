using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using Yarn.Unity;

public class PlayerScript : MonoBehaviour
{
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
    private void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        movementEnabled = true;
        animator = GetComponent<Animator>();
        //transform.position = SpawnLocation;
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
        }

        float horizontalSpeed = movement.x;
        if ((horizontalSpeed > 0 && !facingRight) || (horizontalSpeed < 0 && facingRight))
        {
            Flip();
        }
        animator.SetFloat("Speed", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 CurrScale = transform.localScale;
        CurrScale.x *= -1f;
        transform.localScale = CurrScale;
        //transform.Rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    [YarnCommand("canMove")]
    public void canMove(bool canMove)
    {
        movementEnabled = canMove;
    }
}
