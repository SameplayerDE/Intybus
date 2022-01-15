using Intybus.Interfaces;
using Microsoft.Xna.Framework;

namespace Intybus
{
    public abstract class IntybusEntity : IntybusWorldObject
    {
        /// <summary>
        /// The Position The Entity Has In The World
        /// </summary>
        public Vector2 Position;
    }
}