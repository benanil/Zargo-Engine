using System;

namespace ZargoEngine.Editor
{
    [AttributeUsage(AttributeTargets.Field)]
    public class NonSerializedAttribute : Attribute
    {
        public NonSerializedAttribute()
        {

        }
    }
}
