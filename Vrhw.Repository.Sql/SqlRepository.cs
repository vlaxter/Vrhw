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
        private IMapper _mapper;

        public SqlRepository()
        {
            _context = new VrhwContext();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Diff, DiffDto>());
            _mapper = config.CreateMapper();
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
            var diffDto = _mapper.Map<DiffDto>(diff);
            return diffDto;
        }
    }
}