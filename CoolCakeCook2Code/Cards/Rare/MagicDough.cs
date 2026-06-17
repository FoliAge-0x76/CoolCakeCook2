using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using CCCook2.CoolCakeCook2Code.Localization;
using CCCook2.CoolCakeCook2Code.Powers;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class MagicDough() : CCC2_Cards(1, CardType.Skill, CardRarity.Rare, TargetType.Self) {

    // 魔法面团：1c 保留。阻止本回合受到的未被格挡的伤害 并在下回合结束时受到等量伤害 余音2

    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CustomKeyword.Aftertone)
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("Aftertone", 2)
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {

        await PowerCmd.Apply<MagicDoughPower>(
            context,
            base.Owner?.Creature,
            1,
            base.Owner.Creature,
            this
        );

        DynamicVars["Aftertone"].BaseValue--;
        if (DynamicVars["Aftertone"].BaseValue <= 0) {
            await CardCmd.Exhaust(context, this);
        }
    }

    protected override void OnUpgrade() {
        base.EnergyCost.UpgradeBy(-1);
    }
}
