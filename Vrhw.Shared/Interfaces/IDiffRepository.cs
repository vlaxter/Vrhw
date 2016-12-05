using Vrhw.Shared.DTOs;

namespace Vrhw.Shared.Interfaces
{
    public interface IDiffRepository
    {
        /// <summary>
        /// Inserts or Updates the difference record
        /// </summary>
        /// <param name="id">ID of the Diff</param>
        /// <param name="left">Left field</param>
        /// <param name="right">Right field</param>
        void UpsertDiff(int id, string left, string right);

        /// <summary>
        /// Gets the Left and Right fields
        /// </summary>
        /// <param name="id">ID of the Diff</param>
        /// <returns>Returns a DiffDto that contains the Left and Right fields</returns>
        DiffDto GetDiff(int id);
    }
}