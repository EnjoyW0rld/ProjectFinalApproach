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
        int fuelWidth;

        Sprite health;
        Sprite fuel;
        public HUD(Player pl) : base(1920,1080,false)
        {
            Sprite frame = new Sprite("Assets/UIThingy.png");
            health = new Sprite("Assets/health.png");
            health.x = 192;
            health.y = 120;
            AddChild(health);

            fuel = new Sprite("Assets/fuel.png");
            fuel.x = 192;
            fuel.y = 120;
            fuelWidth = fuel.width;
            AddChild(fuel);

            frame.x += 30;
            frame.y += 30;
            AddChild(frame);
            player = pl;
            transition = new EasyDraw(1920, 1080);
            transition.Clear(0);
            transition.x = -1920;
            AddChild(transition);
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0)) Console.WriteLine(Input.mouseX + "   " + Input.mouseY);
            ClearTransparent();
            Text("Fuel left: " + player.fuelAmount, 10, 200);
            fuel.width = (fuelWidth * PlayerInfo.currentFuelCount) / PlayerInfo.fuelCount;

        }

    }
}
