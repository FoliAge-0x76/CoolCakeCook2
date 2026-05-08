using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;

namespace CCCook2.CoolCakeCook2Code.Localization;

public static class CustomKeyword {

    //定义字段：耐久
    [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword Durability;

    //定义字段：打击类攻击
    [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword StrikeAttack;

    //定义字段：饼类攻击
    [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
    public static CardKeyword CakeAttack;

}