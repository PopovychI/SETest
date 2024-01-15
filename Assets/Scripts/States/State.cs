using UnityEngine;

public abstract class State  {
    public abstract State RunCurrentState();

    public virtual State RunCurrentStateCustomUpdate() => this;

    public virtual void RunOnStart() { }

    public virtual void RunOnExit() { }
}