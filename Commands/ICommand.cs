namespace Commands
{
    public interface ICommand
    {
        void Execute(IEnumerable<string> args);
    }
}
