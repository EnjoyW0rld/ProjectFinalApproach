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
        public readonly BallCollider collisionObject;
        /// <summary>
        /// Information of the collision
        /// </summary>
        /// <param name="n">Collision normal</param>
        /// <param name="toi">Collison time of impact</param>
        public CollisionInfo(Vec2 n, float toi, BallCollider collObj = null)
        {
            TOI = toi;
            normal = n;
            collisionObject = collObj;
        }
    }
}
