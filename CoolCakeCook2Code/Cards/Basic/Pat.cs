using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Characters;
using CCCook2.CoolCakeCook2Code.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using CCCook2.CoolCakeCook2Code.Localization;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Pat() : CCC2_Cards(1,CardType.Attack,CardRarity.Basic,TargetType.AnyEnemy) {

	protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];
	public override List<CardKeyword> CanonicalKeywords => [
		CustomKeyword.StrikeAttack
	];
	protected override async Task OnPlay(PlayerChoiceContext context,CardPlay cardPlay) {
		await CommonActions.CardAttack(this, cardPlay).Execute(context);
	}

	protected override void OnUpgrade() {
		DynamicVars.Damage.UpgradeValueBy(3m);
	}
}
