using System.Collections.Generic;

namespace Intybus.DevTools
{
    public class IntybusCommand
    {

        private string _command;
        private List<string> _arguments;
        
        public string Command => _command;
        public string[] Arguments => _arguments.ToArray();
        public int ArgumentCount => _arguments.Count;
        
        public IntybusCommand(string raw)
        {
            var temp = raw.Split(" ");
            _command = temp[0];
            _arguments = new List<string>();
            for (var index = 1; index < temp.Length; index++)
            {
                var argument = temp[index];
                _arguments.Add(argument);
            }
        }
    }
}