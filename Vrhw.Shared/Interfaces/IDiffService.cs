namespace Vrhw.Shared.Interfaces
{
    public interface IDiffService
    {
        /// <summary>
        /// Insert a Base64 string into the Left field of the given Id, If the id doesn't exists the entry is created.
        /// </summary>
        /// <param name="id">ID of the Diff.</param>
        /// <param name="data">Base64 string.</param>
        /// <returns>Returns True if successful, False if not.</returns>
        bool Left(int id, string data);

        /// <summary>
        /// Insert a Base64 string into the Right field of the given Id, If the id doesn't exists the entry is created.
        /// </summary>
        /// <param name="id">ID of the Diff.</param>
        /// <param name="data">Base64 string.</param>
        /// <returns>Returns True if successful, False if not.</returns>
        bool Right(int id, string data);

        /// <summary>
        /// Evaluate if the Left and Right fields are equal, diferent in size or if Left and Right are the same size, where are the differences.
        /// </summary>
        /// <param name="id">ID of the Diff</param>
        /// <returns>Returns the different result type and the differences if apply.</returns>
        object GetDiff(int id);
    }
}