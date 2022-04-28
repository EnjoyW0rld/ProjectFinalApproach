using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Portal : SpaceBody
    {
        /*Vec2 pos;
        float gravityRadius;
        float mass = 10000 * 1000;
        public new BallCollider ballCollider { get; private set; }
        Sprite body;*/

        public readonly float gateNumber;
        public readonly float facingDirection;
        public new Vec2 pos { get; private set; }
        public new BallCollider ballCollider { get; private set; }

        public Portal(Vec2 ppos,float gRad, float gate,
            float rotate,int planetR,int m,string path) : base(ppos,gRad,planetR,m,path)
        {
            pos = ppos;
            CreateOreol("Assets/violetSphere.png");
            ballCollider = new BallCollider(ppos, planetR);
            AddChild(ballCollider);
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
                DragShip(player, distanceToPlanet);
            }

        }
        public override BallCollider GetCollider()
        {
            return ballCollider;
        }
    }
}
