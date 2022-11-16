namespace Mine2d;

public enum Control
{
    Up,
    Down,
    Left,
    Right,
    Stay,
    Confirm,
}

public static class ControlKeyExtension
{
    public static SDL_Keycode Key(this Control c)
    {
        return c switch
        {
            Control.Up => SDL_Keycode.SDLK_w,
            Control.Down => SDL_Keycode.SDLK_s,
            Control.Left => SDL_Keycode.SDLK_a,
            Control.Right => SDL_Keycode.SDLK_d,
            Control.Stay => SDL_Keycode.SDLK_LCTRL,
            Control.Confirm => SDL_Keycode.SDLK_SPACE,
            _ => throw new ArgumentException("Invalid control"),
        };
    }
}
