using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace RandomBuffsMod
{
	public class RandomBuffsMod : Mod
	{
        //define the utilities classes
        static FFLib ff = new FFLib();

        //tell the mod what to do when it reccive a packet
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            RBMPlayer PBMPlayer = Main.CurrentPlayer.GetModPlayer<RBMPlayer>();
            PBMPlayer.randomBuffID = reader.ReadInt32();
        }
    }
}