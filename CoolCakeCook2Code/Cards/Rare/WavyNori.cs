using CCCook2.CoolCakeCook2Code.Orbs;
using CCCook2.CoolCakeCook2Code.Powers;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class WavyNori() : CCC2_Cards(1, CardType.Skill, CardRarity.Rare, TargetType.Self) {

    // 波力海苔：1c 获得1层波浪。消耗。

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<WavyNoriPower>(1)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<WavyNoriPower>(),
        HoverTipFactory.FromCard<BigWave>()
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {

        await PowerCmd.Apply<WavyNoriPower>(
            context,
            base.Owner?.Creature,
            base.DynamicVars["WavyNoriPower"].BaseValue,
            base.Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade() {
        DynamicVars["WavyNoriPower"].UpgradeValueBy(1);
    }
}
