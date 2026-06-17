using CCCook2.CoolCakeCook2Code.Localization;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;

namespace CCCook2.CoolCakeCook2Code.Extensions;
public static class EnchantmentUtility {
    private static readonly Random _random = new Random();

    private static IReadOnlyList<EnchantmentModel> enchantmentList = new List<EnchantmentModel> {
        ModelDb.Enchantment<Adroit>(),
        ModelDb.Enchantment<Clone>(),
        ModelDb.Enchantment<Corrupted>(),
        ModelDb.Enchantment<Glam>(),
        ModelDb.Enchantment<Goopy>(),
        ModelDb.Enchantment<Imbued>(),
        ModelDb.Enchantment<Inky>(),
        ModelDb.Enchantment<Instinct>(),
        ModelDb.Enchantment<Momentum>(),
        ModelDb.Enchantment<Nimble>(),
        ModelDb.Enchantment<PerfectFit>(),
        ModelDb.Enchantment<RoyallyApproved>(),
        ModelDb.Enchantment<Sharp>(),
        ModelDb.Enchantment<Slither>(),
        ModelDb.Enchantment<SlumberingEssence>(),
        ModelDb.Enchantment<SoulsPower>(),
        ModelDb.Enchantment<Sown>(),
        ModelDb.Enchantment<Spiral>(),
        ModelDb.Enchantment<Steady>(),
        ModelDb.Enchantment<Swift>(),
        ModelDb.Enchantment<TezcatarasEmber>(),
        ModelDb.Enchantment<Vigorous>()
    };
    public static EnchantmentModel GetRandomEnchantment(CardModel card) {
        List<EnchantmentModel> goodEnchantments = new List<EnchantmentModel>();
        foreach (EnchantmentModel enchantment in enchantmentList) {
            if (enchantment.CanEnchant(card)) {
                goodEnchantments.Add(enchantment);
            }
        }
        int randomIndex = _random.Next(goodEnchantments.Count);
        return goodEnchantments[randomIndex];
    }

    public static bool IsSeasonable<T>(CardModel card) where T : EnchantmentModel {
        bool flag = false;
        EnchantmentModel enchantment = ModelDb.Enchantment<T>();
        if (card.Type == CardType.Skill || card.Keywords.Contains(CustomKeyword.CakeAttack)) {
            flag = true;
        }
        if (flag) {
            if (!enchantment.CanEnchant(card)) flag = false;
            if (enchantment.Status == EnchantmentStatus.Disabled) flag = false;
        }
        return flag;
    }
}
