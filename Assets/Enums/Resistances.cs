namespace Assets.Enums {
    
    ///<summary>
    /// Can be used as priorities, where Absorb has the highest and None has the lowest;
    /// <example>`Resistance > Resist`</example> will cause no damage to the target;
    ///</summary>
    
    public enum ResistanceModifiers {
        None,
        Weak,
        Resist,
        Block,
        Reflect,
        Absorb,
    }
}