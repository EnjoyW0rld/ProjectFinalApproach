using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{

    internal class CollisionInfo
    {
        public readonly Vec2 normal;
        public readonly float TOI;
        public readonly float radius;
        /// <summary>
        /// Information of the collision
        /// </summary>
        /// <param name="n">Collision normal</param>
        /// <param name="toi">Collison time of impact</param>
        public CollisionInfo(Vec2 n, float toi, float r)
        {
            TOI = toi;
            normal = n;
            radius = r;
        }
    }
}
