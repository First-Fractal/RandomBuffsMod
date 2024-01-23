using System;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader.Config;

namespace RandomBuffsMod
{
    //modded class that is for allowed the mod to be configable
    internal class RBMConfig : ModConfig
    {
        //tell the mod that the configs are on the server and logistical side of the mod. 
        public override ConfigScope Mode => ConfigScope.ServerSide;

        //define the instance for the mod
        public static RBMConfig Instance;

        //display the general options line 
        [Header("$Mods.RandomBossSizes.Config.Header.GeneralOptions")]

        //define the defualt config value for buff duration
        [DefaultValue(60)]
        public int buffDuration;

        //define the defualt config value allowing only one buff
        [DefaultValue(true)]
        public bool onlyOneBuff;

        //define the defualt config value for supporting modded buffs
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

        //function for populated the allowed buff list
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
