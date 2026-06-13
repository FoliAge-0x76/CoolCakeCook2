using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using CCCook2.CoolCakeCook2Code.Powers;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Yeast() : CCC2_Cards(0, CardType.Power, CardRarity.Uncommon, TargetType.Self) {

    // 酵母菌：0c 获得3点活力，回合开始时，获得当前活力值一半的活力。
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("VigorAmount",3)
        ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<VigorPower>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await PowerCmd.Apply<VigorPower>(
            context,
            base.Owner?.Creature,
            base.DynamicVars["VigorAmount"].BaseValue,
            base.Owner.Creature,
            this
        );
        await PowerCmd.Apply<YeastPower>(
            context,
            base.Owner?.Creature,
            50,
            base.Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade() {
        DynamicVars["VigorAmount"].UpgradeValueBy(1m);
    }
}
