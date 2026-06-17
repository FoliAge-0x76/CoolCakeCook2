using CCCook2.CoolCakeCook2Code.Extensions;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class LittleStrawberry() : CCC2_Cards(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly) {

    // 小草莓：0c 令一名盟友获得3点能量。随机侵蚀其所有牌。
    public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new EnergyVar(3)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Energy)
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, cardPlay.Target.Player);

        IEnumerable<CardModel> allyHand = cardPlay.Target.Player.PlayerCombatState.AllCards;
        foreach (CardModel card in allyHand) {
            AfflictionModel affliction = AfflictionUtility.GetRandomAffliction();
            await CardCmd.Afflict(affliction, card, 1);
        }
    }
    protected override void OnUpgrade() {
        DynamicVars.Energy.UpgradeValueBy(1m);
    }
}
