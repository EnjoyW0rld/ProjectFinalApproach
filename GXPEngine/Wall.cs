using System;
namespace GXPEngine
{
    public class Wall : Sprite
    {
        public Wall() : base ("Wall.png")
        {
            y = 300;
            x = 300;
            createCollider();
        }
    }
}
