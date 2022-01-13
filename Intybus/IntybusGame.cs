using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PrimitiveExpander;

namespace Intybus
{
    public class IntybusGame : Game
    {
        public SpriteBatch SpriteBatch;
        public GraphicsDeviceManager GraphicsDeviceManager;
        
        private Vector3 _offset;

        public IntybusGame()
        {
            // ReSharper disable once HeapView.ObjectAllocation.Evident
            GraphicsDeviceManager = new GraphicsDeviceManager(this);

            if (GraphicsDevice == null)
            {
                GraphicsDeviceManager.ApplyChanges();
            }

            // ReSharper disable once HeapView.ObjectAllocation.Evident
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Initialize()
        {
            PrimitiveRenderer.Initialise(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            
            if (!IsActive) return;

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var totalTime = (float)gameTime.TotalGameTime.TotalSeconds;

            var keyboardState = Keyboard.GetState();
            var mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            var velocity = Vector3.Zero;
            const float speed = 64f;
            
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                velocity.Y -= 1f;
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                velocity.Y += 1f;
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                velocity.X -= 1f;
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity.X += 1f;
            }

            if (velocity.Length() != 0)
            {
                velocity.Normalize();
                velocity *= speed * deltaTime;

                _offset += velocity;
            }
            
            var (width, height) = GraphicsDevice.Viewport.Bounds.Size.ToVector2();

            var viewPosition = _offset + new Vector3(width, height, 0) / 2;

            PrimitiveRenderer.World = Matrix.Identity;
            PrimitiveRenderer.View = Matrix.CreateLookAt(
                viewPosition + Vector3.Backward,
                viewPosition + Vector3.Forward,
                Vector3.Up
            );
            PrimitiveRenderer.Projection = Matrix.CreateOrthographic(
                width / 1,
                -height / 1,
                0.1f, 10f
            );
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            PrimitiveRenderer.DrawCircleF(
                null,
                Color.Black,
                new Vector2(0, 0),
                100f
            );
        }
    }
}