using System.Collections.Generic;
using Vrhw.Shared.DTOs;
using Vrhw.Shared.Interfaces;

namespace Vrhw.Repository.Memory
{
    public class MemoryRepository : IDiffRepository
    {
        private static Dictionary<int, DiffDto> _memory = new Dictionary<int, DiffDto>();

        public void UpsertDiff(int id, string left, string right)
        {
            if (!_memory.ContainsKey(id))
            {
                _memory[id] = new DiffDto();
            }

            _memory[id].Left = left != null ? left : _memory[id].Left;
            _memory[id].Right = right != null ? right : _memory[id].Right;
        }

        public DiffDto GetDiff(int id)
        {
            if (!_memory.ContainsKey(id))
            {
                return null;
            }

            return _memory[id];
        }
    }
}