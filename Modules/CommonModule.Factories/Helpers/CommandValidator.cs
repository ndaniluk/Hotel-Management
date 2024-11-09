﻿using System.Windows.Input;

namespace CommonModule.Factories.Helpers
{
    public static class CommandValidator
    {
        public static (string, string[]) ExtractCommand(string input)
        {
            int braceIndex = input.IndexOf('(');
            if (braceIndex == -1 || !input.EndsWith(")"))
            {
                throw new ArgumentException("Expected format: Command(arg1, arg2, ...).");
            }

            string commandName = input.Substring(0, braceIndex).Trim();
            if (string.IsNullOrWhiteSpace(commandName))
            {
                throw new ArgumentException("Command cannot be empty.");
            }

            var argsPart = input.Substring(braceIndex + 1, input.Length - braceIndex - 2).Trim();
            var args = argsPart.Length > 0 ? argsPart.Split(", ") : [];

            return (commandName, args);
        }
    }
}
