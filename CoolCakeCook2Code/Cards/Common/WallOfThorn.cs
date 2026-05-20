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

public class WallOfThorn() : CCC2_Cards(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {

    // 刺墙：1c 获得6点格挡 造成4点伤害 在本回合获得2点荆棘
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(6, ValueProp.Move),
        new DamageVar(4, ValueProp.Move),
        new PowerVar<WallOfThornPower>(2),
        new CardsVar(0)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CustomKeyword.StrikeAttack
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block),
        HoverTipFactory.FromPower<ThornsPower>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(context);
        await CommonActions.CardBlock(this, cardPlay);
        await PowerCmd.Apply<ThornsPower>(context, Owner?.Creature, DynamicVars["WallOfThornPower"].BaseValue, Owner.Creature, this);
        await PowerCmd.Apply<WallOfThornPower>(context, Owner?.Creature, DynamicVars["WallOfThornPower"].BaseValue, Owner.Creature, this);
        for (int i = 0; i < base.DynamicVars.Cards.IntValue; i++) {
            await Shiv.CreateInHand(base.Owner, base.CombatState);
        }
    }

    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(1m);
        DynamicVars.Damage.UpgradeValueBy(1m);
        DynamicVars["WallOfThornPower"].UpgradeValueBy(1m);
        DynamicVars.Cards.UpgradeValueBy(1m);
        ExtraHoverTips.Add(HoverTipFactory.FromCard<Shiv>());
    }
}
