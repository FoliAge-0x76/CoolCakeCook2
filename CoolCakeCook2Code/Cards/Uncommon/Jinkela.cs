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
using CCCook2.CoolCakeCook2Code.Characters;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Localization;
using CCCook2.CoolCakeCook2Code.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Jinkela() : CCC2_Cards(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) {

    // 金坷垃：1c 回合开始时，每有一种增益获得2点格挡。每当你获得减益时，抽1张牌。
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(2, ValueProp.Move),
        new CardsVar(1)
        ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {

        await PowerCmd.Apply<JinkelaPower>(
            context,
            base.Owner?.Creature,
            base.DynamicVars.Block.BaseValue,
            base.Owner.Creature,
            this
        );
    }
    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(1m);
    }
}
