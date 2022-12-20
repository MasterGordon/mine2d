using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.engine;

namespace Mine2d.game.frontend.inventory;

public class Inventory : IRenderer
{
    public virtual void Render() { }
    public virtual void OnKeyDown(SDL_Event e) { }
    public virtual void OnClick(SDL_Event e) { }
}