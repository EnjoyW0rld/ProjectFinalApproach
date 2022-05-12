using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class HUD : EasyDraw
    {
        Player player;
        EasyDraw transition;
        bool isTransiting;
        int nextScene;
        int fuelWidth;
        int healthWidth;
        int currentEmotion;
        int changeFaceTimer;

        Sprite health;
        Sprite fuel;
        AnimationSprite face;
        AnimationSprite skills;


        public HUD(Player pl) : base(1920,1080,false)
        {
            Sprite frame = new Sprite("Assets/UIThingy.png");
            health = new Sprite("Assets/health.png");
            health.x = 192;
            health.y = 120;
            healthWidth = health.width;
            AddChild(health);

            face = new AnimationSprite("Assets/UIexpressions.png",3,1);
            face.SetFrame(1);
            face.x = 52;
            face.y = 62;
            AddChild(face);

            skills = new AnimationSprite("Assets/SKills.png", 3, 2);
            skills.x = 90;
            skills.y = 194;
            AddChild(skills);

            fuel = new Sprite("Assets/fuel.png");
            fuel.x = 192;
            fuel.y = 120;
            fuelWidth = fuel.width;
            AddChild(fuel);

            frame.x += 30;
            frame.y += 30;
            AddChild(frame);
            player = pl;
            transition = new EasyDraw(1920, 1080);
            transition.Clear(0);
            transition.x = -1920;
            AddChild(transition);

            EventsHandler.ChangeEmotion += SetEmotion;
        }
        void Update()
        {
            //if (Input.GetMouseButtonDown(0)) Console.WriteLine(Input.mouseX + "   " + Input.mouseY);
            ClearTransparent();
            //Text("Fuel left: " + player.fuelAmount, 10, 200);
            fuel.width = (fuelWidth * PlayerInfo.currentFuelCount) / PlayerInfo.fuelCount;
            health.width = (healthWidth * PlayerInfo.currentHealth) / PlayerInfo.MaxHealth;
            if(changeFaceTimer < Time.time)
            {
                face.SetFrame(1);
            }

        }

        void SetEmotion(int i)
        {
            currentEmotion = i;
            face.SetFrame(i);
            changeFaceTimer = Time.time + 2000;
        }

    }
}
