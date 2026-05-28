using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Localization;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Kiri() : CCC2_Cards(1, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies) {

    // 斩：1c 消耗所有牌堆中的小刀，对所有敌人造成消耗小刀数量6倍的伤害。
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    public override List<CardKeyword> CanonicalKeywords => [
        CustomKeyword.StrikeAttack
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Shiv>()
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CalculationBaseVar(0m),
        new CalculationExtraVar(6m),
        new CalculatedVar("CalculatedDamage").WithMultiplier((card, _) => 
            PileType.Discard.GetPile(card.Owner).Cards.Count((c) => c.Tags.Contains(CardTag.Shiv)) +
            PileType.Hand.GetPile(card.Owner).Cards.Count((c) => c.Tags.Contains(CardTag.Shiv)) + 
            PileType.Draw.GetPile(card.Owner).Cards.Count((c) => c.Tags.Contains(CardTag.Shiv))
        )
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        decimal damage = ((CalculatedVar)DynamicVars["CalculatedDamage"]).Calculate(cardPlay.Target);

        await CommonActions.CardAttack(this, cardPlay.Target, damage).Execute(choiceContext);

        foreach (var pile in new[] { PileType.Discard, PileType.Hand, PileType.Draw }) {
            var shivs = pile.GetPile(base.Owner).Cards.Where((c) => c.Tags.Contains(CardTag.Shiv)).ToList();
            foreach (var shiv in shivs) {
                await CardCmd.Exhaust(choiceContext,shiv);
            }
        }
    }
    protected override void OnUpgrade() {
        DynamicVars.CalculationExtra.UpgradeValueBy(2m);
    }
}
