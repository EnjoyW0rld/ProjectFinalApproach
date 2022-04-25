using System;
using System.Threading.Tasks;
using System.Threading;

namespace GXPEngine
{
    public class Bullet : Sprite
    {
        int _direction = 0;
        int speed = 6;
        bool bulletAlive = true;
        bool bulletInWall = false;

        float timer = 0f;
        float enemyTimer = 0f;

        AnimationSprite bullet_anim;

        public Bullet(float x, float y, int direction) : base("Bullet1.png")
        {
            _direction = direction;
            SetOrigin(x, y);
            SetXY(x, y);
            this.collider.isTrigger = true;

            bullet_anim = new AnimationSprite("Shoot.png", 7, 1);
            bullet_anim.SetOrigin(0, 0);
            bullet_anim.SetXY(-10, -24);
            bullet_anim.visible = false;
            AddChild(bullet_anim);
            bullet_anim.collider.isTrigger = true;

        }

        void Update()
        {
            if (bulletAlive)
            {
                if (_direction == 1)
                    Move(speed, 0);
                if (_direction == 0)
                    Move(-speed, 0);
            }
            if (bulletInWall)
            {
                timer += 0.02f;
                enemyTimer += 0.02f;
            }
            bullet_anim.SetCycle(1, 7);

            if (enemyTimer > 0.28f)
            {
                this.LateDestroy();
            }
        }


        void OnCollision(GameObject other)
        {
            if (other is Wall || other is Ground || other is jungleGround || other is jungleWall)
            {
                
                if (!bulletInWall)
                {
                    timer = 0f;
                    bulletInWall = true;
                }
                bullet_anim.visible = true;
                bulletAlive = false;
                bullet_anim.Animate(0.26f);
                if(timer > 10.2f)
                {
                    this.LateDestroy();
                }
                
            }
            if (other is Enemy)
            {
                if (other.visible == true)
                {
                
                    if (!bulletInWall)
                    {
                        enemyTimer = 0f;
                        bulletInWall = true;
                    }
                    bullet_anim.visible = true;
                    bulletAlive = false;
                    bullet_anim.Animate(0.1f);

                    other.visible = false;
                }
            }
        }
    }
}
