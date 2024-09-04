using SAA.Content.NPCs;

namespace SAA.Content.Sys;

public class MatingRecipe
{
    public static void SetMatingRecipe(List<int> require, List<int> requiregroup, int createtype, int createamount = 1)
    {
        int c = require.Count + requiregroup.Count;
        if (c != 2) return;//不能多于两种材料, 毕竟是交配
        List<Point> Require = new();
        foreach (int i in require)
        {
            Require.Add(new Point(i, 1));
        }
        List<Point> Requiregroup = new();
        foreach (int i in requiregroup)
        {
            Require.Add(new Point(i, 1));
        }
        CookSystem.PotCookRecipe.Add(new RecipeStore(Require, Requiregroup, new Point(createtype, createamount), ModContent.TileType<Breeding.Tiles.配种机>()));
    }
    public static void SetMatingRecipes()
    {
        SetMatingRecipe(new List<int> { ModContent.ItemType<奶蜗牛>(), ModContent.ItemType<奶蜗牛>() }, new List<int>(), ModContent.ItemType<奶蜗牛>());
    }
}
