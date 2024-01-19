using Terraria;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class RBMGlobalBuffs : GlobalBuff
    {
        //global variable to track what buff was given by RNG 
        public static int RandomBuffID = 0;

        //function that will remove all buffs that wasnt provided by RNG
        public override void Update(int type, Player player, ref int buffIndex)
        {
            //if the current buff is not the same as the RNG buff
            if (type != RandomBuffID)
            {
                player.ClearBuff(type);
            }
            base.Update(type, player, ref buffIndex);
        }
    }
}
