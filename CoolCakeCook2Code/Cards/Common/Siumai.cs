using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using CCCook2.CoolCakeCook2Code.Characters;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Localization;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoolCakeCook2.CoolCakeCook2Code.Cards.Base;

namespace CCCook2.CoolCakeCook2Code.Cards;

public class Siumai() : CCC2_Cards(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) {

    // 烧卖：0c 去除12点格挡。造成4点伤害。给予1层虚弱。

    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(12, ValueProp.Unpowered),
        new DamageVar(4, ValueProp.Move),
        new PowerVar<WeakPower>(1m)
    ];
    public override List<CardKeyword> CanonicalKeywords => [
        CustomKeyword.CakeAttack
    ];
    protected override List<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.Static(StaticHoverTip.Block),
        HoverTipFactory.FromPower<WeakPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        await CreatureCmd.LoseBlock(cardPlay.Target, base.DynamicVars.Block.BaseValue);
        await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this)
            .Targeting(cardPlay.Target).WithHitFx("vfx/vfx_molten_fist", null, "blunt_attack.mp3").Execute(context);
        await PowerCmd.Apply<WeakPower>(
            context,
            cardPlay.Target,
            base.DynamicVars.Weak.BaseValue,
            base.Owner.Creature,
            this
        );
    }
    protected override void OnUpgrade() {
        DynamicVars.Damage.UpgradeValueBy(1m);
        DynamicVars.Block.UpgradeValueBy(4m);
    }
}
