using Betty_Eval.Configuration;
using Betty_Eval.Validation;

namespace Betty_Eval.Games
{
    /// <summary>
    /// Represents base slot game experience
    /// </summary>
    public abstract class SlotGame
    {
        private const string LOSE = "No luck this time. Your balance is {0}$";
        private const string WIN = "You won {0}$. Your balance is {1}$";

        protected bool _configured;
        protected SlotGameConfiguration _configuration;
        
        /// <summary>
        /// Position prioritized collection of validation logic. Configure with <see cref="SlotGameBuilder{G}"/>
        /// </summary>
        public ValidatorCollection Validators { get; }

        protected SlotGame()
        {
            Validators = [];
        }

        /// <summary>
        /// Executes single run of the game
        /// </summary>
        /// <param name="bet">Amount to bet</param>
        public virtual void Play(int bet)
        {
            if (!_configured)
                return;

            foreach (var validator in Validators)
            {
                if (!validator.Value.Validate())
                    return;
            }

            Player.Balance -= bet;

            var random = new Random();
            var result = random.Next(0, 101);

            if (result <= _configuration.LoseProbability.Probability)
            {
                Console.WriteLine(string.Format(LOSE, Player.Balance));
                return;
            }
            else
            {
                var sum = _configuration.WinProbabilities.Sum(x => x.Probability);
                result = random.Next(0, sum);
                int previousProbability = 0;

                foreach (var probability in _configuration.WinProbabilities)
                {
                    if (result <= probability.Probability + previousProbability)
                    {
                        int multiplier = random.Next((int)(probability.Low * 100), (int)(probability.High * 100));
                        decimal reward = bet * multiplier / 100;
                        Player.Balance += reward;
                        Console.WriteLine(string.Format(WIN, reward, Player.Balance));
                        break;
                    }
                    else
                    {
                        previousProbability += probability.Probability;
                    }
                }
            }
        }

        /// <summary>
        /// Applies all policies required for the game
        /// </summary>
        public abstract void Configure();

        /// <summary>
        /// Terminates the game
        /// </summary>
        public abstract void Exit();
    }
}
