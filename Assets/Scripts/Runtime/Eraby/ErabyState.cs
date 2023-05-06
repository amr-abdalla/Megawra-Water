public class ErabyState : IState
{
    public StateAction EnterActions { get; set; }
    public StateAction ExitActions { get; set; }
    public StateAction UpdateActions { get; set; }

    public ErabyState(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void Enter()
    {
        EnterActions();
    }

    public void Exit()
    {
        ExitActions();
    }

    public void Update()
    {
        UpdateActions();
    }
}
