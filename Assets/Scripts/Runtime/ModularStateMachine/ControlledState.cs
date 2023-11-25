public abstract class ControlledState<T> : State where T : Controls
{
	public T controls = null;

	public override void InitializeControls(Controls i_controls) => controls = (T)i_controls;

}
