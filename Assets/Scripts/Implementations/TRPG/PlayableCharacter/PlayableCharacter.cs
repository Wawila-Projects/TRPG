using System;
using System.Collections.Generic;
using Assets.Scripts.Interfaces.Attacks;
using Assets.Scripts.Interfaces.Characters;
using Assets.Scripts.Interfaces.Elements;
using UnityEngine;

public class PlayableCharacter : MonoBehaviour, ICharacter, IFullCharacter<int,int,int>, IFullFighter<IElement>
{
    PlayerStatsManager stats;
    ICharacter target;
    IDictionary<string, KeyCode> inputs;
    IDictionary<string, IMelee> melee;
    IDictionary<string, IRange> range;
    IDictionary<string, IMagic<IElement>> magic;


    void Awake()
    {
        stats = new PlayerStatsManager();
        initInputDictionary();
    }

    void Start ()
	{
       
    }
	
	void Update ()
	{


	}

    void initInputDictionary()
    {
        inputs = new Dictionary<string, KeyCode>();
        inputs.Add("select", KeyCode.Mouse0);
        inputs.Add("cancel", KeyCode.Escape);
        inputs.Add("deselect", KeyCode.Mouse1);
        inputs.Add("target", KeyCode.Space);
        inputs.Add("iterate", KeyCode.Tab);
        inputs.Add("shiftR", KeyCode.RightShift);
        inputs.Add("shiftL", KeyCode.LeftShift);

        inputs.Add("attack0", KeyCode.Alpha0);
        inputs.Add("attack1", KeyCode.Alpha1);
        inputs.Add("attack2", KeyCode.Alpha2);
        inputs.Add("attack3", KeyCode.Alpha3);
        inputs.Add("attack4", KeyCode.Alpha4);
    }

    void IKillabel.Die()
    {
        throw new NotImplementedException();
    }

    void IHealable<int>.Heal(int healAmount)
    {
        if(this.stats.Health > 0)
            this.stats.Health += healAmount;
    }

    void IRevivable.Revive()
    {
        if (this.stats.Health <= 0)
            this.stats.Health = Mathf.RoundToInt(this.stats.MaxHealth * 0.25f);
    }

    void IMoveable<int>.Move(int moveAmount)
    {
        throw new NotImplementedException();
    }

    void IDamageable<int>.Damage(int damageAmount)
    {
        this.stats.Health -= damageAmount;
    }

    void ISpellCaster<IElement>.CastSpell(IMagic<IElement> spell)
    {
        throw new NotImplementedException();
    }

    void ISwordSwinger.SwingSword(IMelee melee)
    {
        throw new NotImplementedException();
    }

    void IGunSlinger.SlingGun(IRange range)
    {
        throw new NotImplementedException();
    }
} 

