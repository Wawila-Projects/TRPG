namespace Assets.Spells.SpellLexicon {
    public static class SpellLexicon {
        public static FireLexicon Fire => new FireLexicon();
        public static WindLexicon Wind => new WindLexicon();
        public static ElecLexicon Elec => new ElecLexicon();
        public static IceLexicon Ice => new IceLexicon();
        public static LightLexicon Light => new LightLexicon();
        public static DarknessLexicon Darkness => new DarknessLexicon();
        public static PhysicalLexicon Physical => new PhysicalLexicon();
        public static AlmightyLexicon Almighty => new AlmightyLexicon();
    
        public static DarknessLexicon Curse => Darkness;
        public static LightLexicon Bless => Light;
    }
}