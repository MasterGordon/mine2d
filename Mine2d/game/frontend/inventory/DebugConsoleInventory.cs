namespace Mine2d.game.frontend.inventory;

public class DebugConsoleInventory : Inventory
{
    public override void Render()
    {
        var ctx = Context.Get();
        const int uiScale = 1;
        var windowHeight = ctx.FrontendGameState.WindowHeight;
        var history = ctx.FrontendGameState.DebugState.Messages;
        var consoleInput = "> " + ctx.FrontendGameState.DebugState.ConsoleInput;
        (nint texture, int width, int height, nint surfaceMessage) historyTex = default;
        if (history.Count > 0)
        {
            historyTex = ctx.Renderer.CreateTextTexture(string.Join("\n", history));
        }
        var inputTex = ctx.Renderer.CreateTextTexture(consoleInput);
        if (history.Count > 0)
        {
            ctx.Renderer.DrawTexture(historyTex.texture, 0, windowHeight - (inputTex.height * uiScale) - (historyTex.height * uiScale), historyTex.width * uiScale, historyTex.height * uiScale);
        }
        ctx.Renderer.DrawTexture(inputTex.texture, 0, windowHeight - (inputTex.height * uiScale), inputTex.width * uiScale, inputTex.height * uiScale);

        SDL_DestroyTexture(inputTex.texture);
        SDL_DestroyTexture(historyTex.texture);
        SDL_FreeSurface(historyTex.surfaceMessage);
        SDL_FreeSurface(inputTex.surfaceMessage);
    }
}