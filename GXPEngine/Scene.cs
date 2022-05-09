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
        int sceneNumber;

        public Scene(string mapName)
        {

            foreach (GameObject item in MyMapInterpritor.GetGameObjects(mapName))
            {
                spaceBodies.Add(item as SpaceBody);
                AddChild(item);
            }
            AddChild(player = new Player());
            sceneNumber = MyMapInterpritor.GetLevelNumber(mapName);
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
        virtual public SpaceBody[] GetSpaceBodies() => null;
    }
}
