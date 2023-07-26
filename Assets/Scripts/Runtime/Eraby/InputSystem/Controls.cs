//This is the class for handling inputs so that states can recieve simple events for their logic

public abstract class Controls : MonoBehaviourBase
{
	protected bool locked = false;

	#region UNITY

	protected sealed override void Awake()
	{
		base.Awake();
		initInputs();
	}

	private void OnEnable()
	{
		EnableControls();
	}

	private void OnDisable()
	{
		DisableControls();
	}

	private void OnDestroy()
	{
		DisableControls();

		RemoveControls();

		unregisterInputs();
	}

	#endregion

	#region PUBLIC API

	public abstract void RemoveControls();

	public abstract void EnableControls();

	public abstract void DisableControls();

	public void SetLock(bool i_lock)
	{
		locked = i_lock;
	}

	#endregion

	#region PROTECTED

	protected abstract void initInputs();

	protected abstract void unregisterInputs();

	#endregion
}
