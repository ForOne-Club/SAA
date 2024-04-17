namespace SAA.Content.Planting.Seeds
{
    public abstract class 种子袋 : Seed
    {
        public override string Texture => "SAA/Content/Planting/Seeds/种子袋";
        protected virtual int ItemType => ItemID.Sunflower;
        public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
        {
            Main.GetItemDrawFrame(ItemType, out var texture, out var rectangle);
            Vector2 neworigin = rectangle.Size() / 2;
            int wh = texture.Width;
            if (texture.Width < texture.Height)
                wh = texture.Height;
            float newscale = (float)24 / wh;
            spriteBatch.Draw(texture, position + new Vector2(Item.width, Item.height) / 2, rectangle, Color.White, 0, neworigin, newscale * scale, SpriteEffects.None, 0);
        }
        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Main.GetItemDrawFrame(ItemType, out var texture, out var rectangle);
            Vector2 neworigin = rectangle.Size() / 2;
            int wh = texture.Width;
            if (texture.Width < texture.Height)
                wh = texture.Height;
            float newscale = (float)24 / wh;
            spriteBatch.Draw(texture, Item.position + new Vector2(Item.width, Item.height) - Main.screenPosition, rectangle, lightColor, rotation, neworigin, newscale * scale, SpriteEffects.None, 0);
        }
    }
}