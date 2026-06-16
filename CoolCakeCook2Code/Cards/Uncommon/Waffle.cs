using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Powers;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Waffle() : CCC2_Cards(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {

    // 华夫饼 2c 获得13点格挡 本回合受到的伤害和获得的负面效果将被等量地返还给攻击者/给予者

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(13, ValueProp.Move)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CommonActions.CardBlock(this, cardPlay);

        await PowerCmd.Apply<WafflePower>(
            context,
            base.Owner?.Creature,
            1,
            base.Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(5m);
    }
}
