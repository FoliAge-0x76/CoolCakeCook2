using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Localization;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace CCCook2.CoolCakeCook2Code.Cards;

[Pool(typeof(TokenCardPool))]
public sealed class Seasoning() : CustomCardModel(1, CardType.Status, CardRarity.Token, TargetType.Self) {
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    protected override HashSet<CardTag> CanonicalTags => [CustomTag.Seasoning];
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Retain,
        CardKeyword.Exhaust
    ];
    public static async Task<CardModel?> CreateInPile(PlayerChoiceContext choiceContext, Player owner, PileType pile, ICombatState combatState) {
        return (await CreateInPile(choiceContext, owner, 1, pile, combatState)).FirstOrDefault();
    }
    public static async Task<IEnumerable<CardModel>> CreateInPile(PlayerChoiceContext choiceContext, Player owner, int count, PileType pile, ICombatState combatState) {
        if (count == 0) {
            return Array.Empty<CardModel>();
        }

        if (CombatManager.Instance.IsOverOrEnding) {
            return Array.Empty<CardModel>();
        }

        List<CardModel> seasonings = new List<CardModel>();
        for (int i = 0; i < count; i++) {
            seasonings.Add(combatState.CreateCard<Seasoning>(owner));
        }
        await CardPileCmd.AddGeneratedCardsToCombat(seasonings, pile, owner);
        return seasonings;
    }

    protected override void OnUpgrade() {
        base.EnergyCost.UpgradeBy(-1);
    }
}