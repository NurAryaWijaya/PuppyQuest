using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;

    [Header("Movement")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    [SerializeField] private Animator _animator;

    [Header("Jumping")]
    public float jumpForce = 5f;
    [SerializeField] AudioClip jumpSound;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public float groundWidth = 0.5f;
    [SerializeField] public float groundHeight = 0.1f;
    private bool isGrounded;

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    // For Andorid Input
    public void MoveLeftDown()
    {
        moveInput.x = -1;
    }
    public void MoveRightDown()
    {
        moveInput.x = 1;
    }
    public void MoveButtonUp()
    {
        moveInput.x = 0;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        
        if (context.performed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);            
            _animator.SetBool("isJump", true);
            audioSource.PlayOneShot(jumpSound, 0.08f);
        }
        else if (context.canceled)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

    }
    //For Android Input
    public void JumpButton()
    {
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            _animator.SetBool("isJump", true);
            audioSource.PlayOneShot(jumpSound, 0.08f);
        }
    }


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 boxSize = new Vector2 (groundWidth, groundHeight);
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0f, groundLayer);
        // Reset animasi lompat saat menyentuh tanah
        _animator.SetBool("isJump", !isGrounded);
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);
        _animator.SetBool("isWalking", moveInput.x != 0);

        // Flip karakter berdasarkan arah gerakan
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // menghadap kanan
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // menghadap kiri
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(groundCheck.position, new Vector3(groundWidth, groundHeight, 0));
        }
    }
}
