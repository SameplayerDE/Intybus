using System;
using System.Collections.Generic;
using System.Linq;

namespace Intybus.DevTools
{
    public static class IntybusCommandExecutorManager
    {
        private static Dictionary<string, IntybusCommandExecutor> _executors;

        static IntybusCommandExecutorManager()
        {
            _executors = new Dictionary<string, IntybusCommandExecutor>();
        }

        public static void Register(string command, IntybusCommandExecutor intybusCommandExecutor)
        {
            _executors[command] = intybusCommandExecutor;
        }
        
        public static void Unregister(string command)
        {
            _executors.Remove(command);
        }

        public static int CheckCommand(IntybusCommand intybusCommand)
        {
            if (intybusCommand.Command.Length == 0)
            {
                return -1;
            }

            foreach (var (command, executor) in _executors)
            {
                if (!command.Equals(intybusCommand.Command) && !executor.Alias.Contains(intybusCommand.Command)) continue;
                executor.Execute(intybusCommand);
                return 0;
            }

            return 1;
        }
    }
}