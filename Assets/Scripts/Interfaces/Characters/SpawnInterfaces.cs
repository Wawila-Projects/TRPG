namespace Assets.Scripts.Interfaces.Characters
{
    public interface IRespawnable {
        void Respawn ();
    }

    public interface ISpawnable {
        void Spawn ();
    }

    public interface IDespawnable {
        void Despawn ();
    }

    public interface ISummonable : ISpawnable, IDespawnable{}
}