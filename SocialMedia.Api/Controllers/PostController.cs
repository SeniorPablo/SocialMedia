using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Core.DTOs;
using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            return Ok(_mapper.Map<IEnumerable<PostDto>>(posts));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postRepository.GetPost(id);
            return Ok(_mapper.Map<PostDto>(post));
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto entity)
        {
            await _postRepository.AddPost(_mapper.Map<Post>(entity));
            return Ok(entity);
        }
    }
}
