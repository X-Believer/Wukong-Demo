using Microsoft.EntityFrameworkCore;
using System;
using WukongDemo.Models;
using WukongDemo.Data;

namespace WukongDemo.Repositories
{
    public interface IRecruitmentRepository
    {
        Task<Recruitment> AddAsync(Recruitment recruitment);
        Task<Recruitment> GetByIdAsync(int id);
        Task<bool> UpdateAsync(Recruitment recruitment);
    }

    public class RecruitmentRepository : IRecruitmentRepository
    {
        private readonly AppDbContext _context;

        public RecruitmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Recruitment> AddAsync(Recruitment recruitment)
        {
            _context.Recruitments.Add(recruitment);
            await _context.SaveChangesAsync();
            return recruitment;
        }

        public async Task<Recruitment> GetByIdAsync(int id)
        {
            return await _context.Recruitments.FindAsync(id);
        }

        public async Task<bool> UpdateAsync(Recruitment recruitment)
        {
            _context.Recruitments.Update(recruitment);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
