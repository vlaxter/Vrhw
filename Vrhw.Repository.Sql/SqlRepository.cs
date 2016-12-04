using System;
using System.Linq;
using System.Linq.Expressions;
using Vrhw.Repository.Sql.Entities;
using Vrhw.Shared.DTOs;
using Vrhw.Shared.Interfaces;

namespace Vrhw.Repository.Sql
{
    public class SqlRepository : IDiffRepository
    {
        private readonly VrhwContext _context;

        // Typed lambda expression for Select() method.
        private static readonly Expression<Func<Diff, DiffDto>> AsDiffDto =
            x => new DiffDto
            {
                Left = x.Left,
                Right = x.Right
            };

        public SqlRepository()
        {
            _context = new VrhwContext();
        }

        public void UpsertDiff(int id, string left, string right)
        {
            var diff = _context.Diff.Find(id);
            if (diff == null)
            {
                diff = new Diff() { Id = id };
                _context.Diff.Add(diff);
            }

            diff.Left = left != null ? left : diff.Left;
            diff.Right = right != null ? right : diff.Right;

            _context.SaveChanges();
        }

        public DiffDto GetDiff(int id)
        {
            var diff = _context.Diff.Where(x => x.Id == id).Select(AsDiffDto).FirstOrDefault();
            return diff;
        }
    }
}