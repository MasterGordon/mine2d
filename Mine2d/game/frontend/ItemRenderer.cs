using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.game.core.data;

namespace Mine2d.game.frontend;

public static class ItemRenderer
{
    private static readonly Dictionary<int, (IntPtr, int, int)> Textures = new();

    public static void RenderItemStack(ItemStack stack, Vector2 position, bool renderTooltip = true)
    {
        var texture = stack.GetTexture();
        var renderer = Context.Get().Renderer;
        var uiScale = Context.Get().FrontendGameState.Settings.UiScale;
        renderer.DrawTexture(texture, (int)position.X, (int)position.Y, 16 * uiScale, 16 * uiScale);
        if (stack.Count > 1)
        {
            var (textTexture, width, height) = GetCountTexture(stack.Count);
            renderer.DrawTexture(
                textTexture,
                (int)position.X + (16 * uiScale) - width - (1 * uiScale),
                (int)position.Y + (16 * uiScale) - height + (1 * uiScale),
                width,
                height
            );
        }
        if (renderTooltip)
        {
            var cursorPosition = Context.Get().FrontendGameState.CursorPosition;
            if (cursorPosition.X >= position.X
                && cursorPosition.X <= position.X + (16 * uiScale)
                && cursorPosition.Y >= position.Y
                && cursorPosition.Y <= position.Y + (16 * uiScale)
            )
            {
                Context.Get().FrontendGameState.Tooltip = stack.GetName();
            }
        }
    }

    public static (IntPtr, int, int) GetCountTexture(int count)
    {
        if (Textures.TryGetValue(count, out var value))
        {
            return value;
        }
        var (textTexture, width, height, _) = Context.Get().Renderer.CreateTextTexture("" + count);
        Textures.Add(count, (textTexture, width, height));
        return (textTexture, width, height);
    }
}