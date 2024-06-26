using AntiSnakeRingBlock.Blocks;
using JumpKing.API;
using JumpKing.Level;
using JumpKing.Level.Sampler;
using JumpKing.Workshop;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace AntiSnakeRingBlock.Factories
{
    public class FactoryAntiSnake : IBlockFactory
    {
        private static readonly HashSet<Color> supportedBlockCodes = new HashSet<Color> {
            BlockAntiSnake.BLOCKCODE_ANTI_SNAKE,
        };

        public bool CanMakeBlock(Color blockCode, Level level)
        {
            return supportedBlockCodes.Contains(blockCode);
        }

        public bool IsSolidBlock(Color blockCode)
        {
            return false;
        }

        public IBlock GetBlock(Color blockCode, Rectangle blockRect, Level level, LevelTexture textureSrc, int currentScreen, int x, int y)
        {
            switch (blockCode)
            {
                case var _ when blockCode == BlockAntiSnake.BLOCKCODE_ANTI_SNAKE:
                    return new BlockAntiSnake(blockRect);
                default:
                    throw new InvalidOperationException($"{typeof(FactoryAntiSnake).Name} is unable to create a block of Color code ({blockCode.R}, {blockCode.G}, {blockCode.B})");
            }
        }
    }
}
