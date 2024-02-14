using System.ComponentModel;
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
        [Header("$Mods.RandomBuffsMod.Configs.Header.GeneralOptions")]

        //define the defualt config value for buff duration
        [DefaultValue(120)]
        public int buffDuration;

        //define the defualt config value allowing only one buff
        [DefaultValue(false)]
        public bool onlyOneBuff;

        //define the defualt config value for supporting modded buffs
        [DefaultValue(true)]
        public bool includeModdedBuffs;

        [Header("$Mods.RandomBuffsMod.Configs.Header.AllowedBuffsOptions")]

        //define the defualt config value for allowing debuffs
        [DefaultValue(true)]
        public bool includeDebuffs;

        //define the defualt config value for allowing pets
        [DefaultValue(true)]
        public bool includePets;

        //define the defualt config value for allowing minecart
        [DefaultValue(true)]
        public bool includeMinecart;

        //define the defualt config value for allowing potion and mana sickness
        [DefaultValue(false)]
        public bool includePotionAndManaSickness;
    }
}
