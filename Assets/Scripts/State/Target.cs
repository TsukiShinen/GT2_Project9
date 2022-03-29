public class Target : IState
{
    public void Handle(Tank tank)
    {
        tank.State = this;
    }

    public void Update()
    {

    }
}