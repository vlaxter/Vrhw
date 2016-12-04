using Vrhw.Shared.DTOs;

namespace Vrhw.Shared.Interfaces
{
    public interface IDiffRepository
    {
        void UpsertDiff(int id, string left, string right);

        DiffDto GetDiff(int id);
    }
}