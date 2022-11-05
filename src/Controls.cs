using static SDL2.SDL;

enum Control
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    STAY,
    CONFIRM,
}

static class ControlKeyExtension
{
    public static SDL_Keycode Key(this Control c)
    {
        switch (c)
        {
            case Control.UP:
                return SDL_Keycode.SDLK_w;
            case Control.DOWN:
                return SDL_Keycode.SDLK_s;
            case Control.LEFT:
                return SDL_Keycode.SDLK_a;
            case Control.RIGHT:
                return SDL_Keycode.SDLK_d;
            case Control.STAY:
                return SDL_Keycode.SDLK_LCTRL;
            case Control.CONFIRM:
                return SDL_Keycode.SDLK_SPACE;
            default:
                throw new ArgumentException("Invalid control");
        }
    }
}
