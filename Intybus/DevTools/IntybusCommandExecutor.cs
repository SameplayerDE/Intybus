namespace Intybus.DevTools
{
    public abstract class IntybusCommandExecutor
    {
        public string[] Alias;
        public string HelpMessage;
        
        public abstract void Execute(string command, string[] arguments);
        public abstract void Execute(IntybusCommand intybusCommand);
    }
}