using System;
namespace GXPEngine
{
    public class Collectible : Sprite
    {
        Random rnd = new Random();
        AnimationSprite fruit;
        Sound coinSound;
        public Collectible() : base("checkers.png")
        {
            this.alpha = 0f;
            coinSound = new Sound("fruit.wav", false, false);
            fruit = new AnimationSprite("Orange.png", 17, 1);
            fruit.SetOrigin(fruit.width / 2, 5);
            fruit.SetXY(30, 20);
            fruit.SetScaleXY(1.8f, 1.8f);
            fruit.collider.isTrigger = true;


            AddChild(fruit);
            spawn();
        }
         
        public void spawn()
        {
            
            fruit.visible = true;
            this.x = rnd.Next(10,780);
            this.y = rnd.Next(10, 580);
            this.collider.isTrigger = true;
        }
        void Update()
        {
            fruit.SetCycle(1, 17);
            fruit.Animate(0.22f);
            checkCollision();
        }
        public void Pickup()
        {
            coinSound.Play();
            fruit.visible = false;
            spawn();
        }
        
        void checkCollision()
        {
            foreach (GameObject other in this.GetCollisions())
            {

                if (other is Ground || other is Wall || other is Character)
                {
                    spawn();
                }

            }
        }
    }
}