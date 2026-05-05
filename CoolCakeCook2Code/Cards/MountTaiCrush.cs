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
using MegaCrit.Sts2.Core.ValueProps;
using MonoLeaf.CoolCakeCook2Code.Characters;
using MonoLeaf.CoolCakeCook2Code.Extensions;
using MonoLeaf.CoolCakeCook2Code.Localization;
using MonoLeaf.CoolCakeCook2Code.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoLeaf.CoolCakeCook2Code.Cards;

public class MountTaiCrush() : CCC2_Cards(3, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {

    // 泰山压饼：3c 保留。造成23点伤害。

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(23, ValueProp.Move)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CustomKeyWords.StrikeAttack)
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CommonActions.CardAttack(this, cardPlay).Execute(context);
    }

    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(5m);
    }
}
