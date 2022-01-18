using System;
using Cichorium;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PrimitiveExpander;

namespace Intybus
{
    public class IntybusGame : Engine
    {
        public SpriteBatch SpriteBatch;

        private Vector3 _offset;
        private float[] _heights;
        private float[] _steepness;

        private const int _width = 10000;
        private const int _gaps = 5;
        private Texture2D _texture;

        public IntybusGame()
        {
            Content.RootDirectory = "Content";

            IsMouseVisible = true;
            Window.AllowUserResizing = true;

            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Initialize()
        {
            PrimitiveRenderer.Initialise(GraphicsDevice);
            GenerateTerrain(_width, 0.001f);
            
            _texture = Content.Load<Texture2D>("tank");
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

            PrimitiveRenderer.World = Matrix.CreateWorld(new Vector3(GraphicsDevice.Viewport.Bounds.Center.ToVector2(), 0), Vector3.Forward, Vector3.Up);
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

            var x = _offset.X;
            var clampedX = (int)x;
            var rotation = 0f;

            if (x >= 1 && x < _width)
            {
                var a = _heights[clampedX - 1];
                var b = _heights[clampedX];

                var m = b - a;
                var y = (m * (x - clampedX)) + a;

                rotation = m;
                
                _offset.Y = y;
            }
            else
            {
                _offset.X = Math.Clamp(_offset.X, 0, _width);
            }
            
            SpriteBatch.Begin(transformMatrix: Matrix.CreateTranslation(new Vector3(GraphicsDevice.Viewport.Bounds.Center.ToVector2(), 0)));
            SpriteBatch.Draw(_texture, new Vector2(0, 0), null, Color.Red, rotation, new Vector2(16, 32), 1f, SpriteEffects.None, 0f);
            SpriteBatch.End();
            
            /*PrimitiveRenderer.DrawCircleF(
                null,
                Color.Black,
                new Vector2(_offset.X, _offset.Y),
                10f
            );*/

            DrawTerrain();
        }

        private void GenerateTerrain(int width, float scale)
        {
            _steepness = new float[width - 1];
            _heights = SimplexNoise.Noise.Calc1D(width, scale);

        }

        private void DrawTerrain(float zLayer = 0f)
        {
            for (var x = 1; x < _heights.Length; x++)
            {
                var currHeight = _heights[x - 1];
                var nextHeight = _heights[x];

                var a = new Vector2(x - 1, currHeight);
                var b = new Vector2(x - 0, nextHeight);

                PrimitiveRenderer.DrawLine(
                    null,
                    Color.Black,
                    a, b,
                    zLayer
                );
            }
        }
    }
}