using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;    

public class RigidbodyMovement : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 10f;
    private Vector2 moveDirection = Vector2.down;
    // Start is called before the first frame update
    [SerializeField] private float initialDrag = 0f;
    [SerializeField] private float maxDrag = 10f;
    [SerializeField] private float dragIncreasePerSecond = 1f;
    private float currentDrag = 0f;
    private Rigidbody2D rb;

    [SerializeField] private float minVelocity = 1f;
    [SerializeField] private InputActionReference pushAction;
    [SerializeField] private InputActionReference rotateAction;
    private float rotation = 0; 
    private void Awake()
    {
        pushAction.action.Enable();
        rotateAction.action.Enable();
        pushAction.action.performed += ctx => PushShabba();
        rotateAction.action.performed += ctx => RotateVelocityVector2(ctx.ReadValue<Vector2>());
        rotateAction.action.canceled += ctx => RotateVelocity(0);
        rb = GetComponent<Rigidbody2D>();
        currentDrag = initialDrag;
        rb.drag = currentDrag;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate() {
        // linearly increase drag
        
        // clamp speed
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        RotateVelocity(rotation);
        // if velocity is minimum, set linear drag to initial drag
        if (rb.velocity.magnitude < minVelocity) {
            currentDrag = initialDrag;
            rb.drag = currentDrag;
        }
        else {
            currentDrag = Mathf.Clamp(currentDrag + dragIncreasePerSecond * Time.fixedDeltaTime, initialDrag, maxDrag);
            rb.drag = currentDrag;
        }
    }


    private void RotateVelocityVector2(Vector2 direction) {
        // get the x component of the direction vector
        // do not rotate if velocity is 0
        if (rb.velocity == Vector2.zero) return;
        rotation = direction.x;

    }

    private void RotateVelocity(float angle) {
     
        // rotate move direction
        moveDirection = Quaternion.Euler(0, 0, angle) * moveDirection;
        // set the move direction to down if angle is 0
        if (angle == 0) moveDirection = Vector2.down;
        // set velocity to move direction
        rb.velocity = moveDirection * rb.velocity.magnitude;
        Debug.Log("RotateVelocity. angle: " + angle + "");

    }
      
    public void PushShabba(float force = 10f) {
        currentDrag = initialDrag;
        // if velocity isn't 0, set move direction to velocity's direction
        if (rb.velocity != Vector2.zero) {
            moveDirection = rb.velocity.normalized;
        }
        rb.AddForce(moveDirection * force, ForceMode2D.Impulse);
    }

    
}
