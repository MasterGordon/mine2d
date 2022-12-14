using Mine2d.game.core.data;
using Mine2d.game.state;

namespace Mine2d.game.core.items;

public class Item
{
    public ItemId Id { get; set; }
    public string Name { get; set; }
    private readonly IntPtr texture;

    public Item(ItemId id, string name, string[] textureName)
    {
        this.Id = id;
        this.Name = name;
        this.texture = Context.Get().TextureFactory.CreateTexture(textureName);
    }

    public Item(ItemId id, string name, string textureName)
    {
        this.Id = id;
        this.Name = name;
        this.texture = Context.Get().TextureFactory.LoadTexture(textureName);
    }

    public void Render(Vector2 position)
    {
        var ctx = Context.Get();
        var renderer = ctx.Renderer;
        var scale = ctx.FrontendGameState.Settings.GameScale;
        var targetPos = ((position - ctx.FrontendGameState.Camera.Position) * scale) -
            new Vector2(4 * scale, 6 * scale);
        renderer.DrawTexture(this.texture, (int)targetPos.X, (int)targetPos.Y, 8 * scale, 8 * scale);
    }

    public IntPtr GetTexture()
    {
        return this.texture;
    }

    public virtual void Interact(ItemStack stack, Vector2 position, Player player) {
    }

    public virtual ItemKind GetKind()
    {
        return ItemKind.Default;
    }
}
