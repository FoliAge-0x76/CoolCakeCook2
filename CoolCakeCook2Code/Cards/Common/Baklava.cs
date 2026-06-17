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
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.ValueProps;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Baklava() : CCC2_Cards(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {

    // 酥饼：1c 造成9点伤害 对手牌加料1：迅速1

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(9, ValueProp.Move),
        new CardsVar(1)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CustomKeyword.Seasoning,
        CustomKeyword.CakeAttack
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromEnchantment<Swift>((int)DynamicVars.Cards.BaseValue).First()
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        List<CardModel> list = (await CardSelectCmd.FromHand(
            prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 1),
            context: choiceContext,
            player: base.Owner,
            filter: BaklavableFilter,
            source: this
        )).ToList();

        EnchantmentModel enchantment = ModelDb.Enchantment<Swift>();

        foreach (CardModel item in list) {
            CardCmd.Enchant(enchantment.ToMutable(), item, (int)DynamicVars.Cards.BaseValue);
        }
    }

    private bool BaklavableFilter(CardModel card) {
        return EnchantmentUtility.IsSeasonable<Swift>(card);
    }

    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
