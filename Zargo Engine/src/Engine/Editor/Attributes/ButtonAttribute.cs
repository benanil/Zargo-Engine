
using System;
using System.Numerics;

namespace ZargoEngine.Editor
{
    [AttributeUsage(AttributeTargets.Method)]
    public unsafe class ButtonAttribute : Attribute
    {
        public Vector2 size;

        public ButtonAttribute()
        {
            size = new Vector2(60, 20);
        }

        public ButtonAttribute(Vector2 size)
        {
            this.size = size;
        }
    }
}