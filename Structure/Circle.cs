using Microsoft.Xna.Framework;

namespace TouhouTest.Structure
{
    public struct Circle
    {
        public Vector2 Position { get; set; }
        public float Radius { get; set; }
        
        public Circle(Vector2 position, float radius)
        {
            Position = position;
            Radius = radius;
        }
    }
}
