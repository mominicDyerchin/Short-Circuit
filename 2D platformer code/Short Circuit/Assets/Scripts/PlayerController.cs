using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpHeight;

    //Variables for finding the ground
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;

    private bool doubleJumped;

    public LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
        if (colliders.Length > 0) { grounded = true; }
        else {grounded = false;}
    }

    // Update is called once per frame
    void Update()
    {
        //JUMP CODE
        if(grounded) { doubleJumped = false; }
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.Space) && !grounded && !doubleJumped)
        {
            Jump();
            doubleJumped = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            levelManager.RespawnPlayer();
        }

        //LEFT - RIGHT CONTROLS
        if (Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveHoriz(moveSpeed + 10);
            }
            else
            {
                moveHoriz(moveSpeed);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if(Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveHoriz(-moveSpeed - 10);
            }
            else
            {
                moveHoriz(-moveSpeed);
            }
        }

    }
    public void Jump()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
    }

    public void moveHoriz(float moveSpeedVar)
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeedVar, GetComponent<Rigidbody2D>().velocity.y);
    }

}
