using System.Collections.Generic;
using Assets.Take_II.Scripts.Enums;
public abstract class SpellBase {
    string Id;
    string Name;
    string Description;
    int Cost;
    Elements Element; 
    bool IsExclusive;
    List<string> ExplusiveUnits;
    protected bool IsMagical;
    protected bool IsPhysical {
        get { return !IsMagical; }   
        set { IsMagical = !value; }
    }
    protected bool CostsSp;
    protected bool CostsHp {
        get { return !CostsSp; }   
        set { CostsSp = !value; }
    }

    public abstract void Cast();
}