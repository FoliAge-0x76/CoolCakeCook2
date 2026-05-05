using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MonoLeaf.CoolCakeCook2Code.Characters;
using MonoLeaf.CoolCakeCook2Code.Extensions;
using MonoLeaf.CoolCakeCook2Code.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoLeaf.CoolCakeCook2Code.Cards;

public class ScallionPancake() : CCC2_Cards(1, CardType.Skill, CardRarity.Common, TargetType.Self) {

    // 油饼：2c 获得6点格挡，每当受到伤害，获得3点格挡。

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(6, ValueProp.Move),
        new DynamicVar("Scallion",3)
    ];

    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];

    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CommonActions.CardBlock(this, cardPlay);

        decimal amount = base.DynamicVars["Scallion"].BaseValue;

        await PowerCmd.Apply<Scallion>(
            base.Owner?.Creature,
            amount,
            base.Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(2m);
        DynamicVars["Scallion"].UpgradeValueBy(1m);
    }
}
