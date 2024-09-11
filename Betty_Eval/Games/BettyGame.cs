using Betty_Eval.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace Betty_Eval.Games
{
    /// <summary>
    /// Main game of Betty
    /// </summary>
    public class BettyGame : SlotGame
    {
        private const string GAME_ERORR = "Game ERROR. Please contact your vendor";
        private const string MISCONFIGURATION_ERROR = "{0}: Exception on confiuration. {1}";

        public override void Configure()
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var config = File.ReadAllText(path + @"\Configuration\probabilities.json");
                _configuration = JsonConvert.DeserializeObject<SlotGameConfiguration>(config);
                _configured = true;
            }
            catch (Exception e)
            {
                _configured = false;
                Debug.WriteLine(string.Format(MISCONFIGURATION_ERROR, nameof(BettyGame), e.Message));
                Console.WriteLine(GAME_ERORR);
            }
        }

        public override void Exit()
        {
            Console.WriteLine("Thank you for playing our Betty game.");
        }
    }
}
