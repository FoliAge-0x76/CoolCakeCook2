using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using CCCook2.CoolCakeCook2Code.Localization;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class ThinCake() : CCC2_Cards(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {

    // 薄饼：0c 造成1点伤害 将1张小刀加入你的手牌

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(1, ValueProp.Move),
        new CardsVar(1)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CustomKeyword.CakeAttack
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Shiv>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(context);

        for (int i = 0; i < DynamicVars.Cards.BaseValue; i++) {
            await Shiv.CreateInHand(base.Owner, base.CombatState);
        }
    }

    protected override void OnUpgrade() {
        DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
