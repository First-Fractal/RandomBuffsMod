using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class RBMSystem : ModSystem
    {
        //my libary class
        static FFLib ff = new FFLib();

        //track the cooldown for the random biff
        int cooldownMax = ff.TimeToTick(5);
        int cooldown = 0;

        //get a random buff ID
        public static int randomBuffID = Main.rand.Next(1, BuffLoader.BuffCount);

        //run after the world has been updated every tick
        public override void PostUpdateWorld()
        {
            //flag to track if someone is active in the world
            bool activePlayer = false;

            //iter through each player to see if someone is active
            foreach(Player player in Main.player)
            {
                if (player.active == true)
                {
                    activePlayer = true;
                    break;
                }
            }

            //if a player is active, then start the cooldown
            if (activePlayer)
            {
                //is the cooldown done yet
                if (cooldown > 0)
                {
                    //decrease the cooldown
                    cooldown--;
                    //Console.WriteLine(cooldown);
                }
                else
                {
                    //get a random buff
                    randomBuffID = Main.rand.Next(1, BuffLoader.BuffCount);

                    //reset the cooldown
                    cooldown = cooldownMax;
                    
                }
            }

            //get each player in the server
            foreach (Player plr in Main.player)
            {
                //send the current random buff id to the current player
                ModPacket packet = ModContent.GetInstance<RandomBuffsMod>().GetPacket();
                packet.Write(randomBuffID);
                packet.Send();

                //give the player the random buff
                NetMessage.SendData(MessageID.AddPlayerBuff, -1, -1, null, plr.whoAmI, randomBuffID, cooldown);
            }

            //do the vanilla updates
            base.PostUpdateWorld();
        }
    }
}
