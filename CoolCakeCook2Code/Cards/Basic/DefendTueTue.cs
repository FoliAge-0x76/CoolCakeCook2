using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class DefendTueTue() : CCC2_Cards(1, CardType.Skill, CardRarity.Basic, TargetType.Self) {

    // 防御：1c 获得5点格挡。
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(5, ValueProp.Move)
    ];
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];

    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CommonActions.CardBlock(this, cardPlay);
    }

    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(3m);
    }
}
