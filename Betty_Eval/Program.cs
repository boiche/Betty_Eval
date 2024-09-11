using Betty_Eval;
using Betty_Eval.Games;
using Betty_Eval.Validation;

var builder = new SlotGameBuilder<BettyGame>();
builder
    .AddValidator(new RestValidator(20));

GameEnvironment.LoadGame(builder.Build());

while (true)
{
    CommandContext.ReadCommand();

    CommandContext.ExecuteCommand();
    if (GameEnvironment.Terminated)
        break;
}