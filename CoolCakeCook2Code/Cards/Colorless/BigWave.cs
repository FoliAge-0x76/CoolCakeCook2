using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CCCook2.CoolCakeCook2Code.Extensions;
using CCCook2.CoolCakeCook2Code.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

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
    private IReadOnlyList<PowerModel> memoryPower;
    private IReadOnlyList<CardModel> memoryHand;
    private IReadOnlyList<CardModel> memoryDiscard;
    private IReadOnlyList<CardModel> memoryDraw;
    private IReadOnlyList<CardModel> memoryExhaust;

    public static async Task<IEnumerable<CardModel>> CreateInHand(Player owner, int count, ICombatState combatState) {
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
        this.memoryPower = owner.Creature.Powers.ToList().AsReadOnly();
        this.memoryHand = owner.PlayerCombatState.Hand.Cards.ToList().AsReadOnly();
        this.memoryDiscard = owner.PlayerCombatState.DiscardPile.Cards.ToList().AsReadOnly();
        this.memoryDraw = owner.PlayerCombatState.DrawPile.Cards.ToList().AsReadOnly();
        this.memoryExhaust = owner.PlayerCombatState.ExhaustPile.Cards.ToList().AsReadOnly();
        DynamicVars["TurnNumber"].BaseValue = owner.PlayerCombatState.TurnNumber; 
    }
    protected override async Task OnPlay(PlayerChoiceContext context, CardPlay cardPlay) {
        
        await CreatureCmd.SetCurrentHp(Owner.Creature, memoryHp);
        await CreatureCmd.SetMaxHp(Owner.Creature, memoryMaxHp);
        await PlayerCmd.SetEnergy(memoryEnergy, Owner); 
        List<PowerModel> toRemove = Owner.Creature.Powers.ToList();
        foreach (var power in toRemove) {
            await PowerCmd.Remove(power);
        }
        foreach (var power in memoryPower) {
            if (power is WavyNoriPower) continue;
            PowerModel powerToApply = ModelDb.GetById<PowerModel>(power.Id).ToMutable();
            await PowerCmd.Apply(context, powerToApply, Owner.Creature, power.Amount, power.Applier, null);
        }
        int pileIdx = 0;
        List<PileType> piles = new() { PileType.Hand, PileType.Draw, PileType.Discard, PileType.Exhaust };
        List<IReadOnlyList<CardModel>> memoryPiles = new() { memoryHand, memoryDraw, memoryDiscard, memoryExhaust };
        for (int i=0; i<4; i++) {
            var cards = piles[i].GetPile(base.Owner).Cards.ToList();
            foreach (var card in cards) {
                if (piles[i] == PileType.Hand) {
                    await CardCmd.Exhaust(context, card);
                }
                else {
                    card.RemoveFromCurrentPile();
                }
            }
            
            IReadOnlyList<CardModel> memoryPile = memoryPiles[i];
            foreach (var card in memoryPile) {
                CardModel cardToAdd = card;
                await CardPileCmd.Add(cardToAdd, piles[i]);
            }
            pileIdx++;
        }
    }

    protected override void OnUpgrade() {
        AddKeyword(CardKeyword.Retain);
    }
}