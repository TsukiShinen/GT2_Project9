public class Goto : IState
{
    public void Handle(Tank tank)
    {
        tank.State = this;
    }

    public void Update()
    {

    }
}