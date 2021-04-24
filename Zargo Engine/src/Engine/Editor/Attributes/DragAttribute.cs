using System;

namespace ZargoEngine.Editor
{
    [AttributeUsage(AttributeTargets.Field,AllowMultiple = false)]
    public class DragAttribute : Attribute
    {
        public float min = 0;
        public float max = float.MaxValue;
        public float speed = 1;
        public string format = string.Empty;

        public DragAttribute(float speed = 1,float min = 0, float max = 10,string format = ""){
            this.format = format == "" ? "%.1f" : format;
            this.speed = speed;
            this.min = min;
            this.max = max;
        }
    }
}
