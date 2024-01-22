using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class RBMGlobalBuff : GlobalBuff
    {
        //prevent the player from canceling any buff by being able to right click it
        public override bool RightClick(int type, int buffIndex)
        {
            return false;
        }

        //function that will remove all buffs that wasnt provided by RNG
        public override void Update(int type, Player player, ref int buffIndex)
        {
            //if the current buff not the player buff, then remove it
            if (type != player.GetModPlayer<RBMPlayer>().randomBuffID)
            {
                player.ClearBuff(type);
            }

            //do vanilla functions
            base.Update(type, player, ref buffIndex);
        }
    }
}
