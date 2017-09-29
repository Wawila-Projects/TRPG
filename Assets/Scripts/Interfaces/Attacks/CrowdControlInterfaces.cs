namespace Assets.Scripts.Interfaces.Attacks
{
    public interface IStunable  {
        void Stun ();
        void EndStun ();
    }

    public interface ISilencable  {
        void Silence ();
        void EndSilence ();
    }

    public interface IRootable  {
        void Root ();
        void EndRoot ();
    }

    public interface IIncapacitable  {
        void Incapacitate ();
        void EndIncapacitate ();
    }

    public interface IDisonrientable  {
        void Disorient ();
        void EndDisorient ();
    }

    public interface IKnockBackable  {
        void KnockBack ();
        void EndKnockBack ();
    }

    public interface IFullCCable : IStunable, IIncapacitable, IDisonrientable, IKnockBackable, IRootable, ISilencable {}
}