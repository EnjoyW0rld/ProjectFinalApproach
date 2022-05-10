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
        float startTime;
        float value;
        float startValue;
        float currentValue;
        float delay;

        Parameter parameter;
        Function function;

        bool startValueAssigned;
        public Tween(Parameter p, float t, float desiredValue,Function func, float del = 0)
        {
            value = desiredValue;
            function = func;
            parameter = p;
            startTime = Time.time;
            delay = del * 1000;
            easeTime = (t * 1000) + delay;
        }

        void Update()
        {
            if (delay >= (Time.time - startTime)) return;

            if (!startValueAssigned) GetStartValue();
            currentValue = GetCurrentValue();
            ApplyData();
            if(easeTime <= (Time.time - startTime))
            {
                currentValue = value;
                ApplyData();
                parent.RemoveChild(this);
                LateDestroy();
                LateRemove();
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
                case Function.easeInQuad: return Lerp(startValue, value, easeInQuad((Time.time - startTime) / easeTime));
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
