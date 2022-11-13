namespace mine2d.engine.utils;

public class Color
{
    public int R,
        G,
        B,
        A;

    public Color(int r, int g, int b, int a)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }

    public Color(int r, int g, int b)
    {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = 255;
    }

    public SDL_Color ToSdlColor()
    {
        SDL_Color color = new();
        color.r = (byte)this.R;
        color.g = (byte)this.G;
        color.b = (byte)this.B;
        color.a = (byte)this.A;
        return color;
    }
}
