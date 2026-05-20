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

public sealed class JinkelaPower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override List<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.Static(StaticHoverTip.Block)
        ];
    public override async Task BeforePowerAmountChanged(PowerModel power, decimal amount, Creature target, Creature applier, CardModel cardSource) {
        if (!(amount <= 0m) && target == base.Owner) {
            if (power.GetTypeForAmount(amount) == PowerType.Debuff) {
                Flash();
                await CardPileCmd.Draw(new BlockingPlayerChoiceContext(), 1, base.Owner.Player);
            }
        }
    }
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player) {
        if (player == base.Owner.Player) {
            
            int blockgain = 0;
            IReadOnlyList<PowerModel> powers = base.Owner.Powers;
            foreach (PowerModel power in powers) {
                if (power.Type == PowerType.Buff && power.Amount > 0) {
                    blockgain += base.Amount;
                }
            }

            if(blockgain > 0) {
                Flash();
                await CreatureCmd.GainBlock(base.Owner, blockgain, ValueProp.Unpowered, null);
            }
        }
    }
}