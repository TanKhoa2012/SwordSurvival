using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rollBoost = 1f;
    public float jumpForce = 10f;

    private float rollTime;
    public float RollTime;
    bool rollOnce = false;

    private float jumpTime;
    public float JumpTime;
    bool jumpOnce = false;

    public float dashBoost;
    public float dashTime;
    private float _dashTime;
    bool isDashing = false;

    public GameObject ghostEffect;
    public float ghostDelaySecond;
    private Coroutine dashEffectCoroutine;

    private Rigidbody2D rb;
    private Animator animator;
    public Transform character;

    public Vector3 moveInput;

    public SpriteRenderer CharacterSR;

    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = character.GetComponent<Animator>();
    }

    

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        
        moveInput.x = Input.GetAxis("Horizontal");
        transform.position += moveInput * moveSpeed * Time.deltaTime;

        animator.SetFloat("Speed", Mathf.Abs(moveInput.x));

        KeepPlayerInBounds();

        if (Input.GetKeyDown(KeyCode.Space) && _dashTime <= 0 && isDashing == false)
        {
            moveSpeed += dashBoost;
            _dashTime = dashTime;
            isDashing = true;
            StartDashEffect();
        }

        if (_dashTime <= 0 && isDashing == true)
        {
            moveSpeed -= dashBoost;
            isDashing = false;
            StopDashEffect();
        }
        else
        {
            _dashTime -= Time.deltaTime;
        }



        if (Input.GetKeyDown(KeyCode.W) && jumpTime <= 0 && !jumpOnce && isGrounded)
        {
            animator.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpTime = JumpTime;
            jumpOnce = true;
        }

        if (jumpTime <= 0 && jumpOnce)
        {
            animator.SetBool("Jump", false);
            jumpOnce = false;
        }
        else
        {
            jumpTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.S) && rollTime <= 0)
        {
            print("Roll");
            animator.SetBool("Roll", true);
            moveSpeed += rollBoost;
            rollTime = RollTime;
            rollOnce = true;
        }

        if (rollTime <= 0 && rollOnce)
        {
            animator.SetBool("Roll", false);
            moveSpeed -= rollBoost;
            rollOnce = false;
        }
        else
        {
            rollTime -= Time.deltaTime;
        }

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)
                CharacterSR.transform.localScale = new Vector3(1, 1, 1);
            else
                CharacterSR.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void KeepPlayerInBounds()
    {
        // Lấy vị trí nhân vật trong hệ toạ độ Viewport (giá trị từ 0 đến 1)
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

        // Giới hạn nhân vật không ra ngoài màn hình (0 <= viewPos.x/y <= 1)
        viewPos.x = Mathf.Clamp(viewPos.x, 0.05f, 0.95f); // Thay đổi giá trị này nếu muốn nhân vật có khoảng cách nhỏ với viền
        viewPos.y = Mathf.Clamp(viewPos.y, 0.05f, 0.95f);

        // Chuyển đổi lại vị trí về toạ độ thế giới
        transform.position = Camera.main.ViewportToWorldPoint(viewPos);
    }

    void StartDashEffect()
    {
        if (dashEffectCoroutine != null) StopCoroutine(dashEffectCoroutine);
        dashEffectCoroutine = StartCoroutine(DashEffectCoroutine());
    }

    void StopDashEffect()
    {
        if (dashEffectCoroutine != null) StopCoroutine(dashEffectCoroutine);
    }

    IEnumerator DashEffectCoroutine()
    {
        while(true)
        {
            GameObject ghost = Instantiate(ghostEffect, transform.position, transform.rotation);
            Sprite currentSprite = CharacterSR.sprite;
            ghost.GetComponentInChildren<SpriteRenderer>().sprite = currentSprite;

            Destroy(ghost, 0.5f);

            yield return new WaitForSeconds(ghostDelaySecond);
        }

    }
}
