using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
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
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System.Threading.Tasks;
using static Godot.HttpRequest;


namespace CCCook2.CoolCakeCook2Code.Powers;

public sealed class MagicDoughPower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    private decimal _damageReceivedThisTurn;
    private decimal DamageReceivedThisTurn {
        get {
            return _damageReceivedThisTurn;
        }
        set {
            AssertMutable();
            _damageReceivedThisTurn = value;
        }
    }

    public override decimal ModifyHpLostAfterOstyLate(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource) {
        if (target != base.Owner) {
            return amount;
        }
        DamageReceivedThisTurn += amount;
        return 0m;
    }

    public override async Task AfterModifyingHpLostAfterOsty() {
        Flash();
        await PowerCmd.Apply<NextTurnDamagePower>(null, Owner, DamageReceivedThisTurn, Owner, null);
        DamageReceivedThisTurn = 0m;
    }

    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side,
    IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (side == Owner.Side) {
            await PowerCmd.Decrement(this);
        }
    }
}