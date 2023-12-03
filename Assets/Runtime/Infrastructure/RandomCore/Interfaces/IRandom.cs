namespace Runtime.Infrastructure.RandomCore.Interfaces
{
    public interface IRandom
    {
        int Next();

        int Next(int min, int max);

        int Next(int max);

        float NextSingle();

        bool NextBoolean();
    }
}