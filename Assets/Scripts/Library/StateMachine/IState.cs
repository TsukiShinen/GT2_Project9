public interface IState<T>
{
    IState<T> Handle(T Entity);
    void Update(T Entity);
    void FixedUpdate(T Entity);
    void Enter(T Entity);
    void Exit(T Entity);
}
