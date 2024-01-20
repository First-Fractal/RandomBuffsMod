using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class RBMGlobalBuffs : GlobalBuff
    {
        //prevent the player from canceling any buff by being able to right click it
        public override bool RightClick(int type, int buffIndex)
        {
            return false;
        }

        //function that will remove all buffs that wasnt provided by RNG
        public override void Update(int type, Player player, ref int buffIndex)
        {
            //get the buff id depending on single or multiplayer
            int randomBuffID = 0;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                RBMSystem sys = ModContent.GetInstance<RBMSystem>();
                randomBuffID = sys.randomBuffID;
            } else
            {
                RBMPlayer plr = ModContent.GetInstance<RBMPlayer>();
                randomBuffID = plr.randomBuffID;
            }

            //if the current buff is not the same as the RNG buff
            if (type != randomBuffID)
            {
                player.ClearBuff(type);
            }
            base.Update(type, player, ref buffIndex);
        }
    }
}
