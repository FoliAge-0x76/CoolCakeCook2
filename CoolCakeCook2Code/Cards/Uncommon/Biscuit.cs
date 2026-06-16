using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using CCCook2.CoolCakeCook2Code.Powers;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Biscuit() : CCC2_Cards(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) {

    // 饼干：1c 获得3层多层护甲。
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<PlatedArmorPower>(3)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<PlatedArmorPower>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {

        await PowerCmd.Apply<PlatedArmorPower>(
            context,
            base.Owner?.Creature,
            base.DynamicVars["PlatedArmorPower"].BaseValue,
            base.Owner.Creature,
            this
        );
    }
    protected override void OnUpgrade() {
        DynamicVars["PlatedArmorPower"].UpgradeValueBy(1m);
    }
}
