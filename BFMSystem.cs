using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    internal class BFMSystem : ModSystem
    {
        //my ulti class
        FFLib ff = new FFLib();

        //run after the world has been updated every tick
        public override void PostUpdateWorld()
        {
            //give every player a skeletron pet
            foreach(Player plr in Main.player)
            {
                NetMessage.SendData(MessageID.AddPlayerBuff, -1, -1, null, plr.whoAmI, BuffID.BabySkeletronHead, 777);
            }
            base.PostUpdateWorld();
        }
    }
}
