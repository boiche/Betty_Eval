namespace Betty_Eval.Validation
{
    /// <summary>
    /// Validates the recent <see cref="Command"/>
    /// </summary>
    public class CommandValidator : IValidator
    {
        private const string INVALID_COMMAND = "No such command '{0}'";
        private const string INVALID_PARAM_COUNT = "No command '{0}' takes {1} parameters";
        private const string INVALID_PARAM_VALUE = "Invalid param value";                       
        public bool Validate()
        {
            switch (CommandContext.RecentCommand.Type)
            {
                case CommandType.None:
                    {
                        Console.WriteLine(string.Format(INVALID_COMMAND, CommandContext.RecentCommand.Name));
                        return false;
                    }
                case CommandType.Exit:
                    {
                        if (CommandContext.RecentCommand.Parameters.Length != 0)
                        {
                            Console.WriteLine(string.Format(INVALID_PARAM_COUNT, CommandContext.RecentCommand.Type, CommandContext.RecentCommand.Parameters.Length));
                            return false;
                        }

                        return true;
                    }
                case CommandType.Bet:
                case CommandType.Deposit:
                case CommandType.Withdraw:
                    {
                        if (CommandContext.RecentCommand.Parameters.Length != 1)
                        {
                            Console.WriteLine(string.Format(INVALID_PARAM_COUNT, CommandContext.RecentCommand.Type, CommandContext.RecentCommand.Parameters.Length));
                            return false;
                        }
                        if (!int.TryParse(CommandContext.RecentCommand.Parameters[0].ToString(), out int parameter))
                        {
                            Console.WriteLine(INVALID_PARAM_VALUE);
                            return false;
                        }
                        if (parameter < 1)
                        {
                            Console.WriteLine(INVALID_PARAM_VALUE);
                            return false;
                        }

                        return int.TryParse(CommandContext.RecentCommand.Parameters[0].ToString(), out _);
                    }
                default:
                    return false;
            }
        }
    }
}
