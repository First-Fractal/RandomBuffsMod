using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class RBMSystem : ModSystem
    {
        //track the cooldown for the random biff
        static int cooldownMax = FFLib.TimeToTick(0, 1);
        static int cooldown = cooldownMax;

        //get a random buff ID
        public static int randomBuffID = 1;

        public static int buffLen = ModContent.GetContent<ModBuff>().Count();
        public static List<int> allowedBuffs = new List<int>();

        //run after the world has been updated every tick
        public override void PostUpdateWorld()
        {
            //reset the max duration for the config option
            cooldownMax = FFLib.TimeToTick(RBMConfig.Instance.buffDuration);

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
                //lower down the cooldown if the player lower it down during gameplay
                if (cooldown > cooldownMax)
                {
                    cooldown = cooldownMax;
                }

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
                    randomBuffID = Main.rand.Next(1, allowedBuffs.Count);
                    Console.WriteLine(randomBuffID);

                    //reset the cooldown
                    cooldown = cooldownMax;
                    
                }
            }

            //get each player in the server
            foreach (Player plr in Main.player)
            {
                //cehck to see if it's singleplayer
                if (Main.netMode != NetmodeID.SinglePlayer)
                {
                    //send the current random buff id to the current player
                    ModPacket packet = ModContent.GetInstance<RandomBuffsMod>().GetPacket();
                    packet.Write(randomBuffID);
                    packet.Send();

                    //give the player the random buff
                    NetMessage.SendData(MessageID.AddPlayerBuff, -1, -1, null, plr.whoAmI, randomBuffID, cooldown);
                } else
                {
                    plr.AddBuff(randomBuffID, cooldown);

                }
            }

            //do the vanilla updates
            base.PostUpdateWorld();
        }
    }
}
