using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RigidbodyMovement : MonoBehaviour, IShabbaMoveAction
{
    #region private members

    #region components
    private Rigidbody2D rigidBody;
    #endregion

    #region testing flag
    [SerializeField] private bool skipDecceleration = false;
    #endregion

    #region scriptable objects
    [SerializeField] private AngularAccelerationData angularAccelerationData;
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
    [SerializeField] private Vector2 rotateDirection = Vector2.zero;
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

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.drag = initialDrag;
        // set object to look down
        rigidBody.transform.rotation = Quaternion.Euler(0, 0, 180);
    }



    private void FixedUpdate()
    {

    //rigidBody.angularVelocity = ComputeAngularVelocity(rotateDirection);
        moveDirection = Quaternion.Euler(0, 0, ComputeAngularVelocity(rotateDirection) * Time.deltaTime) * moveDirection;

        rigidBody.velocity = moveDirection * rigidBody.velocity.magnitude;

        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);

        // set the direction of the movement to be forward

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

    #region angular acceleration
    private float ComputeAngularVelocity(Vector2 direction)
    {
        // returns the angular velocity vector based on the direction vector and the angular acceleration data
        float x = direction.x;
        float currentAngularVelocity = rigidBody.angularVelocity;
        float targetAngularVelocity = angularAccelerationData.maxAngularVelocity * x;
        float angularAcceleration = angularAccelerationData.angularAcceleration;

        
        // set the angular velocity to 0 if the target angular velocity is 0 or if it has a different sign
        if ( skipDecceleration && (targetAngularVelocity == 0 || currentAngularVelocity * targetAngularVelocity < 0))
        {
            currentAngularVelocity = 0;

        }else{
    
            currentAngularVelocity = Mathf.MoveTowards(currentAngularVelocity, targetAngularVelocity, angularAcceleration * Time.fixedDeltaTime);
        }

        return currentAngularVelocity;

        
    }

    #endregion


    #region action interface
    public void Push(float force = 100f)
    {
        rigidBody.drag = initialDrag;

        // if velocity isn't 0, set move direction to velocity's direction


        // match the moveDirection to the forward direction of the rigidbody's rotation
       // moveDirection = rigidBody.transform.up;


        rigidBody.AddForce(moveDirection * force, ForceMode2D.Impulse);
    }

    public void Rotate(Vector2 direction)
    {
        rotateDirection = direction;
    }

    #endregion


    
}
