namespace Intybus.DevTools.Commands
{
    public class HelpCommandExecutor : IntybusCommandExecutor
    {
        public override void Execute(string command, string[] arguments)
        {
            
        }

        public override void Execute(IntybusCommand intybusCommand)
        {
            if (intybusCommand.ArgumentCount == 0)
            {
                
            }
        }
    }
}