using CCCook2.CoolCakeCook2Code.Localization;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Enchantments;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Afflictions;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Powers;

namespace CCCook2.CoolCakeCook2Code.Extensions;

public static class AfflictionUtility {
    private static readonly Random _random = new Random();

    private static IReadOnlyList<AfflictionModel> afflictionList = new List<AfflictionModel> {
        ModelDb.Affliction<Bound>(),
        ModelDb.Affliction<Entangled>(),
        ModelDb.Affliction<Galvanized>(),
        ModelDb.Affliction<Hexed>(),
        ModelDb.Affliction<Ringing>(),
        ModelDb.Affliction<Smog>(),
        ModelDb.Affliction<Tainted>()
    };

    public static AfflictionModel GetRandomAffliction() {
        int randomIndex = _random.Next(afflictionList.Count);
        return afflictionList[randomIndex];
    }
    public static bool isAfflictionPower(PowerModel power) {
        if (power is ChainsOfBindingPower ||
            power is TangledPower ||
            power is HexPower ||
            power is RingingPower ||
            power is SmoggyPower ||
            power is TaintedPower) {
            return true;
        }
        return false;
    }
}
