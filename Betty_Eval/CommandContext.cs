using Betty_Eval.Games;
using Betty_Eval.Validation;

namespace Betty_Eval
{
    /// <summary>
    /// Represents single slot game command
    /// </summary>
    /// <param name="Name">Typed name of the command</param>
    /// <param name="Type">Registerd type of the command</param>
    /// <param name="Parameters">Collection of integers</param>
    public record Command(string Name, CommandType Type, params int[] Parameters);
    public static class CommandContext
    {
        private static readonly Dictionary<CommandType, Delegate> _commands;
        private static Command _recentCommand;        
        private static readonly CommandValidator _commandValidator;

        /// <summary>
        /// Contains the last typed command
        /// </summary>
        public static Command RecentCommand { get => _recentCommand; }

        static CommandContext()
        {
            _commands = new Dictionary<CommandType, Delegate>()
            {
                { CommandType.Exit, () => { GameEnvironment.Exit(); } },
                { CommandType.Bet, (int amount) => { GameEnvironment.Play(amount); } },
                { CommandType.Deposit, (int amount) => { GameEnvironment.Deposit(amount); } },
                { CommandType.Withdraw, (int amount) => { GameEnvironment.Withdraw(amount); } }
            };
            _commandValidator = new CommandValidator();
            _recentCommand = new(string.Empty, CommandType.None, []);
        }

        /// <summary>
        /// Prompts user to input new command
        /// </summary>
        public static void ReadCommand()
        {
            _recentCommand = FormatCommand(Console.ReadLine());            
        }        

        /// <summary>
        /// Executes recent <see cref="Command"/>
        /// </summary>
        public static void ExecuteCommand()
        {
            if (!_commandValidator.Validate())
                return;

            if (_recentCommand.Parameters.Length > 0)
                _commands[_recentCommand.Type].DynamicInvoke(_recentCommand.Parameters[0]);
            else
                _commands[_recentCommand.Type].DynamicInvoke();
        }

        private static Command FormatCommand(string? input)
        {
            if (input == null)
                return new(string.Empty, CommandType.None, []);
            if (input.Length == 0)
                return new(string.Empty, CommandType.None, []);
            
            var command = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            Enum.TryParse(command[0], true, out CommandType type);

            return new(command[0], type, command[1..].Select(x =>
            {
                int.TryParse(x, out int result);
                return result;
            }).ToArray());
        }
    }
}
