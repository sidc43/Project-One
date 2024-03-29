using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Player attributes")]
    [SerializeField] private float maxHealth;
    public float health;
    public Item activeItem;

    [SerializeField] private InventoryManager inventoryManager;

    [Header("Player physics")]
    [SerializeField] private float maxSpeed;
    public float speed;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    void Update()
    {
        activeItem = inventoryManager.GetActiveItem();
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Normalize the movement vector only if it's not zero
        if (movement != Vector2.zero)
        {
            movement.Normalize();
        }

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    public float GetMaxSpeed() => maxSpeed;
}
