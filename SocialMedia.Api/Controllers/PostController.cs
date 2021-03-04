using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Core.DTOs;
using SocialMedia.Api.Core.Entities;
using SocialMedia.Api.Core.Interfaces.Services;
using SocialMedia.Api.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;
        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
            var response = new ApiResponse<IEnumerable<PostDto>>(postsDto);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _postService.GetPost(id);
            var postDto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(PostDto entity)
        {
            var post = _mapper.Map<Post>(entity);
            await _postService.AddPost(post);
            entity = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(entity);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int id, PostDto entity)
        {
            entity.PostId = id;
            var result = await _postService.UpdatePost(_mapper.Map<Post>(entity));
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = new ApiResponse<bool>(await _postService.DeletePost(id));
            return Ok(response);
        }
    }
}
