using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class BallCollider : GameObject
    {
        public Vec2 pos;
        public readonly float radius;
        public BallCollider(Vec2 p, float r)
        {
             pos = p;
            radius = r;
            //x = p.x;
            //y = p.y;

        }
        void Update()
        {
            GameObject obj = (GameObject)parent;
            pos.x = obj.x;
            pos.y = obj.y;
        }

    }
}
