using System.Security.Claims;
using DevBlog.Core.Dtos;
using DevBlog.Core.Entities;
using DevBlog.Core.Interfaces;
using DevBlog.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevBlog.Controller
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        public TestController() { }

            [HttpGet]
            [Route("test")]
        public IActionResult Test()
        {
            return Ok("works");
        }
    }
}
