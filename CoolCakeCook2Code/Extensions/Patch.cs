using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;

[HarmonyPatch(typeof(EnchantmentModel), "get_IsStackable")]
public static class IsStackable_Fix {
    
    [HarmonyPrefix]
    public static bool Prefix(EnchantmentModel __instance, ref bool __result) {
        if (__instance is Adroit ||
            __instance is Momentum ||
            __instance is Nimble ||
            __instance is Swift ||
            __instance is Sharp ||
            __instance is Vigorous
            ) {
            __result = true;
            return false;
        }
        return true; 
    }
}
