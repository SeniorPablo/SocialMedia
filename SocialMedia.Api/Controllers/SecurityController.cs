using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Core.DTOs;
using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Enumerations;
using SocialMedia.Api.Core.Interfaces.Services;
using SocialMedia.Api.Responses;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize(Roles = nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        public SecurityController(ISecurityService securityService, IMapper mapper)
        {
            _securityService = securityService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Security(SecurityDto entity)
        {
            var security = _mapper.Map<Security>(entity);
            await _securityService.RegisterUser(security);
            entity = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(entity);
            return Ok(response);
        }
    }
}
