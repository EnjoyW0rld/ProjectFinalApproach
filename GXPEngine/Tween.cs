using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    internal class Tween : GameObject
    {
        public enum Parameter { x,y, a}
        public enum Function { easeInQuad }

        float easeTime;
        float value;
        float startValue;
        float currentValue;

        Parameter parameter;
        Function function;

        bool startValueAssigned;
        public Tween(Parameter p, float t, float desiredValue,Function func)
        {
            value = desiredValue;
            function = func;
            parameter = p;
            easeTime = (t * 1000) + Time.time;
        }

        void Update()
        {
            if (!startValueAssigned) GetStartValue();
            currentValue = GetCurrentValue();
            ApplyData();
            if(easeTime <= Time.time)
            {
                currentValue = value;
                ApplyData();
                LateDestroy();
                LateRemove();
                parent.RemoveChild(this);
            }
        }

        void ApplyData()
        {
            switch (parameter)
            {
                case Parameter.x:
                    if(parent is Player)
                    ((Player)parent).ChangePos(currentValue);
                    else parent.x = currentValue;
                    break;
                case Parameter.y: 
                    if(parent is Player)
                        ((Player)parent).ChangePos(-1, currentValue);
                    else parent.y = currentValue;
                    break;
                case Parameter.a: ((Sprite)parent).alpha = currentValue; break;
            }
        }
        float GetCurrentValue()
        {
            switch (function)
            {
                case Function.easeInQuad: return Lerp(startValue, value, easeInQuad(Time.time / easeTime));
            }
            return 0;
        }

        float easeInQuad(float t)
        {
            return t * t;
        }

        float Lerp(float minValue,float maxValue, float pct)
        {
            return minValue + (maxValue - minValue) * pct;
        }
        void GetStartValue()
        {
            switch (parameter)
            {
                case Parameter.x: startValue = parent.x;break;
                case Parameter.y: startValue = parent.y;break;
                case Parameter.a: 
                    try
                    {
                        startValue =  ((Sprite)parent).alpha;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    break;
            }
            startValueAssigned = true;
        }
    }
}
