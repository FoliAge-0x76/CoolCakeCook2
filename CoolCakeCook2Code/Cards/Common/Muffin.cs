using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Localization;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.ValueProps;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Muffin() : CCC2_Cards(1, CardType.Skill, CardRarity.Common, TargetType.Self) {

    // 松饼：1c 获得9点格挡 选择弃牌堆中的至多一张牌 去除其附魔
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(9, ValueProp.Move)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block)
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await CommonActions.CardBlock(this, cardPlay);

        CardModel cardModel = (await CardSelectCmd.FromCombatPile(prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 0, 1), context: choiceContext, pile: PileType.Discard.GetPile(base.Owner), player: base.Owner, filter: HasEnchantment)).FirstOrDefault();
        if (cardModel != null) {
            CardCmd.ClearEnchantment(cardModel);
        }
    }
    private bool HasEnchantment(CardModel card) {
        return card.Enchantment != null;
    }
    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(3m);
    }
}
