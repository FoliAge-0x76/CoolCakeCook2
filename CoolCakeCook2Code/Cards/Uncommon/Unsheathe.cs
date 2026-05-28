using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Characters;
using CCCook2.CoolCakeCook2Code.Extensions;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Unsheathe() : CCC2_Cards(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) {

    // 拔刀：1c 将5张小刀加入你的手牌。

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new CardsVar(5)
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<Shiv>()
    ];
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay) {
        await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
        await Shiv.CreateInHand(base.Owner, base.DynamicVars.Cards.IntValue, base.CombatState);
    }

    protected override void OnUpgrade() {
        DynamicVars.Cards.UpgradeValueBy(2m);
    }
}
