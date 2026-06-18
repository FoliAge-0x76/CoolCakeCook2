using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;

namespace CCCook2.CoolCakeCook2Code.Powers;

public sealed class WafflePower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource) {
        if (!(amount <= 0m) && applier != null && !applier.IsPlayer) {
            if (power.GetTypeForAmount(amount) == PowerType.Debuff) {
                Flash();
                PowerModel powerToApply = ModelDb.GetById<PowerModel>(power.Id).ToMutable();
                await PowerCmd.Apply(choiceContext, powerToApply, applier, amount, base.Owner, cardSource);
            }
        }
    }
    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource) {
        if (target == base.Owner && result.TotalDamage > 0 && props.IsPoweredAttack() && dealer != null) {
            await CreatureCmd.Damage(choiceContext, dealer, result.TotalDamage, ValueProp.Unpowered, base.Owner, null);
        }
    }
    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (side == base.Owner.Side) {
            await PowerCmd.Decrement(this);
        }
    }
}