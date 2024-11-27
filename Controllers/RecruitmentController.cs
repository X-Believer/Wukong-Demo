using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using WukongDemo.Models;
using WukongDemo.Services;

namespace WukongDemo.Controllers
{
    [ApiController]
    [Route("api/recruitments")]
    public class RecruitmentController : ControllerBase
    {
        private readonly IRecruitmentService _recruitmentService;

        public RecruitmentController(IRecruitmentService recruitmentService)
        {
            _recruitmentService = recruitmentService;
        }

        // 创建招募信息
        [HttpPost]
        public async Task<IActionResult> CreateRecruitment([FromBody] Recruitment recruitment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdRecruitment = await _recruitmentService.CreateRecruitmentAsync(recruitment);
            return CreatedAtAction(nameof(GetRecruitmentById), new { id = createdRecruitment.Id }, createdRecruitment);
        }

        // 获取招募信息
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecruitmentById(int id)
        {
            var recruitment = await _recruitmentService.GetRecruitmentByIdAsync(id);
            if (recruitment == null) return NotFound();
            return Ok(recruitment);
        }

        // 更新招募人数
        [HttpPut("{id}/recruit")]
        public async Task<IActionResult> UpdateRecruitedMembers(int id, [FromBody] int newMemberCount)
        {
            var result = await _recruitmentService.UpdateRecruitedMembersAsync(id, newMemberCount);
            if (!result) return BadRequest("Unable to update recruited members.");
            return NoContent();
        }

        // 关闭招募
        [HttpPut("{id}/close")]
        public async Task<IActionResult> CloseRecruitment(int id)
        {
            var result = await _recruitmentService.CloseRecruitmentAsync(id);
            if (!result) return BadRequest("Unable to close recruitment.");
            return NoContent();
        }
    }
}
