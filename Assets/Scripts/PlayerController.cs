using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float moveSpeed = 5f;
    public Vector2 moveDirection;
    public InputActionReference moveAction;
    public Rigidbody rb;
    public float rotationSpeed = 700f;

    [Header("Camera Settings")]
    public Transform player;
    public Transform cameraTransform;
    public float followSpeed = 5f;
    public Vector3 offset;

    [Header("Animations Settings")]
    public Animator anim;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        moveDirection = moveAction.action.ReadValue<Vector2>();

        Vector3 targetPosition = player.position + offset;
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetPosition, followSpeed * Time.deltaTime);

        anim.SetBool("isRun", moveDirection.magnitude > 0);
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(moveDirection.x, 0f, moveDirection.y);
        Vector3 moveVelocity = move.normalized * moveSpeed;

        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);

        if (move.magnitude > 0)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
