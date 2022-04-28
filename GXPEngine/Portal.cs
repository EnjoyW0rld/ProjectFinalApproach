using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Portal : GameObject
    {
        Vec2 pos;
        float gravityRadius;
        float mass = 10000 * 1000;
        public readonly float gateNumber;
        public readonly float facingDirection;
        public new BallCollider ballCollider { get; private set; }
        Sprite body;

        public Portal(Vec2 ppos,float r, float gate, float rotate,string path)
        {
            pos = ppos;
            gravityRadius = r;
            gateNumber = gate;
            facingDirection = rotate;
            rotation = rotate;
            
            //AddChild(ballCollider = new BallCollider(ppos,));
        }
        void Update()
        {
            Scene par = (Scene)parent;
            Player player = par.GetPlayer();
            float distanceToPlanet = player.pos.DistanceTo(pos);
            if (distanceToPlanet <= gravityRadius && distanceToPlanet > ballCollider.radius + player.ballCollider.radius)
            {
                //DragShip(player, distanceToPlanet);
            }

        }
    }
}
