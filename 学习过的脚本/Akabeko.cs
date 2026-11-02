using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace MegaCrit.Sts2.Core.Models.Relics;


/// <summary>
/// 这是赤牛的脚本
/// </summary>
public sealed class Akabeko : RelicModel
{
	//选稀有度
	public override RelicRarity Rarity => RelicRarity.Uncommon;

	//添加数值
	protected override IEnumerable<DynamicVar> CanonicalVars => new global::_003C_003Ez__ReadOnlySingleElementList<DynamicVar>(new PowerVar<VigorPower>(8m));

	//添加“鼠标悬浮时出现‘活力’的注释”这个功能
	protected override IEnumerable<IHoverTip> ExtraHoverTips => new global::_003C_003Ez__ReadOnlySingleElementList<IHoverTip>(HoverTipFactory.FromPower<VigorPower>());

	//在回合开始时执行
	public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
	{
		if (participants.Contains(base.Owner.Creature) && base.Owner.PlayerCombatState.TurnNumber <= 1)
		{
			Flash();
			await PowerCmd.Apply<VigorPower>(new ThrowingPlayerChoiceContext(), base.Owner.Creature, base.DynamicVars["VigorPower"].IntValue, base.Owner.Creature, null);
		}
	}
}
