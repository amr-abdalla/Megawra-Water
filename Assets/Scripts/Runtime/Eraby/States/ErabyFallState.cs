// using Cinemachine;
using System.Collections;
using UnityEngine;

public class ErabyFallState : MoveHorizontalAbstractState
{
    [SerializeField]
    private float timeBeforeBounce = 0.5f;

    [SerializeField]
    private PersistentErabyData persistentData = null;

    [Header("Extra Configs")]
    [SerializeField]
    ErabyBounceState bounceState = null;

    [SerializeField]
    BounceTapManager tapManager = null;
    protected Coroutine landingRoutine = null;

    private float resetDiveVelocityY = 0f;

    private bool isGliding = false;

    #region STATE API
    protected override void onStateInit() { }

    float startTime;

    protected override void onStateEnter()
    {
        // Debug.Log("Enter fall");
        startTime = Time.time;
        tapManager.ResetTap();
        controls.DiveStarted += goToFastFall;
        controls.DiveReleased += onFastFallCancel;
        controls.JumpPressed += onGlide;
        controls.JumpReleased += onGlideCancel;

        if (controls.isDiving())
            goToFastFall();

        if (controls.isJumping())
            isGliding = true;

        base.onStateEnter();
        controls.EnableControls();
    }

    protected override void onStateUpdate()
    {
        if (true == body.IsGrounded && body.CurrentGroundTransform != null)
        {
            if (landingRoutine == null)
                landingRoutine = StartCoroutine(
                    landingSequence(body.CurrentGroundTransform.gameObject.tag)
                );
        }
    }

    protected override void onStateExit()
    {
        // Debug.Log("TIME of Fall " + (Time.time - startTime));
        controls.DiveStarted -= goToFastFall;
        controls.DiveReleased -= onFastFallCancel;
        controls.JumpPressed -= onGlide;
        controls.JumpReleased -= onGlideCancel;
        this.DisposeCoroutine(ref landingRoutine);
        base.onStateExit();
    }

    protected override void onStateFixedUpdate()
    {
        base.onStateFixedUpdate();
        if (isGliding)
        {
            float newVelocityY =
                body.VelocityY + accelerationData.GlideDecelerationY * Time.fixedDeltaTime;
            body.SetVelocityY(newVelocityY);
        }
    }

    public override void ResetState()
    {
        base.ResetState();
        onStateExit();
    }
    #endregion

    #region PRIVATE
    private IEnumerator landingSequence(string i_tag)
    {
        // Debug.Log("Landing sequence");

        persistentData.landingVelocityX = body.VelocityX;

        body.SetVelocityX(0);
        body.SetVelocityY(0);
        controls.DisableControls();

        HaraPlatformAbstract platform = getCollidedPlatformComponent();

        // sfx suggestion: impact sound
        // impactSFX?.Play();

        yield return this.Wait(timeBeforeBounce);

        if (platform != null)
            platform.onCollision();

        if (i_tag == "Bouncy")
        {
            bounceState.SetFallPlatform(platform);
            setState<ErabyBounceState>();
        }
        // else if (i_tag == "Respawn")
        // {
        //     Debug.LogError("ENTER CELEBRATION");
        //     setState<HarrankashCelebrationState>();

        // }
        else
            setState<ErabyCrashState>();

        this.DisposeCoroutine(ref landingRoutine);
    }
    #endregion

    #region UTILITY
    private HaraPlatformAbstract getCollidedPlatformComponent()
    {
        //Debug.LogError("Get component fall");
        HaraPlatformAbstract collidedPlatform =
            body.CurrentGroundTransform.gameObject.GetComponentInParent<HaraPlatformAbstract>();

        if (collidedPlatform == null)
            collidedPlatform =
                body.CurrentGroundTransform.gameObject.GetComponent<HaraPlatformAbstract>();

        if (collidedPlatform == null)
            collidedPlatform =
                body.CurrentGroundTransform.gameObject.GetComponentInChildren<HaraPlatformAbstract>();

        return collidedPlatform;
    }
    #endregion

    void goToFastFall()
    {
        isGliding = false;
        // controls.DisableMove();
        resetDiveVelocityY = body.VelocityY > 0 ? 0 : body.VelocityY;
        // body.SetVelocityX(0);
        body.SetVelocityY(-accelerationData.DiveVelocityY);
    }

    void onFastFallCancel()
    {
        // body.SetVelocityX(initialVelocityX);
        // controls.EnableMove();
        // body.SetVelocityY(resetDiveVelocityY);
    }

    void onGlide()
    {
        isGliding = true;
    }

    void onGlideCancel()
    {
        isGliding = false;
    }
}
