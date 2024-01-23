using Terraria;
using Terraria.ID;
using Terraria.Chat;
using Terraria.Localization;
using Microsoft.Xna.Framework;

//this is my own libary that I use to store snippits. I don't want it to be it's own mod, so I'll use copy and paste this file when needed.
namespace RandomBuffsMod
{
    public class FFLib
    {
        //function that will convert human time to terraria ticks
        public static int TimeToTick(int secs = 0, int mins = 0, int hours = 0, int days = 0)
        {
            //define the units
            int sec = 60;
            int min = sec * 60;
            int hour = min * 60;
            int day = hours * 24;

            //multiply the units and combine the final time
            return (sec * secs) + (min * mins) + (hour * hours) + (day * days);
        }

        //list of all the boss parts
        public static int[] BossParts = { NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail, NPCID.Creeper, NPCID.SkeletronHand, NPCID.SkeletronHead, NPCID.WallofFleshEye, NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail, NPCID.Probe, NPCID.PrimeCannon, NPCID.PrimeLaser, NPCID.PrimeSaw, NPCID.PrimeVice, NPCID.PlanterasHook, NPCID.PlanterasTentacle, NPCID.GolemFistLeft, NPCID.GolemFistRight, NPCID.GolemHead, NPCID.GolemHeadFree, NPCID.CultistBossClone, NPCID.MoonLordCore, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordFreeEye, NPCID.MoonLordLeechBlob };

        public static void Talk(string message, Color color)
        {
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(message, color);
            }
            else
            {
                ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(message), color);
            }
        }

        //function for checking if a boss is currently alive
        public static bool IsBossAlive()
        {
            foreach(NPC npc in Main.npc)
            {
                if (npc.boss == true)
                {
                    return true;
                } else
                {
                    foreach (int part in BossParts)
                    {
                        if (npc.type == part)
                        {
                            return true;
                        }
                    }
                }
                
            }
            return false;
        }
    }
}