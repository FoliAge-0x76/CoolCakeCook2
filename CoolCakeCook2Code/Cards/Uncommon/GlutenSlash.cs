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

public class GlutenSlash() : CCC2_Cards(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) {

    // 面筋劈：2c 对所有敌人造成11点伤害 对手牌加料2：伶俐2
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(11, ValueProp.Move),
        new CardsVar(2),
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
            prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, (int)base.DynamicVars.Cards.BaseValue),
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
        DynamicVars.Damage.UpgradeValueBy(2m);
        DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
