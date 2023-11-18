namespace Timeline
{
    public interface IAction
    {
        public void Handle(string[] arguments);
    }
}