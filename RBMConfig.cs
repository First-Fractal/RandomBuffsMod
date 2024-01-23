using System;
using System.ComponentModel;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader.Config;
using Terraria.ModLoader;

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
        [DefaultValue(120)]
        public int buffDuration;

        //define the defualt config value allowing only one buff
        [DefaultValue(false)]
        public bool onlyOneBuff;

        //define the defualt config value for supporting modded buffs
        [DefaultValue(true)]
        public bool includeModdedBuffs;

        //define the defualt config value for allowing debuffs
        [DefaultValue(true)]
        public bool includeDebuffs;

        //define the defualt config value for allowing pets
        [DefaultValue(true)]
        public bool includePets;

        //define the defualt config value for allowing minecart
        [DefaultValue(true)]
        public bool includeMinecart;

        //function for populated the allowed buff list
        public void updateAllowedBuffs()
        {
            //clear the list
            RBMSystem.allowedBuffs = new List<int>();

            //get a list of all buffs loaded
            for (int i = 0; i <= BuffLoader.BuffCount -1; i++)
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

                //check if the list should not include debuff
                if (!Instance.includeDebuffs)
                {
                    //if the current buffID is a debuff, then ignore it
                    if (Main.debuff[i] == true || Main.pvpBuff[i] == true)
                    {
                        Console.WriteLine("I've skip a buff with the ID: " + i + " cause it's a debuff");
                        continue;
                    }
                }

                //check if the list should not include pets
                if (!Instance.includePets)
                {
                    //if the current buffid is a pet, then ignore it
                    if (Main.vanityPet[i] == true || Main.lightPet[i] == true)
                    {
                        continue;
                    }
                }

                //check if the list should not include minecarts
                if (!Instance.includeMinecart)
                {
                    //if the current buffid is a minecart, then ignore it
                    if (BuffID.Sets.BasicMountData[i] != null)
                    {
                        continue;
                    }
                }

                //add the current buff ID to the list
                RBMSystem.allowedBuffs.Add(i);
            }
        }

        //update the allow buff list when the mod is loaded
        public override void OnLoaded()
        {
            updateAllowedBuffs();
            base.OnLoaded();
        }

        //update the allow buff list when the config has been changed
        public override void OnChanged()
        {
            updateAllowedBuffs();
            base.OnChanged();
        }
    }
}
