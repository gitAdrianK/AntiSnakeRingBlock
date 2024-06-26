using JumpKing.Level;
using Microsoft.Xna.Framework;

namespace AntiSnakeRingBlock.Blocks
{
    public class BlockAntiSnake : IBlock, IBlockDebugColor
    {
        public static readonly Color BLOCKCODE_ANTI_SNAKE = new Color(105, 111, 143);

        private readonly Rectangle collider;

        public BlockAntiSnake(Rectangle collider)
        {
            this.collider = collider;
        }

        public Color DebugColor
        {
            get { return BLOCKCODE_ANTI_SNAKE; }
        }

        public Rectangle GetRect()
        {
            return collider;
        }

        public BlockCollisionType Intersects(Rectangle hitbox, out Rectangle intersection)
        {
            if (collider.Intersects(hitbox))
            {
                intersection = Rectangle.Intersect(hitbox, collider);
                return BlockCollisionType.Collision_NonBlocking;
            }
            intersection = Rectangle.Empty;
            return BlockCollisionType.NoCollision;

        }
    }
}

