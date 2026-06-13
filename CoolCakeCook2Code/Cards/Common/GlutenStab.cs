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

public class GlutenStab() : CCC2_Cards(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {

    // 面筋戳：1c 造成10点伤害 对手牌加料1：伶俐2
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(10, ValueProp.Move),
        new BlockVar(2, ValueProp.Unpowered)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CustomKeyword.Seasoning,
        CustomKeyword.StrikeAttack
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromEnchantment<Adroit>((int)DynamicVars.Block.BaseValue).First()
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        List<CardModel> list = (await CardSelectCmd.FromHand(
            prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 1),
            context: choiceContext,
            player: base.Owner,
            filter: GlutenableFilter,
            source: this
        )).ToList();

        EnchantmentModel enchantment = ModelDb.Enchantment<Adroit>();

        foreach (CardModel item in list) {
            CardCmd.Enchant(enchantment.ToMutable(), item, DynamicVars.Block.BaseValue);
        }
    }

    private bool GlutenableFilter(CardModel card) {
        return EnchantmentUtility.IsSeasonable<Adroit>(card);
    }

    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(3m);
        DynamicVars.Block.UpgradeValueBy(1m);
    }
}
