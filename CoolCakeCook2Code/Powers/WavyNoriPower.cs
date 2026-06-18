using CCCook2.CoolCakeCook2Code.Cards;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;


namespace CCCook2.CoolCakeCook2Code.Powers;

public sealed class WavyNoriPower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromCard<BigWave>()
    ];
    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource) {
        if (target == base.Owner && props.IsPoweredAttack() && dealer != null) {
            await PowerCmd.Decrement(this);
        }
    }
    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side,
    IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (side == Owner.Side) {
            await BigWave.CreateInHand(choiceContext, base.Owner.Player, 1, combatState);
            await PowerCmd.Decrement(this);
        }
    }
}