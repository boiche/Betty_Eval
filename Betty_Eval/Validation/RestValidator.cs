namespace Betty_Eval.Validation
{
    /// <summary>
    /// Monitors player's playing experience. Suggests terminating the game after a while
    /// </summary>
    public class RestValidator : IValidator
    {
        private int _betCount;
        public RestValidator(int betCount)
        {
            _betCount = betCount;
        }

        public bool Validate()
        {
            _betCount--;
            if (_betCount <= 0)
            {
                Console.WriteLine("Played too much. Consider taking a break.");
                //return false;
            }

            return true;
        }
    }
}