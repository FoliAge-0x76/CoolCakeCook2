using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Powers;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class IceLolly() : CCC2_Cards(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) {

    // 雪糕 1c 给予2层缩小 消耗

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ShrinkPower>(2)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ShrinkPower>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await PowerCmd.Apply<ShrinkPower>(
            context,
            cardPlay.Target,
            base.DynamicVars["ShrinkPower"].BaseValue,
            base.Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade() {
        DynamicVars["ShrinkPower"].UpgradeValueBy(1);
    }
}
