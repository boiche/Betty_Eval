using Betty_Eval.Validation;

namespace Betty_Eval.Games.Builders
{
    /// <summary>
    /// Defines configuration methods for building <see cref="SlotGame"/>
    /// </summary>
    /// <typeparam name="G">Type of <see cref="SlotGame"/></typeparam>
    public interface ISlotGameBuilder<G> where G : SlotGame
    {
        /// <summary>
        /// Registers <see cref="IValidator"/> as lowest priority.
        /// </summary>
        /// <typeparam name="T">Validator type</typeparam>
        /// <param name="instance">Validator instance</param>
        /// <returns></returns>
        ISlotGameBuilder<G> AddValidator<T>(T instance) where T : IValidator;
       
        /// <summary>
        /// Creates an instance of the configured <see cref="SlotGame"/>
        /// </summary>
        /// <returns></returns>
        SlotGame Build();
    }
}
