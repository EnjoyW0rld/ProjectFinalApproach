using System;
namespace GXPEngine
{
    public class Player : Sprite
    {
        
        GameObject[] bullets = new GameObject[100];

        public static int _score;
        int _bullets = 10;
        int currentLevel = 1;
        int speed = 3;
        int direction;
        int recoil = 100;
        bool isGrounded;
        bool Jumped;
        float yVelocity;

        AnimationSprite character;

        private Sprite _sensor;
        private Sprite ground_sensor;
        Sound shot;
        Camera cam = new Camera(0, 0, 800, 600);
        Character character1 = new Character();
        
        public Player(float x, float y) : base("checkers.png")
        {

            //spawnPlayer();
            this.x = x;
            this.y = y;
            this.alpha = 0f;
            this.SetScaleXY(0.9f, 0.9f);
            //character1.collider.isTrigger = false;


            _sensor = new Sprite("checkers.png");
            _sensor.visible = false;
            _sensor.SetScaleXY(1f,1f);
            _sensor.SetXY(0f, 0f);
            _sensor.collider.isTrigger = true;
            

            ground_sensor = new Sprite("colors.png");
            ground_sensor.visible = false;
            ground_sensor.SetScaleXY(0.5f, 0.2f);
            ground_sensor.SetXY(15f, 55f);
            ground_sensor.collider.isTrigger = true;


            AddChild(ground_sensor);
            AddChild(_sensor);
            AddChild(cam);
            AddChild(character1);

        }

        public void Update()
        {

            MoveUntilCollision(0, yVelocity);
            
            Collisions();
            CheckShooting();

            GroundedCheck();

            if (!isGrounded)
            {
                yVelocity += 0.26f;

            }
            else
            {
                yVelocity = 0;

            }

            HandleMovement();

            if(HUD.currentTime == 0)
            {
                gameOver();
                HUD.secondsLeft += 60;
            }
        }

        void GroundedCheck()
        {
            isGrounded = false;
            foreach (GameObject ground in ground_sensor.GetCollisions())
            {
                if (ground is Ground)
                {
                    isGrounded = true;
                    break;
                }
                if (ground is jungleGround)
                {
                    isGrounded = true;
                    break;
                }
                if (ground is Lava)
                {
                    Lava lava = ground as Lava;
                    gameOver();
                }
            }
        }
        void Jump()
        {
            if (isGrounded)
            {
                yVelocity = -11;
                isGrounded = false;
            }
        }

        private void Collisions()
        {
            foreach (GameObject other in _sensor.GetCollisions())
            {
                if(other is Collectible)
                {
                    Collectible crystal = other as Collectible;
                    crystal.Pickup();
                    _score++;
                }

                

                if (other is Enemy)
                {
                    Lava lava = other as Lava;
                    if(other.visible)
                        gameOver();
                }

            }
        }

        private void HandleMovement()
        {
            bool moving = false;
            if (Input.GetKey(Key.A))
            {
                MoveUntilCollision(-speed, 0);
                moving = true;
                character1.Mirror(true, false);
                direction = 0; //left
            }
            if (Input.GetKey(Key.D))
            {
                MoveUntilCollision(speed, 0);
                moving = true;
                character1.Mirror(false, false);
                direction = 1; //right
            }
            if (Input.GetKey(Key.W))
            {
                Jump();
            }
            if (moving)
            {
                character1.SetCycle(1, 13);
            }
            else
            {
                character1.SetCycle(4, 1);
            }

            character1.Animate(0.2f);
        }

        private void CheckShooting()
        {

            if (Input.GetKeyUp(Key.SPACE) && _bullets > 0)
            {
                shot = new Sound("shot.mp3", false, true);
                shot.Play();
                Bullet bullet = new Bullet(-20, -25, direction);
                parent.LateAddChild(bullet);
                bullet.x = this.x;
                bullet.y = this.y;
                _bullets--;
                bullet.collider.isTrigger = true;

            }
            
        }

        public int GetScore()
        {
            return _score;   
        }
        public int GetBullets()
        {
            return _bullets;
        }

        public void gameOver()
        {
            int time;
            time = 60 - HUD.currentTime;
            HUD.secondsLeft += time;
            _bullets = 10;
            _score = 0;
            Menu._gameover = true;
            this.x = 420;
            this.y = 350;
            Level.restart = true;
            Level2.restart = true;
        }


    }
}
