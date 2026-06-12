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

public class WheatCake() : CCC2_Cards(2, CardType.Skill, CardRarity.Common, TargetType.Self) {

    // 麦饼：2c 获得12点格挡 加料：为1张手牌附魔稳定。
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(12, ValueProp.Move)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block),
        HoverTipFactory.FromKeyword(CustomKeyword.Seasoning),
        HoverTipFactory.FromEnchantment<Steady>().First()
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await CommonActions.CardBlock(this, cardPlay);

        List<CardModel> list = (await CardSelectCmd.FromHand(
            prefs: new CardSelectorPrefs(base.SelectionScreenPrompt, 1),
            context: choiceContext,
            player: base.Owner,
            filter: SaltibleFilter,
            source: this
        )).ToList();

        EnchantmentModel enchantment = ModelDb.Enchantment<Steady>();

        foreach (CardModel item in list) {
            CardCmd.Enchant(enchantment.ToMutable(), item, 1);
        }
    }
    private bool SaltibleFilter(CardModel card) {
        return EnchantmentUtility.IsSeasonable<Steady>(card);
    }
    protected override void OnUpgrade() {
        DynamicVars.Block.UpgradeValueBy(4m);
    }
}
