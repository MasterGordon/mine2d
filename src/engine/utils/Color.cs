class Color
{
    public int r,
        g,
        b,
        a;

    public Color(int r, int g, int b, int a)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public Color(int r, int g, int b)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = 255;
    }

    public SDL_Color toSDLColor()
    {
        SDL_Color color = new();
        color.r = (byte)r;
        color.g = (byte)g;
        color.b = (byte)b;
        color.a = (byte)a;
        return color;
    }
}
