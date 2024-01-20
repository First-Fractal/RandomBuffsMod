using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class RBMSystem : ModSystem
    {
        //define the utilities classes
        static Random rand = new Random();
        static FFLib ff = new FFLib();

        //variables to keep track of the cooldown
        //static int cooldownMax = ff.TimeToTick(0, 1);
        public static int cooldownMax = ff.TimeToTick(5, 0);
        public static int cooldown = 0;

        public override void PostUpdateWorld()
        {
            //if the cooldown is not at zero, then subtract it slowly
            if (cooldown > 0)
            {
                cooldown -= 1;
                Console.WriteLine(cooldown);
            } else
            {
                //set a random buff ID
                RBMGlobalBuffs.randomBuffID = rand.Next(1, BuffLoader.BuffCount);

                //reset the timer
                cooldown = cooldownMax;
            }

            //loop through each player in the world
            foreach (Player player in Main.player)
            {
                //check if the player is active, not dead
                if (!player.dead && player.active)
                {
                    //apply the buff for one minute 
                    player.AddBuff(RBMGlobalBuffs.randomBuffID, 2, false);
                }
            }

            //do the vanilla updates
            base.PostUpdateWorld();
        }
    }
}
