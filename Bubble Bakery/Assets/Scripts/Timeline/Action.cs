namespace Timeline
{
    public interface IAction
    {
        public abstract void Handle(string[] arguments);
    }
}