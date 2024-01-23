using System;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.Config;

namespace RandomBuffsMod
{
    internal class RBMConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        public static RBMConfig Instance;

        [Header("$Mods.RandomBossSizes.Config.Header.GeneralOptions")]

        [DefaultValue(60)]
        public int buffDuration;

        [DefaultValue(true)]
        public bool onlyOneBuff;

        [DefaultValue(true)]
        public bool includeModdedBuffs;

        //[DefaultValue(true)]
        //public bool includeDebuffs;

        //[DefaultValue(true)]
        //public bool includeMount;

        //[DefaultValue(true)]
        //public bool includePets;

        //[DefaultValue(true)]
        //public bool includeSummons;

        public void updateAllowedBuffs()
        {
            //clear the list
            RBMSystem.allowedBuffs = new List<int>();

            //get a list of all buffs loaded
            for (int i = 0; i <= RBMSystem.buffLen; i++)
            {
                //check if the list should not include modded buffs
                if (!Instance.includeModdedBuffs)
                {
                    //if the current buffid is outside the vanilla range, then ignore it
                    if (i > BuffID.Count)
                    {
                        continue;
                    }
                }

                ////check if the list should not include debuff
                //if (!Instance.includeDebuffs)
                //{
                //    //this piece of shit is not working, and idk why
                //    //if the current buffid is a debuff, then ignore it
                //    if (Main.debuff[i] == true || BuffID.Sets.NurseCannotRemoveDebuff[i] == true)
                //    {
                //        Console.WriteLine("I've skip a buff with the ID: " + i + " cause it's a debuff");
                //        continue;
                //    }
                //}

                ////check if the list should not include pets
                //if (!Instance.includePets)
                //{
                //    //if the current buffid is a pet, then ignore it
                //    if (Main.vanityPet[i] == true)
                //    {
                //        Console.WriteLine("I've skip a buff with the ID: " + i + " cause it's a pet");
                //        continue;
                //    }
                //}

                //add the current buff ID to the list
                RBMSystem.allowedBuffs.Add(i);
            }

            Console.WriteLine("The allowed buff list is " + RBMSystem.allowedBuffs.Count + " while the buffLen is " + RBMSystem.buffLen);
        }

        public override void OnLoaded()
        {
            updateAllowedBuffs();
            base.OnLoaded();
        }

        public override void OnChanged()
        {
            updateAllowedBuffs();
            base.OnChanged();
        }
    }
}
