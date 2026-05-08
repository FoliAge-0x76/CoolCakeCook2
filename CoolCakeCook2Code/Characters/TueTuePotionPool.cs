using BaseLib.Abstracts;
using CCCook2.CoolCakeCook2Code.Extensions;
using Godot;

namespace CCCook2.CoolCakeCook2Code.Characters;

public class TueTuePotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => TueTue.Color;
    

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}