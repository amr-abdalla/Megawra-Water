using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RigidbodyMovement : MonoBehaviour
{
    #region private members

    #region components
    private Rigidbody2D rigidBody;
    #endregion

    #region max values
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float maxDrag = 10f;
    [SerializeField] private float minVelocity = 1f;
    [SerializeField] private float dragIncreasePerSecond = 1f;
    #endregion

    #region initial values
    private Vector2 moveDirection = Vector2.down;
    [SerializeField] private float initialDrag = 0f;
    #endregion

    #region current values
    [SerializeField] private float currentRotation = 0;
    #endregion

    #region input Actions
    [SerializeField] private InputActionReference pushAction;
    [SerializeField] private InputActionReference rotateAction;
	#endregion

	#endregion

	private void Update()
	{
        Debug.DrawRay(transform.position, rigidBody.velocity, Color.red);
	}

	private void Awake()
    {
        InitInput();

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.drag = initialDrag;
    }

    private void InitInput()
    {
        pushAction.action.Enable();
        rotateAction.action.Enable();
        pushAction.action.performed += ctx => PushShabba();
        rotateAction.action.performed += ctx => RotateVelocityVector2(ctx.ReadValue<Vector2>());
        //rotateAction.action.canceled += ctx => RotateVelocity(0);
    }

    private void FixedUpdate()
    {
        // linearly increase drag

        // clamp speed
        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);
        RotateVelocity(currentRotation);
        // if velocity is minimum, set linear drag to initial drag
        if (rigidBody.velocity.magnitude < minVelocity)
        {
            rigidBody.drag = initialDrag;
        }
        else
        {
            rigidBody.drag = Mathf.Clamp(rigidBody.drag + dragIncreasePerSecond * Time.fixedDeltaTime, initialDrag, maxDrag);
        }
    }

    #region rotation

    private void RotateVelocityVector2(Vector2 direction)
    {
       // Debug.Log(direction);
        Debug.Log(Time.deltaTime);
        // get the x component of the direction vector
        // do not rotate if velocity is 0
        if (rigidBody.velocity == Vector2.zero) return;

       // currentRotation = direction.x;

        if(rotationRoutine is not null) StopCoroutine(rotationRoutine);
        rotationRoutine = StartCoroutine(ChangeRotation(direction.x));
    }

    private float easeInOutCubic(float t){
       return t < 0.5f ? 4 * t * t * t : 1 - Mathf.Pow(-2 * t + 2, 3) / 2;
    }
    private Coroutine rotationRoutine;

    IEnumerator ChangeRotation(float targetRotation)
	{
        float t = 0f;
        float duration = 0.1f;
        while(currentRotation != targetRotation)
		{
            // interpolate between current rotation and target rotation, using easeInOutCubic
            
            t += Time.deltaTime / duration;

            currentRotation = Mathf.Lerp(currentRotation, targetRotation, easeInOutCubic(t));

            currentRotation = Mathf.Clamp(currentRotation, -1, 1);

            yield return null;
        }
	}        

    private void RotateVelocity(float angle)
    {
        // rotate move direction
        rigidBody.drag = 1f;
        moveDirection = Quaternion.Euler(0, 0, angle) * moveDirection;
        // set velocity to move direction
        rigidBody.velocity = moveDirection * rigidBody.velocity.magnitude;
        //Debug.Log("RotateVelocity. angle: " + angle + "");

    }

    #endregion

    public void PushShabba(float force = 10f)
    {
        rigidBody.drag = initialDrag;

        // if velocity isn't 0, set move direction to velocity's direction
        if (rigidBody.velocity != Vector2.zero)
        {
            moveDirection = rigidBody.velocity.normalized;
        }

        rigidBody.AddForce(moveDirection * force, ForceMode2D.Impulse);
    }
}
