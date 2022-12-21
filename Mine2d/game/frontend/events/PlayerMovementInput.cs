using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mine2d.engine.system;
using Mine2d.engine.system.annotations;
using Mine2d.game.backend.network.packets;

namespace Mine2d.game.frontend.events
{
    public class PlayerMovementInput
    {
        [EventListener(EventType.KeyDown)]
        public static void onKeyDown(SDL_Event e)
        {
            if(e.key.keysym.sym == SDL_Keycode.SDLK_SPACE) {
                Context.Get().Backend.ProcessPacket(new JumpPacket {
                    PlayerId = Context.Get().FrontendGameState.PlayerGuid
                });
            }
        }
    }
}