using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class RiceBall() : CCC2_Cards(1, CardType.Power, CardRarity.Rare, TargetType.Self) {

    // 糯米团子：1c 清除身上所有负面效果 消耗牌堆中所有状态牌 获得1层人工制品
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ArtifactPower>(1)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
        HoverTipFactory.FromPower<ArtifactPower>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        foreach (var debuff in base.Owner.Creature.Powers.Where(p => p.GetTypeForAmount(p.Amount) == PowerType.Debuff)) {
            await PowerCmd.Remove(debuff);
        }

        foreach (var pile in new[] { PileType.Discard, PileType.Hand, PileType.Draw }) {
            var statusCards = pile.GetPile(base.Owner).Cards.Where((c) => c.Type == CardType.Status).ToList();
            foreach (var statusCard in statusCards) {
                await CardCmd.Exhaust(choiceContext, statusCard);
            }
        }

        await PowerCmd.Apply<ArtifactPower>(
            choiceContext,
            base.Owner?.Creature,
            base.DynamicVars["ArtifactPower"].BaseValue,
            base.Owner.Creature,
            this
        );
    }
    protected override void OnUpgrade() {
        AddKeyword(CardKeyword.Retain);
    }
}
