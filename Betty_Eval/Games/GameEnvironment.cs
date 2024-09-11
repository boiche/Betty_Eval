using Betty_Eval.Validation;

namespace Betty_Eval.Games
{
    /// <summary>
    /// Represents current game environment. Provides entry point to each action allowed
    /// </summary>
    public static class GameEnvironment
    {
        private static SlotGame? _loadedSlotGame;
        /// <summary>
        /// Is game terminated
        /// </summary>
        public static bool Terminated { get; private set; }

        /// <summary>
        /// Loads desired <see cref="SlotGame"/> for further actions over it
        /// </summary>
        /// <param name="game"></param>
        public static void LoadGame(SlotGame game)
        {
            _loadedSlotGame = game;
            game.Configure();
        }

        /// <summary>
        /// Starts the <see cref="SlotGame"/>
        /// </summary>
        /// <param name="bet">Amount to bet</param>
        public static void Play(int bet)
        {
            if (!new BettingValidator().Validate())
                return;
            
            _loadedSlotGame?.Play(bet);
        }

        /// <summary>
        /// Terminates the <see cref="SlotGame"/>
        /// </summary>
        public static void Exit()
        {
            _loadedSlotGame?.Exit();
            Terminated = true;
        }

        /// <summary>
        /// Deposits funds to the <see cref="Player"/>'s wallet
        /// </summary>
        /// <param name="amount">Amount to deposit</param>
        internal static void Deposit(int amount)
        {
            Player.Balance += amount;
            Console.WriteLine($"{amount}$ deposited successfully. Your balance is {Player.Balance}$");
        }

        /// <summary>
        /// Withdraws funds from the <see cref="Player"/>'s wallet
        /// </summary>
        /// <param name="amount">Amount to withdraw</param>
        internal static void Withdraw(int amount)
        {
            if (!new WithdrawValidator().Validate())
                return;

            Player.Balance -= amount;
            Console.WriteLine($"{amount}$ withdrawn successfully. Your balance is {Player.Balance}$");
        }
    }
}
