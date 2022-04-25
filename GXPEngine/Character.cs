using System;
namespace GXPEngine
{
    public class Character : GameObject
    {
        AnimationSprite character;

        public Character() : base(false)
        {
            
            character = new AnimationSprite("Run.png", 12, 1);
            character.SetOrigin(character.width / 2, 5);
            character.SetXY(30, 20);
            character.SetScaleXY(1.6f, 1.6f);
            character.collider.isTrigger = true;
            AddChild(character);
        }

        public void Mirror(bool mir, bool ror)
        {
            character.Mirror(mir, ror);
        }

        public void SetCycle(int value, int length)
        {
            character.SetCycle(value, length);
        }

        public void Animate(float skolko)
        {
            character.Animate(skolko);
        }
    }
}
