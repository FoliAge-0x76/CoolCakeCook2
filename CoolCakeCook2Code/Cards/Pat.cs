using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MonoLeaf.CoolCakeCook2Code.Characters;
using MonoLeaf.CoolCakeCook2Code.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using System.Collections.Generic;
using System.Threading.Tasks;
using MonoLeaf.CoolCakeCook2Code.Localization;

namespace MonoLeaf.CoolCakeCook2Code.Cards;

public class Pat() : CCC2_Cards(1,CardType.Attack,CardRarity.Basic,TargetType.AnyEnemy) {

	protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

	protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(6, ValueProp.Move)];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CustomKeyWords.StrikeAttack)
	];
    protected override async Task OnPlay(PlayerChoiceContext context,CardPlay cardPlay) {
		await CommonActions.CardAttack(this, cardPlay).Execute(context);
	}

	protected override void OnUpgrade() {
		DynamicVars.Damage.UpgradeValueBy(3m);
	}
}
