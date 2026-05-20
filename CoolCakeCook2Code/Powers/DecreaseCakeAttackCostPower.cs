using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Localization;
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
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System.Threading.Tasks;


namespace CCCook2.CoolCakeCook2Code.Powers;

public sealed class DecreaseCakeAttackCostPower : CCC2_Powers {
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override List<IHoverTip> ExtraHoverTips =>
        [
            HoverTipFactory.FromKeyword(CustomKeyword.CakeAttack)
        ];
    public override bool TryModifyEnergyCostInCombat(CardModel card, decimal originalCost, out decimal modifiedCost) {
        modifiedCost = originalCost;
        if (card.Owner.Creature != base.Owner) {
            return false;
        }
        if (card.Type != CardType.Attack) {
            return false;
        }
        bool flag;
        switch (card.Pile?.Type) {
            case PileType.Hand:
            case PileType.Play:
                flag = true;
                break;
            default:
                flag = false;
                break;
        }
        if (flag) {
            flag = false;
            foreach (var keyword in card.Keywords) {
                if (keyword == CustomKeyword.CakeAttack) {
                    flag = true;
                    break;
                }
            }
        }
        if (!flag) {
            return false;
        }
        modifiedCost = originalCost - 1;
        return true;
    }

    public override async Task BeforeCardPlayed(CardPlay cardPlay) {
        if (cardPlay.Card.Owner.Creature == base.Owner && cardPlay.Card.Type == CardType.Attack) {
            bool flag;
            switch (cardPlay.Card.Pile?.Type) {
                case PileType.Hand:
                case PileType.Play:
                    flag = true;
                    break;
                default:
                    flag = false;
                    break;
            }
            if (flag) {
                flag = false;
                foreach (var keyword in cardPlay.Card.Keywords) {
                    if (keyword == CustomKeyword.CakeAttack) {
                        flag = true;
                        break;
                    }
                }
            }
            if (flag) {
                await PowerCmd.Decrement(this);
            }
        }
    }
}