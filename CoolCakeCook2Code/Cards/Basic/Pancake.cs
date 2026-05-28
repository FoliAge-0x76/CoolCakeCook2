using CCCook2.CoolCakeCook2Code.Localization;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Pancake() : CCC2_Cards(2, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy) {

	// 大饼：2c 造成12点伤害，给予1层易伤。饼类攻击。

	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new DamageVar(12, ValueProp.Move),
        new PowerVar<VulnerablePower>(1m)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
		CustomKeyword.CakeAttack
	];
	protected override List<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<VulnerablePower>()
	];

	protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
		await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
			.Targeting(cardPlay.Target).WithHitFx("vfx/vfx_attack_slash").Execute(context);
		await PowerCmd.Apply<VulnerablePower>(
			context,
			cardPlay.Target,
            DynamicVars.Vulnerable.BaseValue,
			base.Owner.Creature,
			this
		);
	}

	protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(4m);
    }
}
