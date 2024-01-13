using SAA.Content.Foods;

namespace SAA.Content.Placeable.Tiles
{
    public abstract class 垂直植株1x2 : ModTile
    {
        public virtual Color DisplayColor => new Color(243, 153, 26);
        public virtual Vector3 Light => new Vector3(0.25f, 0.15f, 0.02f);
        public virtual Point ItemDrop => new Point(ModContent.ItemType<油果>(), 1);
        public virtual int dusttype => DustID.Grass;
        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.DrawYOffset = -2;
            TileObjectData.addTile(Type);
            HitSound = SoundID.Grass;
            DustType = dusttype;
            AddMapEntry(DisplayColor);
        }
        // This method allows you to determine how much light this block emits
        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = Light.X;
            g = Light.Y;
            b = Light.Z;
        }
        // This method allows you to determine whether or not the tile will draw itself flipped in the world
        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            // Flips the sprite if x coord is odd. Makes the tile more interesting
            if (i % 2 == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;
        }
        // This method allows you to change the sound a tile makes when hit
        //public override bool KillSound(int i, int j, bool fail)
        //{
        //    // Play the glass shattering sound instead of the normal digging sound if the tile is destroyed on this hit
        //    if (!fail)
        //    {
        //        SoundEngine.PlaySound(SoundID.Shatter, new Vector2(i, j).ToWorldCoordinates());
        //        return false;
        //    }
        //    return base.KillSound(i, j, fail);
        //}
        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 16, 32, ItemDrop.X, ItemDrop.Y);
        }
    }
}
