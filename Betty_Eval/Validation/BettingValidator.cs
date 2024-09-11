namespace Betty_Eval.Validation
{
    /// <summary>
    /// Checks amounts of any bet whether player has sufficient funds to bet
    /// </summary>
    public class BettingValidator : IValidator
    {
        private const string NO_FUNDS = "Insufficient funds. Please deposit. Current balance is {0}$";

        public bool Validate()
        {
            if (CommandContext.RecentCommand.Type != CommandType.Bet)
                return true;

            if (CommandContext.RecentCommand.Parameters[0] > Player.Balance)
            {
                Console.WriteLine(string.Format(NO_FUNDS, Player.Balance));
                return false;
            }

            return true;
        }
    }
}
