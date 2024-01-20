using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
    public class RBMPlayer : ModPlayer
    {
        //define the utilities classes
        static FFLib ff = new FFLib();

        //variables for tracking the random buffs
        public int randomBuffID = 0;

        public override void PostUpdate()
        {
            //check if the player is alive and not dead.
            if (Player.active && !Player.dead)
            {
                //apply the buff
                Player.AddBuff(randomBuffID, ff.TimeToTick(0, 2));

                //sync the player buffs
                NetMessage.SendData(MessageID.PlayerBuffs, -1, -1, null, Player.whoAmI);
            }

            base.PostUpdate();
        }
    }
}
