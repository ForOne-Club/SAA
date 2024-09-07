using SAA.Content.Foods;
using SAA.Content.NPCs;

namespace SAA.Content.Sys;

public class MatingRecipe
{
    /// <summary>
    /// 较大或大型动物配种配方, 使用配种机交配, 消耗食物较多, 产出动物较少, 用于扩大生产
    /// </summary>
    /// <param name="require"></param>
    /// <param name="requiregroup"></param>
    /// <param name="createtype"></param>
    /// <param name="createamount"></param>
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
    public static void SetSameTypeMatingRecipe(int type, int createamount = 1)
    {
        SetMatingRecipe(new List<int> { type, type }, new List<int>(), type, createamount);
    }

    public static void SetMatingRecipes()
    {
        SetSameTypeMatingRecipe(ModContent.ItemType<奶蜗牛>());
        SetSameTypeMatingRecipe(ModContent.ItemType<血腥奶蜗牛>());
        //SetSameTypeMatingRecipe(ModContent.ItemType<乌贼>());这算是小型动物
        //下面是原版
        SetSameTypeMatingRecipe(2122);//鸭子
        SetSameTypeMatingRecipe(2123);
        SetSameTypeMatingRecipe(4374);//䴙䴘


        SetMatingRecipe(new List<int> { 2122, 2123 }, new List<int>(), 2123);
    }
}
