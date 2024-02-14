using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
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
        
        //make a list of allowed buffs
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
                }
                else
                {
                    //get a random buff
                    randomBuffID = Main.rand.Next(1, allowedBuffs.Count - 1);

                    //clamp the random buff id just in case
                    randomBuffID = Math.Clamp(randomBuffID, 1, allowedBuffs.Count -1);

                    //grab the random buff from the allowed list
                    randomBuffID = allowedBuffs[randomBuffID];

                    //reset the cooldown
                    cooldown = cooldownMax;

                    //tell the players about the new buff
                    FFLib.Talk(Language.GetTextValue("Mods.RandomBuffsMod.Message"), Color.OrangeRed);
                }
            }

            //get each player in the server
            foreach (Player plr in Main.player)
            {
                //check if the player is active and don't give the buff if the player is dead
                if (plr.active && !plr.dead)
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
                    }
                    else
                    {
                        //give the single player a random buff
                        plr.AddBuff(randomBuffID, cooldown);

                        //save the random buff id for the players for future refences
                        plr.GetModPlayer<RBMPlayer>().randomBuffID = randomBuffID;
                    }
                }
            }

            //do the vanilla updates
            base.PostUpdateWorld();
        }

        //update the list every second to match the config
        public override void PreUpdateWorld()
        {
            if (cooldown % FFLib.TimeToTick(1) != 0)
                return;

            //clear the list
            allowedBuffs = new List<int>();

            //get a list of all buffs loaded
            for (int i = 0; i <= BuffLoader.BuffCount - 1; i++)
            {
                //check if the list should not include modded buffs
                if (!RBMConfig.Instance.includeModdedBuffs)
                {
                    //if the current buff id is outside the vanilla range, then ignore it
                    if (i > BuffID.Count)
                    {
                        continue;
                    }
                }

                //check if the list should not include debuff
                if (!RBMConfig.Instance.includeDebuffs)
                {
                    //if the current buffID is a debuff, then ignore it
                    if (Main.debuff[i] == true || Main.pvpBuff[i] == true)
                    {
                        continue;
                    }
                }

                //check if the list should not include pets
                if (!RBMConfig.Instance.includePets)
                {
                    //if the current buffid is a pet, then ignore it
                    if (Main.vanityPet[i] == true || Main.lightPet[i] == true)
                    {
                        continue;
                    }
                }

                //check if the list should not include minecarts
                if (!RBMConfig.Instance.includeMinecart)
                {
                    //if the current buffid is a minecart, then ignore it
                    if (BuffID.Sets.BasicMountData[i] != null)
                    {
                        continue;
                    }
                }

                //add the current buff ID to the list
                allowedBuffs.Add(i);
            }
            base.PreUpdateWorld();
        }
    }
}
