using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class RBMSystem : ModSystem
    {
        //define the utilities class
        static FFLib ff = new FFLib();

        //variables to keep track of the cooldown
        public static int cooldownMax = ff.TimeToTick(5, 0);
        public static int cooldown = 0;

        //global variable to track what buff was given by RNG 
        public int randomBuffID = 0;

        public override void PostUpdateWorld()
        {
            //if the cooldown is not at zero, then subtract it slowly
            if (cooldown > 0)
            {
                cooldown -= 1;
                //Console.WriteLine(cooldown);
            } else
            {
                //set a random buff ID
                randomBuffID = Main.rand.Next(1, BuffLoader.BuffCount);

                //reset the timer
                cooldown = cooldownMax;
            }

            //check if the player is in singleplayer, or in multiplayer
            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                //if the player is in multiplayer, then tell the player that its time for a random buff and what it is
                ModPacket packet = ModContent.GetInstance<RandomBuffsMod>().GetPacket();
                packet.Write(randomBuffID);
                packet.Send();
            } else
            {
                //loop through each player in the world
                foreach (Player player in Main.player)
                {
                    //check if the player is active, not dead
                    if (!player.dead && player.active)
                    {
                        //give the singleplayer the buff
                        player.AddBuff(randomBuffID, 2, false);
                    }
                }
            }

            //do the vanilla updates
            base.PostUpdateWorld();
        }
    }
}
