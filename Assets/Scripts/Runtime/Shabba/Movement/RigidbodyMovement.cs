using UnityEngine;

public class RigidbodyMovement : MonoBehaviourBase, IShabbaMoveAction
{
    [SerializeField] private AngularAccelerationData angularAccelerationData;
    // Code review : create a curve settings scriptable object instead

    [SerializeField] AnimationCurve dragEvolutionCurve = null;

    [Header("Max values")]
    [SerializeField] private float minSpeed = 1f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float maxDrag = 10f;
    [SerializeField] private float timeToMaxDrag = 1f;

    [Header("Initial values")]
    [SerializeField] private float initialDrag = 0f;

    private Rigidbody2D rigidBody;

    // Runtime variables
    private float currentDragTimer = 0f;
    float currentAngularVelocity = 0f;
    private Vector2 rotateDirection = Vector2.zero;
    private Vector2 moveDirection = Vector2.down;

    #region UNITY

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        resetToInitialState();
    }

    private void Update()
	{
        Debug.DrawRay(transform.position, rigidBody.velocity, Color.red);
	}

    void OnDisable()
    {
        resetToInitialState();
    }

    private void FixedUpdate()
    {
        float deltaAngle = ComputeAngularVelocity(rotateDirection.x) * Time.deltaTime;
        moveDirection = Quaternion.Euler(0, 0, -deltaAngle) * moveDirection;

        rigidBody.velocity = moveDirection * rigidBody.velocity.magnitude;

        rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxSpeed);

        // set the direction of the movement to be forward

        // if velocity is minimum, set linear drag to initial drag
        if (rigidBody.velocity.magnitude >= minSpeed)
        {
            applyDrag();
        }
        else
        {
            onDidStopMoving();
        }
    }

    #endregion


    #region PUBLIC API

    [ExposePublicMethod]
    public void Push(float force = 100f)
    {
        rigidBody.drag = initialDrag;

        // if velocity isn't 0, set move direction to velocity's direction


        // match the moveDirection to the forward direction of the rigidbody's rotation
        // moveDirection = rigidBody.transform.up;


        rigidBody.AddForce(moveDirection * force, ForceMode2D.Impulse);
    }

    [ExposePublicMethod]
    public void ResetToInitialState()
    {
        resetToInitialState();
    }

    [ExposePublicMethod]
    public void Rotate(Vector2 direction)
    {
        rotateDirection = direction;
    }

    public Vector2 CurrentVelocity => null == rigidBody ? MathConstants.VECTOR_2_ZERO : rigidBody.velocity;

    #endregion

    #region PRIVATE

    void onDidStopMoving()
    {
        // Code review : this could trigger an event to tell all listeners 
        // that object stopped moving (+ start whataver behaviour should happen in that case)
    }

    void resetToInitialState()
    {
        currentDragTimer = 0f;
        currentAngularVelocity = 0f;

        if (null != rigidBody)
        {
            rigidBody.drag = initialDrag;
            // set object to look down
            rigidBody.transform.rotation = Quaternion.Euler(0, 0, 180);
            rigidBody.velocity = MathConstants.VECTOR_2_ZERO;
        }
    }

    void applyDrag()
    {
        float tDrag = currentDragTimer / timeToMaxDrag;
        float tEval = dragEvolutionCurve.Evaluate(tDrag);
        rigidBody.drag = Mathf.Lerp(rigidBody.drag, maxDrag, tEval);

        currentDragTimer += Time.fixedDeltaTime;
        currentDragTimer = Mathf.Clamp(currentDragTimer, 0f, timeToMaxDrag);
    }

    private float ComputeAngularVelocity(float i_angleSign)
    {
        if (i_angleSign == 0f)
            currentAngularVelocity = descelerate(currentAngularVelocity, angularAccelerationData.angularDesceleration);
        else
            currentAngularVelocity = accelerate(i_angleSign, currentAngularVelocity, angularAccelerationData.maxAngularVelocity, angularAccelerationData.angularAcceleration);

        return currentAngularVelocity;
    }

    // Code review : externalize these 2 functions into a static class (AccelerationUtility)
    float accelerate(float i_sign, float i_speed, float i_maxSpeed, float i_acceleration)
    {
        float deltaVel = i_acceleration * Time.fixedDeltaTime * i_sign;
        i_speed += deltaVel;

        i_speed = i_sign > 0 ?
            Mathf.Clamp(i_speed, 0, i_maxSpeed) :
            Mathf.Clamp(i_speed, -i_maxSpeed, 0);

        return i_speed;
    }

    float descelerate(float i_speed, float i_desceleration)
    {
        if (i_speed == 0f) return 0f;

        float deltaVel = i_desceleration * Time.fixedDeltaTime;

        if (i_speed < 0)
        {
            i_speed += deltaVel;
            if (i_speed > 0) i_speed = 0f;
        }
        else
        {
            i_speed -= deltaVel;
            if (i_speed < 0) i_speed = 0f;
        }

        return i_speed;
    }
    #endregion
}
