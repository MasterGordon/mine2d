using mine2d.core.tiles;

namespace mine2d.core.data;

public struct STile
{
    public int Id { get; set; }
    public int Hits { get; set; }

    public static STile From(int id)
    {
        return new STile()
        {
            Id = id
        };
    }

    public static STile From(Tiles id)
    {
        return From((int)id);
    }
}
