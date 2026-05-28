using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Characters;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Localization;
using CCCook2.CoolCakeCook2Code.Powers;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class RotiPrata() : CCC2_Cards(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {

    // 飞饼：1c 造成9点伤害 选择手牌中的一张牌 在回合结束时放回你的牌堆顶
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(9, ValueProp.Move)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CustomKeyword.StrikeAttack
    ];

    private CardModel returnCardModel = null;
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);

        CardModel cardModel = (await CardSelectCmd.FromHand(prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 1), context: choiceContext, player: base.Owner, filter: null, source: this)).FirstOrDefault();

        if (cardModel != null) { 
            returnCardModel = cardModel;
        }
    }
    public override async Task BeforeFlushLate(PlayerChoiceContext choiceContext, Player player) {
        if (returnCardModel != null && returnCardModel.Pile.Type != PileType.Exhaust) {
            await CardPileCmd.Add(returnCardModel, PileType.Draw, CardPilePosition.Top);
            returnCardModel = null;
        }
    }
    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(3);
    }
}
