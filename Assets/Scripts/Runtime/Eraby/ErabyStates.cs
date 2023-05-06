using static Eraby.ErabyConstants;

public class JumpingState : IState
{
    public string Name { get; } = JUMPING;

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}

public class FlyingState : IState
{
    public string Name { get; } = FLYING;

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}

public class FallingState : IState
{
    public string Name { get; } = FALLING;

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}

public class DivingState : IState
{
    public string Name { get; } = DIVING;

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}

public class BouncingState : IState
{
    public string Name { get; } = BOUNCING;

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}

public class CrashingState : IState
{
    public string Name { get; } = CRASHING;

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}

public class LosingState : IState
{
    public string Name { get; } = LOSING;

    public void Enter() { }

    public void Exit() { }

    public void Update() { }
}
