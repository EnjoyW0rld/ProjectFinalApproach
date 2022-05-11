﻿using System;
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
        List<Satelite> satelites = new List<Satelite>();
        Vec2 playerStartPos;
        public readonly int sceneNumber;
        //EasyDraw HUD;
        HUD hud;

        public Scene(string mapName)
        {
            AddChild(MyMapInterpritor.GetBackground(mapName));
            //HUD = new EasyDraw(1920,1080,false);
            player = new Player();
            hud = new HUD(player);
            //AddChild(new Sprite("Assets/spaceback.png"));
            foreach (GameObject item in MyMapInterpritor.GetGameObjects(mapName))
            {
                spaceBodies.Add(item as SpaceBody);
                AddChild(item);
                if(item is Planet)
                {
                    Planet p = item as Planet;
                    if (p.addSatelite)
                    {

                    Satelite satelite = new Satelite(item as SpaceBody,p.sSpeed);
                    AddChild(satelite);
                    satelites.Add(satelite);

                    }

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
            AddChild(hud);
            sceneNumber = MyMapInterpritor.GetLevelNumber(mapName);
        }
        void Update()
        {
        }
        virtual public Player GetPlayer() => player;
        virtual public Satelite[] GetSatelites() => satelites.ToArray();
        virtual public BallCollider[] GetBallColliders()
        {
            if (colliders == null)
            {
                List<BallCollider> ballColliders = new List<BallCollider>();
                //colliders = new BallCollider[spaceBodies.Count];
                for (int i = 0; i < spaceBodies.Count; i++)
                {
                    ballColliders.Add(spaceBodies[i].GetCollider());
                    //colliders[i] = spaceBodies[i].GetCollider();
                }
                for (int i = 0; i < satelites.Count; i++)
                {
                    ballColliders.Add(satelites[i].GetBallCollider());
                }
                colliders = ballColliders.ToArray();
            }
            return colliders;
        }
        virtual public SpaceBody[] GetSpaceBodies() => spaceBodies.ToArray();
    }
}
