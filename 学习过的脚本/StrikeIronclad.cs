using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace MegaCrit.Sts2.Core.Models.Cards;

/// <summary>
/// The only difference between the starting Strike cards are portrait, attack vfx, and color.
/// </summary>
public sealed class StrikeIronclad : CardModel
{
	//其他卡牌使用此卡牌时，会查询的标签。如完美打击会查询“打击”标签。此标签不应该像“附魔”一样给卡牌添加新能力，它只应该作为其他卡牌或遗物的使用标签
	protected override HashSet<CardTag> CanonicalTags => new HashSet<CardTag> { CardTag.Strike }; 
	//将卡牌会造成的伤害，buff等放在IEnumberable这个组合里，
	protected override IEnumerable<DynamicVar> CanonicalVars => new global::_003C_003Ez__ReadOnlySingleElementList<DynamicVar>(new DamageVar(6m, ValueProp.Move));

	/// <summary>
	/// The only difference between the starting Strike cards are portrait, attack vfx, and color.
	/// </summary>
	
	//调用父类的构造函数，添加卡牌的属性，比如费用，类型，稀有度
	public StrikeIronclad()
		: base(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
	{
	}

	//在判断指向的目标不为空后，使用链式调用来进行卡牌配置
	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
		await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
			.WithHitFx("vfx/vfx_attack_slash")
			.Execute(choiceContext);
	}

	//升级后会有什么作用
	protected override void OnUpgrade()
	{
		base.DynamicVars.Damage.UpgradeValueBy(3m);
	}
}
