using System;
using System.Linq;
using System.Linq.Expressions;
using Vrhw.Repository.Sql.Entities;
using Vrhw.Shared.DTOs;
using Vrhw.Shared.Interfaces;
using AutoMapper;

namespace Vrhw.Repository.Sql
{
    public class SqlRepository : IDiffRepository
    {
        private readonly VrhwContext _context;

        public SqlRepository()
        {
            _context = new VrhwContext();
            Mapper.Initialize(cfg => cfg.CreateMap<Diff, DiffDto>());
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
            var diff = _context.Diff.FirstOrDefault(x => x.Id == id);
            var diffDto = Mapper.Map<DiffDto>(diff);
            return diffDto;
        }
    }
}