using System;
namespace GXPEngine
{
    public class jungleWall : Sprite
    {
        public jungleWall() : base("jungleWall.png")
        {
            y = 300;
            x = 300;
            createCollider();
        }
    }
}
