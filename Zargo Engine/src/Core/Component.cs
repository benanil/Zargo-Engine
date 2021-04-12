
using System;
using ZargoEngine;

namespace ZargoEngine.Core
{
    public abstract class Component : IDisposable
    {
        public volatile GameObject gameObject;

        protected Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public void Dispose()
        {

        }
    }
}
