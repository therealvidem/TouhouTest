using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TouhouTest.Component
{
    public struct Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Rectangle? Source;
        public Color Color;
        public float Rotation;
        public Vector2 Origin;
        public Vector2 Scale;
        public SpriteEffects Effects;
        public float LayerDepth;
    }
}
