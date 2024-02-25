namespace SAA.Content.DamageClasses;

public class BotanistDamageClass : DamageClass
{
    //这是一个damage类的例子，用来演示当前所有的功能，并解释如何创建一个你自己的，如果你需要的话。
    //有关如何将属性加成应用于特定伤害类别的信息，请参阅examplemod /Content/Items/Accessories/ExampleStatBonusAccessory。
    public override LocalizedText DisplayName => Language.GetOrRegister("Mods.SAA.DamageClass.Botanist");

    public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
    {
        //这个方法可以让你让你的伤害职业受益于其他职业的默认属性奖励，以及通用属性奖励。
        //简单总结一下DamageClass使用的两个非标准的损害类名:
        //你猜对了，Default是默认的损害类。它不会影响任何特定职业的属性奖励或普遍属性奖励。
        //有很多物品和投射物都使用这个，比如抛出的水和Bone Glove的骨头。
        //通用，另一方面，所有通用属性加成的比例，没有其他;它是基础伤害类，所有其他非默认类型都是建立在此基础上的。
        if (damageClass == DamageClass.Generic)
            return StatInheritanceData.Full;

        return new StatInheritanceData(
            damageInheritance: 0f,
            critChanceInheritance: 0f,
            attackSpeedInheritance: 0f,
            armorPenInheritance: 0f,
            knockbackInheritance: 0f
        );
        //你可能会问，我们刚才到底做了什么?嗯，让我看看……
        // StatInheritanceData是一个结构体，对于这个方法的任何给定结果，你都需要返回其中一个。
        //通常情况下，后两者将被写成“StatInheritanceData”。没有”，而不是手工打出来……
        //……但为了清楚起见，我们把它写出来，并按顺序标记每个参数; 它们应该是不言自明的。
        //为了解释这些返回值是如何工作的，每个返回值都表现得像一个百分比，其中0f为0%，1f为100%，等等。
        //返回值表明你的类将在多大程度上缩放问题的属性对于任何伤害类(es)你已经返回它。
        //如果你创建一个没有任何参数的StatInheritanceData，所有参数都将被设置为1f。
        //例如，如果我们为damageclass . range提供一个假设的返回值…
        /*
			if (damageClass == DamageClass.Ranged)
				return new StatInheritanceData(
					damageInheritance: 1f,
					critChanceInheritance: -1f,
					attackSpeedInheritance: 0.4f,
					armorPenInheritance: 2.5f,
					knockbackInheritance: 0f
				);
			*/
        //这将允许我们的自定义职业受益于以下远程属性加成:
        // -伤害，100%有效
        // -攻击速度，40%的效果
        // -100%的暴击率(这意味着任何提高远程暴击率的东西都会降低我们自定义职业的暴击率)
        // -穿甲效果为250%

        //注意:设置这些值没有硬性限制。请注意，无论你给他们设置了什么，都可能会产生意想不到的后果，
        //并且我们不对由于你病态的好奇心而对你、你的性格或你的世界造成的任何暂时或永久的损害负责。
        //要引用这些类型的非香草伤害类，使用“ModContent.GetInstance<TargetDamageClassHere>()”代替“DamageClass.XYZ”。
    }

    public override bool GetEffectInheritance(DamageClass damageClass)
    {
        //此方法允许你使你的伤害职业受益，并能够激活其他职业的效果(如幽灵螺栓，岩浆石)基于返回true。
        //注意，不像我们上面的属性继承方法，你不需要在这个方法中考虑普遍的奖励。
        //对于这个例子，我们将使我们的类能够激活混战和魔法特效。
        if (damageClass == DamageClass.Melee)
            return true;
        if (damageClass == DamageClass.Magic)
            return true;

        return false;
    }

    public override void SetDefaultStats(Player player)
    {
        //此方法允许您为示例damage类设置默认的统计修饰符。
        //在这里，我们将使我们的例子伤害职业有更多的暴击几率和护甲穿透比正常。
        player.GetCritChance<BotanistDamageClass>() += 4;
        //player.GetArmorPenetration<BotanistDamageClass>() += 10;
        //这些类型的修改也存在于伤害(GetDamage)，击退(GetKnockback)和攻击速度(GetAttackSpeed)。
        //你会看到这些在香草类和我们的示例类的引用中到处使用。熟悉它们。
    }

    //这个属性让你决定你的伤害类别是否可以使用标准的暴击计算。
    //注意，将其设置为false也会阻止显示暴击几率的提示行。
    //这个预防会推翻ShowStatTooltipLine设置的任何内容，所以要小心!
    public override bool UseStandardCritCalcs => true;

    public override bool ShowStatTooltipLine(Player player, string lineName)
    {
        //这个方法可以防止某些常见的统计提示线出现在与这个DamageClass相关的项上。
        //你可以使用的四个行名是“伤害”，“暴击几率”，“速度”和“击退”。所有四种情况默认为true，因此将显示。例如……
        //if (lineName == "Speed")
        //    return false;

        return true;
        //请注意，这个钩子不会永远在这里;直到即将到来的对工具提示的整体修改出现。
        //一旦发生这种情况，一个更好的，更通用的解释如何实现它将被展示，这个钩子将被删除。
    }
}