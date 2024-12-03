using WukongDemo.recruitment.Models;
using WukongDemo.recruitment.Repositories;

namespace WukongDemo.recruitment.Services
{
    public interface IRecruitmentService
    {
        Task<Recruitment> CreateRecruitmentAsync(Recruitment recruitment);
        Task<Recruitment> GetRecruitmentByIdAsync(int id);
        Task<bool> UpdateRecruitedMembersAsync(int id, int newMemberCount);
        Task<bool> CloseRecruitmentAsync(int id);
    }

    public class RecruitmentService : IRecruitmentService
    {
        private readonly IRecruitmentRepository _repository;

        public RecruitmentService(IRecruitmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Recruitment> CreateRecruitmentAsync(Recruitment recruitment)
        {
            return await _repository.AddAsync(recruitment);
        }

        public async Task<Recruitment> GetRecruitmentByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateRecruitedMembersAsync(int id, int newMemberCount)
        {
            var recruitment = await _repository.GetByIdAsync(id);
            if (recruitment == null || !recruitment.CanRecruit()) return false;

            recruitment.RecruitedMembers = Math.Min(newMemberCount, recruitment.ExpectedMembers);
            recruitment.UpdatedAt = DateTime.Now;

            return await _repository.UpdateAsync(recruitment);
        }

        public async Task<bool> CloseRecruitmentAsync(int id)
        {
            var recruitment = await _repository.GetByIdAsync(id);
            if (recruitment == null) return false;

            recruitment.Status = "closed";
            recruitment.UpdatedAt = DateTime.Now;

            return await _repository.UpdateAsync(recruitment);
        }
    }
}
