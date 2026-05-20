using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Localization;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace CCCook2.CoolCakeCook2Code.Powers;

public sealed class LasagnaPower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromKeyword(CustomKeyword.StrikeAttack)
    ];
    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay) {
        if (cardPlay.Card.Owner.Creature == base.Owner && (cardPlay.Card.Tags.Contains(CardTag.Strike) || cardPlay.Card.Tags.Contains(CardTag.Shiv))) {
            await CreatureCmd.GainBlock(base.Owner, Amount, ValueProp.Unpowered, null, fast: true);
        }
    }

}