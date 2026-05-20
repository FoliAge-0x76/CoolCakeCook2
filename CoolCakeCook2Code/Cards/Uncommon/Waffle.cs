using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Characters;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Powers;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Waffle() : CCC2_Cards(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {

    // 华夫饼 2c 获得16点格挡 本回合受到的伤害和获得的负面效果将被等量地返还给攻击者/给予者

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(16, ValueProp.Move)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await PowerCmd.Apply<WafflePower>(
            context,
            base.Owner?.Creature,
            base.DynamicVars.Block.BaseValue,
            base.Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(5m);
    }
}
