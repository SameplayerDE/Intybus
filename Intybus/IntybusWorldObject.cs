using Microsoft.Xna.Framework;

namespace Intybus
{
    public abstract class IntybusWorldObject
    {
        /// <summary>
        /// The World The Object Is Acting In
        /// </summary>
        public IntybusWorld World;

        public virtual void PreUpdate(GameTime gameTime) {}
        public virtual void Update(GameTime gameTime) {}
        public virtual void PostUpdate(GameTime gameTime) {}
    }
}