using SAA.Content.Sys;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace SAA.Content.Commands;

public class HungerCutCommand : ModCommand
{
    // CommandType.Chat是指命令可以在单人和多人的聊天中使用
    public override CommandType Type
		=> CommandType.Chat;

    // 触发此命令所需的文本
    public override string Command
		=> "HungerCut";

    // 此命令的简短用法说明
    public override string Usage
		=> "/HungerCut [value]" +
        "\n value — 设定数值";

	// A short description of this command
	public override string Description
		=> "增加或减少自己的饱食度";

	public override void Action(CommandCaller caller, string input, string[] args) {
        // 检查输入参数
        if (args.Length == 0)
			throw new UsageException("value数据缺失");
		// 如果不能解析int，则意味着命令使用错误。
		if (!int.TryParse(args[0], out int value))
        {
            throw new UsageException("value数据填写错误");
        }
        caller.Player.GetModPlayer<HungerforPlayer>().Hunger -= value;
    }
}