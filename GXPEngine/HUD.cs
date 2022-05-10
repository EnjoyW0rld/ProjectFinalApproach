using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class HUD : EasyDraw
    {
        Player player;
        EasyDraw transition;
        bool isTransiting;
        int nextScene;
        public HUD(Player pl) : base(1920,1080,false)
        {
            player = pl;
            transition = new EasyDraw(1920, 1080);
            transition.Clear(0);
            transition.x = -1920;
            AddChild(transition);
        }
        void Update()
        {
            
            ClearTransparent();
            Text("Fuel left: " + player.fuelAmount, 10, 200);

        }

    }
}
