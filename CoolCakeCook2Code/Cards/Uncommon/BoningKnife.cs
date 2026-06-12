using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Localization;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class BoningKnife() : CCC2_Cards(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) {

    // 剔骨刀：2c 造成6点伤害，将2张随机附魔的小刀加入手牌。
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(6, ValueProp.Move),
        new CardsVar(2)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CustomKeyword.StrikeAttack
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Shiv>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
        foreach (CardModel item in await Shiv.CreateInHand(base.Owner, base.DynamicVars.Cards.IntValue, base.CombatState)) {
            EnchantmentModel enchantment = EnchantmentUtility.GetRandomEnchantment(item);
            int count = 1;
            if (enchantment.IsStackable) count = 3;
            CardCmd.Enchant(enchantment.ToMutable(), item, count);
        }
    }
    protected override void OnUpgrade() {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}
