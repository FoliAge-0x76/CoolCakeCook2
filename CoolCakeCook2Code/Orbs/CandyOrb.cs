using BaseLib.Abstracts;
using BaseLib.Extensions;
using CCCook2.CoolCakeCook2Code.Extensions;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;

namespace CCCook2.CoolCakeCook2Code.Orbs;

public class CandyOrb : CustomOrbModel {
    private Node2D _visualRoot;
    public override Color DarkenedColor => new Color("796606");

    public override string? CustomIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".OrbImagePath();

    public override decimal PassiveVal => 1m;

    public override decimal EvokeVal => 2m;
    
    protected override string PassiveSfx => "event:/sfx/characters/defect/defect_frost_channel";

    protected override string EvokeSfx => "event:/sfx/characters/defect/defect_frost_channel";

    protected override string ChannelSfx => "event:/sfx/characters/defect/defect_frost_channel";

    public override bool IncludeInRandomPool => false;
    public override Node2D? CreateCustomSprite() {
        var scene = PreloadManager.Cache.GetScene($"{Id.Entry.RemovePrefix().ToLowerInvariant()}.tscn".ScenePath());
        if (scene == null) {
            GD.PushError("CandyOrb: Failed to load visual scene.");
            return null;
        }
        _visualRoot = scene.Instantiate<Node2D>();

        return _visualRoot;
    }
    public override async Task BeforeTurnEndOrbTrigger(PlayerChoiceContext choiceContext) {
        await Passive(choiceContext, null);
    }

    public override async Task Passive(PlayerChoiceContext choiceContext, Creature? target) {
        Trigger();
        await CreatureCmd.Heal(base.Owner.Creature, PassiveVal);
    }

    public override async Task<IEnumerable<Creature>> Evoke(PlayerChoiceContext playerChoiceContext) {
        await CreatureCmd.Heal(base.Owner.Creature, EvokeVal);
        return new[] { base.Owner.Creature };
    }
}