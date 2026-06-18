using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Localization;
using CCCook2.CoolCakeCook2Code.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.ValueProps;

namespace CCCook2.CoolCakeCook2Code.Cards;

[Pool(typeof(TokenCardPool))]
public sealed class BigWave() : CustomCardModel(2, CardType.Skill, CardRarity.Token, TargetType.Self) {
    public override string CustomPortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigCardImagePath();
    public override string PortraitPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".CardImagePath();
    public override List<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DynamicVar("TurnNumber", 0)
    ];
    private int memoryHp;
    private int memoryMaxHp;
    private int memoryEnergy;
    private int memoryMaxEnergy;
    private IReadOnlyList<PowerModel> memoryPower;
    private IReadOnlyList<CardPile> memoryPiles;

    public static async Task<IEnumerable<CardModel>> CreateInHand(PlayerChoiceContext choiceContext, Player owner, int count, ICombatState combatState) {
        if (count == 0) {
            return Array.Empty<CardModel>();
        }

        if (CombatManager.Instance.IsOverOrEnding) {
            return Array.Empty<CardModel>();
        }

        List<CardModel> waves = new List<CardModel>();
        for (int i = 0; i < count; i++) {
            BigWave wave = combatState.CreateCard<BigWave>(owner);
            wave.Memorize(owner);
            waves.Add(wave);
        }
        await CardPileCmd.AddGeneratedCardsToCombat(waves, PileType.Hand, owner);
        return waves;
    }

    public void Memorize(Player owner) {
        this.memoryHp = owner.Creature.CurrentHp;
        this.memoryMaxHp = owner.Creature.MaxHp;
        this.memoryEnergy = owner.PlayerCombatState.Energy;
        this.memoryMaxEnergy = owner.PlayerCombatState.MaxEnergy;
        this.memoryPower = owner.Creature.Powers.ToList().AsReadOnly();
        //this.memoryPiles = owner.PlayerCombatState.AllPiles;
        DynamicVars["TurnNumber"].BaseValue = owner.PlayerCombatState.TurnNumber; 
    }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        
        await CreatureCmd.SetCurrentHp(Owner.Creature, memoryHp);
        await CreatureCmd.SetMaxHp(Owner.Creature, memoryMaxHp);
        await PlayerCmd.SetEnergy(memoryEnergy, Owner); 
        //await PlayerCmd.SetMaxEnergy(player.PlayerCombatState, memoryMaxEnergy);
        List<PowerModel> toRemove = Owner.Creature.Powers.ToList();
        foreach (var power in toRemove) {
            await PowerCmd.Remove(power);
        }
        foreach (var power in memoryPower) {
            if (power is WavyNoriPower) continue;
            PowerModel powerToApply = ModelDb.GetById<PowerModel>(power.Id).ToMutable();
            await PowerCmd.Apply(context, powerToApply, Owner.Creature, power.Amount, power.Applier, null);
        }
        // TODO: 牌堆恢复逻辑
    }

    protected override void OnUpgrade() {
        AddKeyword(CardKeyword.Retain);
    }
}