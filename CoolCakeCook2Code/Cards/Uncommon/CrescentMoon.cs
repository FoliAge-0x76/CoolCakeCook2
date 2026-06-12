using CCCook2.CoolCakeCook2Code.Orbs;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class CrescentMoon() : CCC2_Cards(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {

    // 新月：1c 生成一颗冰糖充能球。消耗

    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Channeling),
        HoverTipFactory.FromOrb<CandyOrb>()
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await OrbCmd.Channel<CandyOrb>(choiceContext, base.Owner);
    }

    protected override void OnUpgrade() {
        base.EnergyCost.UpgradeBy(-1);
    }
}
