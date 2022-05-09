using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Scene : GameObject
    {

        List<SpaceBody> spaceBodies = new List<SpaceBody>();
        Player player;
        BallCollider[] colliders;
        Vec2 playerStartPos;
        public readonly int sceneNumber;
        EasyDraw HUD;


        public Scene(string mapName)
        {
            HUD = new EasyDraw(1920,1080,false);
            player = new Player();
            //AddChild(new Sprite("Assets/spaceback.png"));
            foreach (GameObject item in MyMapInterpritor.GetGameObjects(mapName))
            {
                spaceBodies.Add(item as SpaceBody);
                AddChild(item);
                if(item is Planet)
                {
                    Planet pl = (Planet)item;
                    if(pl.st == Planet.PlanetState.Start)
                    {
                        playerStartPos = pl.pos;
                        playerStartPos.x += pl.ballCollider.radius + player.ballCollider.radius;
                    }
                }
            }
            AddChild(player);
            player.SetStartPos(playerStartPos);
            AddChild(HUD);
            sceneNumber = MyMapInterpritor.GetLevelNumber(mapName);
        }
        void Update()
        {
            HUD.ClearTransparent();
            HUD.Text("Fuel left: " + player.fuelAmount, 10, 200);
        }
        virtual public Player GetPlayer() => player;
        virtual public Satelite[] GetSatelites() => null;
        virtual public BallCollider[] GetBallColliders()
        {
            if (colliders == null)
            {
                colliders = new BallCollider[spaceBodies.Count];
                for (int i = 0; i < spaceBodies.Count; i++)
                {
                    colliders[i] = spaceBodies[i].GetCollider();
                }
            }
            return colliders;
        }
        virtual public SpaceBody[] GetSpaceBodies() => spaceBodies.ToArray();
    }
}
