namespace Betty_Eval.Validation
{
    /// <summary>
    /// Checks amounts of any withdraw whether player has enough funds to withdraw
    /// </summary>
    public class WithdrawValidator : IValidator
    {
        public bool Validate()
        {
            if (CommandContext.RecentCommand.Type != CommandType.Withdraw)
                return true;

            if (CommandContext.RecentCommand.Parameters[0] > Player.Balance)
            {
                Console.WriteLine($"Insufficient funds. Please deposit. Current balance is {Player.Balance:F2}$");
                return false;
            }

            return true;
        }
    }
}
