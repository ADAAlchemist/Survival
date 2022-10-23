using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    private float dirY = 0f;

    [SerializeField] private Transform target;
    [SerializeField] private float moveSpeed = 2.5f;
    [SerializeField] private float senseDistance = 10f;

    private enum MovementState
    {
        idle,
        running
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        rb.transform.position = Vector2.MoveTowards(rb.transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        // Set velocity to 0 in order to avoid player pushing the zombie
        rb.velocity = new Vector2(0,0);

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX != 0f || dirY != 0f)
        {
            state = MovementState.running;
        }
        else
        {
            state = MovementState.idle;
        }

        anim.SetInteger("state", ((int)state));
    }
}
