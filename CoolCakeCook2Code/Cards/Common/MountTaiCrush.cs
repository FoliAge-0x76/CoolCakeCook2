using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using CCCook2.CoolCakeCook2Code.Localization;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class MountTaiCrush() : CCC2_Cards(3, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {

    // 泰山压饼：3c 保留。造成23点伤害。
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(23, ValueProp.Move)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain,
        CustomKeyword.StrikeAttack
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
    }

    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(9m);
    }
}
