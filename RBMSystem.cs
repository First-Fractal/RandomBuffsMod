using System;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class RBMSystem : ModSystem
    {
        Random rand = new Random();
        FFLib ff = new FFLib();
        public override void PostUpdateWorld()
        {
            //set a random buff ID
            RBMGlobalBuffs.randomBuffID = rand.Next(1, BuffID.Count);

            //loop through each player in the world
            foreach (Player player in Main.player)
            {
                //check if the player is active, not dead, and dosent already have the buff
                if (!player.dead && player.active && !player.HasBuff(RBMGlobalBuffs.randomBuffID))
                {
                    //apply the buff for one minute 
                    player.AddBuff(RBMGlobalBuffs.randomBuffID, ff.TimeToTick(0, 0, 1));
                }
            }

            //do the vanilla updates
            base.PostUpdateWorld();
        }
    }
}
