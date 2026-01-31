using UnityEngine;

public class Player2movement : MonoBehaviour
{
    private float horizontal;
    private float speed = 9f;
    //private float jumpingPower = -750f;
    private float jumpingPower = 750f;
    private bool isFacingRight = true;

    private bool isWallSliding;
    private float wallSlidingSpeed = 2f;

    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    //private Vector2 wallJumpingPower = new Vector2(8f, -16f);
    private Vector2 wallJumpingPower = new Vector2(8f, 16f);

   private InputControl inputControl;

    

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private LayerMask wallLayer;
    
   


    GameObject inputManager; 
    GameObject heavierPlayer;
    private void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("InputManager");

         inputControl = FindAnyObjectByType<InputControl>();
         

    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal2");

        if (Input.GetButtonDown("Jump2") && IsGrounded())
        {
            //rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            rb.AddForce(transform.up*jumpingPower);
        }

        if (Input.GetButtonUp("Jump2") && rb.velocity.y < 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        WallSlide();
        
        if (inputControl.isConnected == false)
        {
        WallJump();
        }

        if (!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (!inputManager.GetComponent<InputControl>().isConnected)
        {
            if (!isWallJumping)
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }
        }
        //else
        {
            //inputManager.GetComponent<InputControl>().heavierPlayer.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontal * speed, rb.velocity.y));
        }


    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, wallLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && horizontal != 0f)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump2") && wallJumpingCounter > 0f)
        {
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}