using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using MonoLeaf.CoolCakeCook2Code.Characters;
using MonoLeaf.CoolCakeCook2Code.Extensions;
using MonoLeaf.CoolCakeCook2Code.Powers;
using MonoLeaf.CoolCakeCook2Code.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoLeaf.CoolCakeCook2Code.Cards;

public class TheEverestCrush() : CCC2_Cards(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) {

    // 珠峰压饼（用于测试耐久词条）：0c 造成325点伤害。耐久3。
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CustomKeyWords.Durability),
        HoverTipFactory.FromKeyword(CustomKeyWords.StrikeAttack)
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(325, ValueProp.Move),
        new DynamicVar("Durability", 3)
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(context);
        DynamicVars["Durability"].BaseValue--;
        //CurrentDurability--;
        if (DynamicVars["Durability"].BaseValue <= 0) {
            await CardCmd.Exhaust(context, this);
        }
    }
    protected override void OnUpgrade() {
        base.EnergyCost.UpgradeBy(-1);
    }
}
