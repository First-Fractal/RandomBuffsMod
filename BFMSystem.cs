using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class BFMSystem : ModSystem
    {
        //my libary class
        FFLib ff = new FFLib();

        //get a random buff ID
        public int randomBuffID = Main.rand.Next(1, BuffLoader.BuffCount);

        //run after the world has been updated every tick
        public override void PostUpdateWorld()
        {
            //give every player a random buff
            foreach(Player plr in Main.player)
            {
                randomBuffID = Main.rand.Next(1, BuffLoader.BuffCount);
                NetMessage.SendData(MessageID.AddPlayerBuff, -1, -1, null, plr.whoAmI, randomBuffID, 777);
            }
            base.PostUpdateWorld();
        }
    }
}
