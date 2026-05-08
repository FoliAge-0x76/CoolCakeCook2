using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Threading.Tasks;


namespace CCCook2.CoolCakeCook2Code.Powers;

public sealed class JinkelaPower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new EnergyVar(1)];
    public override bool IsInstanced => true;

    protected override List<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.Static(StaticHoverTip.Energy)
        ];
    public override async Task BeforePowerAmountChanged(PowerModel power, decimal amount, Creature target, Creature applier, CardModel cardSource) {
        if (!(amount <= 0m) && target == base.Owner) {
            if (power.GetTypeForAmount(amount) == PowerType.Debuff) {
                await CardPileCmd.Draw(new BlockingPlayerChoiceContext(), base.Amount, base.Owner.Player);
            }
            else if (power.GetTypeForAmount(amount) == PowerType.Buff) { 
                await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, base.Owner.Player);
            }
            Flash();
        }
    }

}