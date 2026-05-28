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
using CCCook2.CoolCakeCook2Code.Characters;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Localization;
using CCCook2.CoolCakeCook2Code.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class MagicDough() : CCC2_Cards(0, CardType.Skill, CardRarity.Rare, TargetType.Self) {

    // 魔法面团：0c 阻止本回合受到的未被格挡的伤害 并在下回合结束时受到等量伤害 消耗

    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {

        await PowerCmd.Apply<MagicDoughPower>(
            context,
            base.Owner?.Creature,
            1,
            base.Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade() {
        AddKeyword(CardKeyword.Retain);
    }
}
