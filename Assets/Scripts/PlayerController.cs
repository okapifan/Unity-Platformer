using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Player Components
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;

    // Player Movement
    private float movementInputDirection;
    private bool isFacingRight = true;
    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    private bool canJump;

    // Player Collision
    public bool isGrounded;
    public Transform groundCheck;
    public float groundCheckRadius = 0.25f;
    public LayerMask whatIsGround;

    // Animations
    private bool isRunning = false;

    // DataManager for save
    public DataManager dataManager;

    // Player label
    public string playerName = "Player";
    public float labelLocation = 1f;
    private GameObject playerLabel;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //Get Save
        dataManager.load();
        playerName = dataManager.data.name;
        //playerBaseSprite.color = dataManager.data.color;

        playerLabel = new GameObject("player_label");
        playerLabel.transform.rotation = Camera.main.transform.rotation; // Causes the text faces camera.
        TextMesh playerLabelText = playerLabel.AddComponent<TextMesh>();
        playerLabelText.text = dataManager.data.name ?? "Player";
        playerLabelText.color = new Color(0.8f, 0.8f, 0.8f);
        playerLabelText.fontStyle = FontStyle.Bold;
        playerLabelText.alignment = TextAlignment.Center;
        playerLabelText.anchor = TextAnchor.MiddleCenter;
        playerLabelText.characterSize = 0.065f;
        playerLabelText.fontSize = 60;
        playerLabel.transform.SetParent(gameObject.transform);
        playerLabel.transform.position = gameObject.transform.position + Vector3.up * labelLocation;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        canJump = (isGrounded)  ?  true: false;
    }

    private void CheckMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        isRunning = (rb.velocity.x != 0) ? true : false;
    }

    private void UpdateAnimations()
    {
        anim.SetBool("isRunning", isRunning);
    }

    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void Jump()
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void ApplyMovement()
    {
        rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        if (!isFacingRight)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
