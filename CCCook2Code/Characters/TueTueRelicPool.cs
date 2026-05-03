using BaseLib.Abstracts;
using MonoLeaf.CCCook2Code.Extensions;
using Godot;

namespace MonoLeaf.CCCook2Code.Characters;

public class TueTueRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => TueTue.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}