namespace mine2d;

enum Control
{
    Up,
    Down,
    Left,
    Right,
    Stay,
    Confirm,
}

static class ControlKeyExtension
{
    public static SDL_Keycode Key(this Control c)
    {
        switch (c)
        {
            case Control.Up:
                return SDL_Keycode.SDLK_w;
            case Control.Down:
                return SDL_Keycode.SDLK_s;
            case Control.Left:
                return SDL_Keycode.SDLK_a;
            case Control.Right:
                return SDL_Keycode.SDLK_d;
            case Control.Stay:
                return SDL_Keycode.SDLK_LCTRL;
            case Control.Confirm:
                return SDL_Keycode.SDLK_SPACE;
            default:
                throw new ArgumentException("Invalid control");
        }
    }
}