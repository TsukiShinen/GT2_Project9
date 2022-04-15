public interface IState<in T>
{
    IState<T> Handle(T entity);
    void Update(T entity);
    void FixedUpdate(T entity);
    void Enter(T entity);
    void Exit(T entity);
}
