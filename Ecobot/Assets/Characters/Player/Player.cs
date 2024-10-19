using System;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    EntityStats stats;
    private GameObject bullet;
    private Rigidbody2D bulletRB;
    private Vector2 direction, bulletInitPos;
    public float jumpForce;
    public bool isGrounded;
    public Transform groundCheck;
    private Vector2 force;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        stats = GetComponent<EntityStats>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
        Jump();
        Attack();
    }

    #region Movement
    void Move()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.linearVelocity = new Vector2(moveInput * stats.maxVelocity, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(moveInput * stats.velocity, rb.linearVelocity.y);
        }
    }

    void Jump()
    {
        force = new Vector2(0, 1);
        force = force.normalized;
        if (isGrounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown("w") || Input.GetKeyDown("up")))
        {
            rb.AddForce(force * stats.jumpForce, ForceMode2D.Impulse);
            rb.gravityScale = 1f;
        }
    }
    #endregion

    #region Attack
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bullet = ProjectileManager.Instance.GetProjectile();
            bulletRB = bullet.GetComponent<Rigidbody2D>();

            if (bullet != null)
            {
                // Define a posição inicial do projétil
                bulletInitPos = rb.position;
                bullet.transform.position = bulletInitPos;

                // Converte a posição do mouse para o espaço do mundo
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0; // Certifica-se de que a posição Z está correta

                // Calcula a direção da bala em relação ao mouse
                Vector2 direction = ((Vector2)mousePosition - bulletInitPos).normalized;

                // Ativa o projétil e aplica força na direção do mouse
                bullet.SetActive(true);
                bulletRB.AddForce(direction * stats.bulletSpeed, ForceMode2D.Impulse); // stats.bulletSpeed define a velocidade do projétil
            }
        }
    }
    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = true;
            rb.gravityScale = 1;
            Debug.Log("Grounded");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
            Debug.Log("Not Grounded");
        }
    }
    #endregion
}
