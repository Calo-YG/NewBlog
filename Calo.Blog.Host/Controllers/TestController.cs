﻿using Calo.Blog.EntityCore.DataBase.Entities;
using Calo.Blog.Extenions.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Calo.Blog.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// justTestApi
        /// </summary>
        [HttpGet("SetNotOP")]
        [NoResult]
        public int SetNotOP()
        {
            return 1;
        }

        [HttpGet("TestTask")]
        public async Task<int> TestTask()
        {
            await Task.CompletedTask;
            return 1;
        }
        [HttpGet("GetUser")]
        public User GetUser(Guid id)
        {
            return new User { Id = 1, };
        }
   }
}