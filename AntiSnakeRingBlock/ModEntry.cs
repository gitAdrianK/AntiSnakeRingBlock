using AntiSnakeRingBlock.Behaviours;
using AntiSnakeRingBlock.Blocks;
using AntiSnakeRingBlock.Factories;
using EntityComponent;
using HarmonyLib;
using JumpKing.Level;
using JumpKing.MiscEntities.WorldItems;
using JumpKing.MiscEntities.WorldItems.Inventory;
using JumpKing.Mods;
using JumpKing.Player;
using System.Reflection;

namespace AntiSnakeRingBlock
{
    [JumpKingMod("Zebra.AntiSnakeRingBlock")]
    public static class ModEntry
    {
        const string IDENTIFIER = "Zebra.AntiSnakeRingBlock";
        const string HARMONY_IDENTIFIER = "Zebra.AntiSnakeRingBlock.Harmony";

        /// <summary>
        /// Called by Jump King before the level loads
        /// </summary>
        [BeforeLevelLoad]
        public static void BeforeLevelLoad()
        {
            LevelManager.RegisterBlockFactory(new FactoryAntiSnake());

            Harmony harmony = new Harmony(HARMONY_IDENTIFIER);
            MethodInfo hasSnakeRingEnabled = typeof(InventoryManager).GetMethod(nameof(InventoryManager.HasItemEnabled));
            HarmonyMethod preventSnake = new HarmonyMethod(typeof(ModEntry).GetMethod(nameof(PreventSnake)));
            harmony.Patch(
                hasSnakeRingEnabled,
                postfix: preventSnake);
        }

        [OnLevelStart]
        public static void OnLevelStart()
        {
            EntityManager entityManager = EntityManager.instance;
            PlayerEntity player = entityManager.Find<PlayerEntity>();

            if (player == null)
            {
                return;
            }

            BehaviourAntiSnake behaviourAntiSnake = new BehaviourAntiSnake();
            player.m_body.RegisterBlockBehaviour(typeof(BlockAntiSnake), behaviourAntiSnake);
        }

        public static void PreventSnake(Items p_item, ref bool __result)
        {
            if (p_item == Items.SnakeRing && BehaviourAntiSnake.IsOnBlock)
            {
                __result = false;
            }
        }
    }
}
