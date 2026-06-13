using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Unsheathe() : CCC2_Cards(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {

    // 拔刀：1c 将4张小刀加入你的手牌。

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(4)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Shiv>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await Shiv.CreateInHand(base.Owner, base.DynamicVars.Cards.IntValue, base.CombatState);
    }

    protected override void OnUpgrade() {
        DynamicVars.Cards.UpgradeValueBy(2m);
    }
}
