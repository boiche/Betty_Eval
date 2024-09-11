using Betty_Eval.Games.Builders;
using Betty_Eval.Validation;

namespace Betty_Eval.Games
{
    /// <summary>
    /// Manages slot game behaviour and validation
    /// </summary>
    /// <typeparam name="G"></typeparam>
    public class SlotGameBuilder<G> : ISlotGameBuilder<G> where G : SlotGame
    {
        private readonly SlotGame _slotGame;

        public SlotGameBuilder() 
        {
            _slotGame = Activator.CreateInstance<G>();
        }

        public ISlotGameBuilder<G> AddValidator<T>(T instance) where T : IValidator
        {
            var prop = _slotGame.GetType().GetProperty("Validators")?.GetValue(_slotGame) as ValidatorCollection;
            prop?.Add(typeof(T), instance);

            return this;
        }     
        
        public SlotGame Build()
        {
            return _slotGame;
        }
    }
}
