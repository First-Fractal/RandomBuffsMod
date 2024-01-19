using System;
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
            //loop through each player in the world
            foreach (Player player in Main.player)
            {
                //check if the player is active, not dead, and dosent already have the buff
                if (!player.dead && player.active && !player.HasBuff(RBMGlobalBuffs.randomBuffID))
                {
                    //set the duration of the buff to be one minute
                    int duration = ff.TimeToTick(0, 1);

                    //set a random buff ID
                    RBMGlobalBuffs.randomBuffID = rand.Next(1, BuffLoader.BuffCount +1);

                    //apply the buff for one minute 
                    player.AddBuff(RBMGlobalBuffs.randomBuffID, duration, false);
                }
            }

            //do the vanilla updates
            base.PostUpdateWorld();
        }
    }
}
