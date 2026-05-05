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
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using MonoLeaf.CoolCakeCook2Code.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MonoLeaf.CoolCakeCook2Code.Powers;

public sealed class Scallion : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override bool IsInstanced => false;

    protected override List<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.Static(StaticHoverTip.Block)
        ];

    // 当玩家受到伤害后触发
    public override async Task AfterDamageReceived(PlayerChoiceContext context, Creature target,
        DamageResult result, ValueProp props,
        Creature dealer, CardModel cardSource) {

        Flash();
        if(target != Owner) return;
        await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null, fast: true);
    }

    // 回合结束时触发
    public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side,
        CombatState combatState) {
        if (side == Owner.Side) {
            await PowerCmd.Remove(this); // 清除能力
        }
    }
}