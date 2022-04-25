using System;
using TiledMapParser;
using GXPEngine;

public class Level2 : GameObject
{
    Player player;
    public int currentLevel = 1;
    public static bool restart = false;
    const int SIZE = 62;
    const int WIDTH = 15;
    const int HEIGHT = 15;

    Enemy[] enemies = new Enemy[7];

    int[,] level = new int[HEIGHT, WIDTH]
    {
            { 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6, 6},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5},
            { 4, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 5},
            { 4, 1, 7, 1, 1, 7, 1, 1, 1, 7, 1, 1, 7, 1, 5},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5},
            { 4, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 5},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5},
            { 4, 7, 1, 1, 7, 1, 1, 7, 1, 1, 7, 1, 1, 7, 5},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5},
            { 4, 1, 1, 1, 1, 1, 1, 7, 1, 1, 1, 1, 1, 1, 5},
            { 4, 1, 1, 7, 1, 1, 1, 1, 1, 1, 1, 7, 1, 1, 5},
            { 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 5},
            { 7, 3, 7, 3, 3, 3, 7, 3, 7, 3, 3, 3, 7, 3, 7}
    };


    public Level2()
    {
        setupLevel(currentLevel);
        if (player == null)
        {
            Player player = new Player(450, 400);
            AddChild(player);
            HUD hud;
            hud = new HUD(player);
            hud.collider.isTrigger = true;
            AddChild(hud);
        }
    }

    void setupLevel(int currentLevel)
    {
        for (int row = 0; row < HEIGHT; row++)
        {
            for (int col = 0; col < WIDTH; col++)
            {
                int tile = level[row, col];

                switch (tile)
                {
                    case 1:
                        jungleBackground background = new jungleBackground();
                        AddChild(background);
                        background.x = col * SIZE;
                        background.y = row * SIZE;
                        break;
                    case 3:
                        Lava lava = new Lava();
                        AddChild(lava);
                        lava.x = col * SIZE;
                        lava.y = row * SIZE;
                        break;
                    case 4://looking right
                        jungleWall wall = new jungleWall();
                        AddChild(wall);
                        wall.x = col * SIZE + 2;
                        wall.y = row * SIZE;
                        break;
                    case 5://looking left
                        jungleWall leftwall = new jungleWall();
                        AddChild(leftwall);
                        leftwall.x = col * SIZE+62;
                        leftwall.y = row * SIZE+62;
                        leftwall.rotation = 180;
                        break;
                    case 6://looking down
                        jungleWall ceilingwall = new jungleWall();
                        AddChild(ceilingwall);
                        ceilingwall.x = col * SIZE + 62;
                        ceilingwall.y = row * SIZE + 2;
                        ceilingwall.rotation = 90;
                        break;
                    case 7:
                        jungleGround ground = new jungleGround();
                        AddChild(ground);
                        ground.x = col * SIZE;
                        ground.y = row * SIZE;
                        break;

                }
            }
        }


        //================================
        enemies[0] = new Enemy(100, 300);
        AddChild(enemies[0]);

        Waypoint waypoint1 = new Waypoint(100, 300, 0);
        waypoint1.collider.isTrigger = true;
        AddChild(waypoint1);

        Waypoint waypoint2 = new Waypoint(800, 300, 180);
        waypoint2.collider.isTrigger = true;
        AddChild(waypoint2);
        //================================
        enemies[1] = new Enemy(800, 300);
        AddChild(enemies[1]);


        //================================
        enemies[2] = new Enemy(200, 100);
        AddChild(enemies[2]);

        Waypoint waypoint5 = new Waypoint(200, 100, 90);
        waypoint5.collider.isTrigger = true;
        AddChild(waypoint5);

        Waypoint waypoint6 = new Waypoint(200, 800, 270);
        waypoint6.collider.isTrigger = true;
        AddChild(waypoint6);
        //================================
        enemies[3] = new Enemy(700, 800);
        AddChild(enemies[3]);

        Waypoint waypoint7 = new Waypoint(700, 100, 90);
        waypoint7.collider.isTrigger = true;
        AddChild(waypoint7);

        Waypoint waypoint8 = new Waypoint(700, 800, 270);
        waypoint8.collider.isTrigger = true;
        AddChild(waypoint8);
        //================================
        enemies[4] = new Enemy(100, 600);
        AddChild(enemies[4]);

        Waypoint waypoint9 = new Waypoint(100, 600, 0);
        waypoint9.collider.isTrigger = true;
        AddChild(waypoint9);

        Waypoint waypoint10 = new Waypoint(800, 600, 180);
        waypoint10.collider.isTrigger = true;
        AddChild(waypoint10);
        //================================
        enemies[5] = new Enemy(800, 600);
        AddChild(enemies[5]);
        //================================
        enemies[6] = new Enemy(300, 100);
        AddChild(enemies[6]);

        Waypoint waypoint11 = new Waypoint(300, 100, 0);
        waypoint11.collider.isTrigger = true;
        AddChild(waypoint11);

        Waypoint waypoint12 = new Waypoint(600, 100, 180);
        waypoint12.collider.isTrigger = true;
        AddChild(waypoint12);

        Collectible crystal1 = new Collectible();
        crystal1.spawn();
        AddChild(crystal1);
    }

    void Update()
    {
        if (player != null)
        {
            x = game.width / (2 * game.scaleX) - player.x;
            y = game.height / (2 * game.scaleY) - player.y;
        }

        if (restart)
        {
            for (int i = 0; i < 7; i++)
            {
                enemies[i].visible = true;
            }
            restart = false;
        }
    }
}