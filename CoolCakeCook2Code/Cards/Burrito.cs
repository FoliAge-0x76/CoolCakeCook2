using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MonoLeaf.CoolCakeCook2Code.Characters;
using MonoLeaf.CoolCakeCook2Code.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoLeaf.CoolCakeCook2Code.Cards;

public class Burrito() : CCC2_Cards(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) {

    // 卷饼：1c 给予1/2层虚弱。若这名敌人的意图是攻击，抽3张牌。

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<WeakPower>(1m),
        new CardsVar(3)
    ];

    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<WeakPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await PowerCmd.Apply<WeakPower>(
            cardPlay.Target,
            base.DynamicVars.Weak.BaseValue,
            base.Owner.Creature,
            this
        );
        if(cardPlay.Target.Monster.IntendsToAttack) {
            await CardPileCmd.Draw(context, base.DynamicVars.Cards.BaseValue, base.Owner);
        }
    }

    protected override void OnUpgrade() {
        DynamicVars.Weak.UpgradeValueBy(1m);
    }
}
