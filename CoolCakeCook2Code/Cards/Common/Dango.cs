using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Patches.Content;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Characters;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Localization;
using CCCook2.CoolCakeCook2Code.Powers;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Dango() : CCC2_Cards(0, CardType.Skill, CardRarity.Common, TargetType.Self) {

    // 团子：0c 你的下一张饼类攻击费用-1 耐久2
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CustomKeyword.CakeAttack),
        HoverTipFactory.FromKeyword(CustomKeyword.Durability)
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<DecreaseCakeAttackCostPower>(1),
        new DynamicVar("Durability", 2)
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await PowerCmd.Apply<DecreaseCakeAttackCostPower>(
            context,
            base.Owner?.Creature,
            1,
            base.Owner.Creature,
            this
        );

        DynamicVars["Durability"].BaseValue--;
        if (DynamicVars["Durability"].BaseValue <= 0) {
            await CardCmd.Exhaust(context, this);
        }
    }
    protected override void OnUpgrade() {
        DynamicVars["Durability"].UpgradeValueBy(1);
    }
}
