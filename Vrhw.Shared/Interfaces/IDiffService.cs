namespace Vrhw.Shared.Interfaces
{
    public interface IDiffService
    {
        bool Left(int id, string data);

        bool Right(int id, string data);

        object GetDiff(int id);
    }
}