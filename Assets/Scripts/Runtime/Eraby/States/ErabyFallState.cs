// using Cinemachine;
using System.Collections;
using UnityEngine;

public class ErabyFallState : FallAbstractState
{
    [SerializeField]
    private AudioSource impactSFX = null;

    [SerializeField]
    private float timeBeforeBounce = 0.5f;

    [SerializeField]
    private TrailRenderer trail = null;

    // [Header("Camera")]
    // [SerializeField]
    // VCamSwitcher vCamSwitcher = null;

    // [SerializeField]
    // CinemachineVirtualCamera nearCam = null;

    [Header("Extra Configs")]
    [SerializeField]
    HarrankashPlatformEventDispatcher eventDispatcher = null;

    [SerializeField]
    ErabyBounceState bounceState = null;

    protected Coroutine landingRoutine = null;

    private bool firstFall = true;

    #region STATE API
    protected override void onStateInit() { }

    float startTime;

    protected override void onStateEnter()
    {
        // Debug.Log("Enter fall");
        startTime = Time.time;
        controls.DiveStarted += goToFastFall;
        // body.SetVelocityY(0);
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
        this.DisposeCoroutine(ref landingRoutine);
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
        body.SetVelocityX(0);
        body.SetVelocityY(0);
        controls.DisableControls();

        HaraPlatformAbstract platform = getCollidedPlatformComponent();

        // sfx suggestion: impact sound
        // impactSFX?.Play();

        yield return this.Wait(timeBeforeBounce);

        if (platform != null)
            platform.onCollision();

        firstFall = false;
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
        setState<ErabyDiveState>();
    }
}
