using System;
namespace GXPEngine
{
    public class Background : Sprite
    {
        public Background() : base ("Gray.png")
        {
            y = 300;
            x = 300;
            this.collider.isTrigger = true;
        }
    }
}
