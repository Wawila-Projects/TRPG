using Assets.Scripts.Interfaces.Characters;
using UnityEngine;
public class PlayerStatsManager
{
    public int Health;
	public int ResourcePool;
	public int Level;

	// Primary Stats
	public int Strength;		// Attack Power, Parry, Resource Pool Regen Rate, Resource Regen Time
	public int Agility;			// Critical Strike Damage/Chance, Dodge, Range Attack Power
	public int Endurance;		// Max Health, Physical Debuff Resistance, Block, Max Resource Pool 
	public int Willpower;		// Spell Block, Spell Attack Power, Spell Debuff Resistance, Resource Pool Regen Rate
	public int Spirit;			// Spell Healing Power, Spell Haste, Max Resource Pool, Resource Pool Regen time
	public int Speed;			// Movement amount, Multistrike, Attack Haste

	
	// Secondary Stats
	[SerializeField] public int MaxHealth;
	[SerializeField] public int MaxResourcePool;
	[SerializeField] public int HealthRegen;
	[SerializeField] public float ResourcePoolRegenRating;	// unused
	[SerializeField] public float ResourcePoolRegenTime;	// unused
	[SerializeField] public float ParryRating;
	[SerializeField] public float DodgeRating;
	[SerializeField] public float BlockRating;
	[SerializeField] public float SpellBlockRating;
	[SerializeField] public int PhysicalAttackPower;
	[SerializeField] public int SpellAttackPower;
	[SerializeField] public int RangeAttackPower;
	[SerializeField] public int SpellHealingPower;
	[SerializeField] public float SpellHaste;
	[SerializeField] public float AttackHaste;
	[SerializeField] public float MultistrikeChance;
	[SerializeField] public float CriticalStrikeChance;
	[SerializeField] public float CriticalStrikeDamage;
	[SerializeField] public float PhysicalDebuffResistanceRating;
	[SerializeField] public float SpellDebuffResistanceRating;

	private int calculateMaxHealth(params float[] modifiers) {
		float currentMaxHealth = (float) MaxHealth;
		currentMaxHealth = Endurance * 2;
		foreach(float mod in modifiers)
		{
			currentMaxHealth += (mod * MaxHealth);
		}

		return Mathf.RoundToInt(currentMaxHealth);
	}

	private int calculateMaxResource(params float[] modifiers) {
		float currentMaxResource = (float) MaxResourcePool;
		currentMaxResource = Spirit * 3;
		currentMaxResource = Endurance * 2;
		foreach(int mod in modifiers)
		{
			currentMaxResource += (mod * MaxResourcePool);
		}

		if (currentMaxResource < 0)
			currentMaxResource = 0;

		return Mathf.RoundToInt(currentMaxResource);
	}
	
}

