using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Planet : Sprite
    {
        Vec2 pos;
        float gravityRadius;
        float gravityPower;
        public Planet(Vec2 ppos, float rad) : base("triangle.png")
        {
            SetOrigin(width/2, height/2);
            gravityPower = 0.2f;
            pos = ppos;
            SetXY(pos.x, pos.y);
            gravityRadius = rad;
            Sprite oreol = new Sprite("circle.png", false);
            oreol.SetOrigin(oreol.width/2, oreol.height/2);
            oreol.width = (int)gravityRadius;
            oreol.height = (int)gravityRadius;
            oreol.alpha = 0.2f;
            AddChild(oreol);
        }
        void Update()
        {
            
            MyGame myGame = (MyGame)game;
            Player player = myGame.GetPlayer();
            if(player.pos.DistanceTo(pos) <= gravityRadius)
            {
                Vec2 direction = (pos - player.pos).Normalized();
                player.AddVelocity(direction * gravityPower);
            }
        }
    }
}
