namespace Assets.Scripts.Interfaces.Elements
{
    public interface IBiome {
        void Effect ();
    }

    public interface IForest : IBiome { }
    public interface IJungle : IBiome { }
    public interface IGrassland : IBiome { }
    public interface ITundra : IBiome { }
    public interface IDesert : IBiome { }
    public interface IAquatic : IBiome { }
}