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
using MegaCrit.Sts2.Core.ValueProps;
using System.Threading.Tasks;


namespace CCCook2.CoolCakeCook2Code.Powers;

public sealed class WafflePower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task BeforePowerAmountChanged(PowerModel power, decimal amount, Creature target, Creature applier, CardModel cardSource) {
        if (!(amount <= 0m) && target == base.Owner) {
            if (power.GetTypeForAmount(amount) == PowerType.Debuff) {
                Flash();
                await PowerCmd.Apply(null, power, applier, amount, base.Owner, null);
            }
        }
    }
    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature? dealer, CardModel? cardSource) {
        if (target == base.Owner && result.TotalDamage > 0 && props.IsPoweredAttack() && dealer != null) {
            await CreatureCmd.Damage(choiceContext, dealer, result.TotalDamage, ValueProp.Unpowered, base.Owner, null);
        }
    }
    public override async Task AfterSideTurnStart(CombatSide side, ICombatState combatState) {
        if (side == base.Owner.Side) {
            await PowerCmd.Decrement(this);
        }
    }
}