using AutoMapper;
using Blog.WebApi.Entities;
using Blog.WebApi.Exceptions;
using Blog.WebApi.Helpers;
using Blog.WebApi.Models;
using Blog.WebApi.Models.Posts;
using Blog.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;
        private IMapper _mapper;

        public PostsController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Post> Get(string filter, string sortColumn, string sortOrder)
        {
            return _postService.GetAll(filter, sortColumn, sortOrder);
        }

        [HttpGet("{id}")]
        public Post Get(int id)
        {
            return _postService.GetById(id);
        }


        [HttpPost]
        public IActionResult Post([FromBody] PostCreateModel model)
        {
            // map model to entity
            var post = _mapper.Map<Post>(model);

            try
            {
                // create user
                _postService.Create(post);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] PostUpdateModel model)
        {
            var post = _mapper.Map<Post>(model);
            try
            {
                // update user 
                _postService.Update(id, post);
                return Ok();
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _postService.Delete(id);
            return Ok();
        }
    }
}
