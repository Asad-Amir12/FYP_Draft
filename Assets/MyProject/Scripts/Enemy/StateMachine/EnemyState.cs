public abstract class EnemyState<T>
{
    protected T Owner;
    public EnemyState(T owner) { Owner = owner; }
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Tick() { }           // called every frame
    public virtual void FixedTick() { }      // called every physics frame
}
