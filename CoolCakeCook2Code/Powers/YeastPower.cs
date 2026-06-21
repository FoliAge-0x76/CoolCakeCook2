using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;


namespace CCCook2.CoolCakeCook2Code.Powers;

public sealed class YeastPower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState) {
        if (side == base.Owner.Side) {
            Flash();
            int currentVigor = Owner.GetPower<VigorPower>()?.Amount ?? 0;
            if (currentVigor < 50) {
                decimal vigorToApply = Math.Min(50 - currentVigor, currentVigor * Amount / 100);
                if(vigorToApply == 0) vigorToApply = 1;
                await PowerCmd.Apply<VigorPower>(null, Owner, vigorToApply, Owner, null);
            }
        }
    }
}