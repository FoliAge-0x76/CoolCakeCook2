using BaseLib.Abstracts;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using CCCook2.CoolCakeCook2Code.Cards;
using CCCook2.CoolCakeCook2Code.Extensions;
using System.Threading.Tasks;

namespace CCCook2.CoolCakeCook2Code.Powers;

public abstract class CCC2_Powers : CustomPowerModel {
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();

    public virtual Task AfterCompose(PlayerChoiceContext choiceContext, Player player, CardModel source) {
        return Task.CompletedTask;
    }


}