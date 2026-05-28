using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using CCCook2.CoolCakeCook2Code.Characters;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Powers;
using CCCook2.CoolCakeCook2Code.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class TheEverestCrush() : CCC2_Cards(5, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) {

    // 珠峰压饼：5c 造成325点伤害。
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    public override List<CardKeyword> CanonicalKeywords => [
        CustomKeyword.StrikeAttack
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(325, ValueProp.Move)
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(context);
    }
    protected override void OnUpgrade() {
        base.EnergyCost.UpgradeBy(-1);
    }
}
