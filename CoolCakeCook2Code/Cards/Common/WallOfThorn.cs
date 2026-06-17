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

public class WallOfThorn() : CCC2_Cards(1, CardType.Skill, CardRarity.Common, TargetType.Self) {

    // 刺墙：1c 获得5点格挡 在本回合获得3点荆棘 将1张小刀加入你的手牌
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(5, ValueProp.Move),
        new PowerVar<WallOfThornPower>(3),
        new CardsVar(1)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block),
        HoverTipFactory.FromPower<ThornsPower>(),
        HoverTipFactory.FromCard<Shiv>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CommonActions.CardBlock(this, cardPlay);
        await PowerCmd.Apply<ThornsPower>(context, Owner?.Creature, DynamicVars["WallOfThornPower"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<WallOfThornPower>(context, Owner?.Creature, DynamicVars["WallOfThornPower"].BaseValue, Owner.Creature, this);
        for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++) {
            await Shiv.CreateInHand(base.Owner, base.CombatState);
        }
    }

    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(2m);
        DynamicVars["WallOfThornPower"].UpgradeValueBy(2m);
    }
}
