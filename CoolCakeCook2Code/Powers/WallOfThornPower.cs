using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CCCook2.CoolCakeCook2Code.Powers;

public sealed class WallOfThornPower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override List<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromPower<ThornsPower>()
        ];

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side,
        IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (side == Owner.Side) {
            await PowerCmd.Apply<ThornsPower>(choiceContext, Owner, -Amount, Owner, null);
            await PowerCmd.Remove(this);
        }
    }
}
