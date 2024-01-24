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
            //disable the long expert mode debuff
            BuffID.Sets.LongerExpertDebuff[type] = false;

            //put the timer on all debuffs
            Array.Fill(Main.buffNoTimeDisplay, false);
            //Array.Fill(Main.vanityPet, false);

            //if the player only wants one buff and the current buff not the player buff, then remove it
            if (RBMConfig.Instance.onlyOneBuff && type != player.GetModPlayer<RBMPlayer>().randomBuffID)
            {
                player.ClearBuff(type);
            }

            //do vanilla functions
            base.Update(type, player, ref buffIndex);
        }

        public override void ModifyBuffText(int type, ref string buffName, ref string tip, ref int rare)
        {
            string buffSource = "Vanilla";

            if (type > BuffID.Count)
            {
                buffSource = "Modded";
            }
            string buff = "(" + buffSource + " Buff)";
            tip += "\n[c/327DFF:" + buff + "]";

            base.ModifyBuffText(type, ref buffName, ref tip, ref rare);
        }
    }
}
