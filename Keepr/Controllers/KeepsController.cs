using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KeepsController : ControllerBase
    {
        private readonly KeepsService _ks;

        public KeepsController(KeepsService ks)
        {
            _ks = ks;
        }

        [HttpGet]
        public ActionResult<List<Keep>> Get()
        {
            try
            {
                List<Keep> keeps = _ks.Get();
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Keep> Get(int id)
        {
            try
            {
                Keep keep = _ks.Get(id);
                return Ok(keep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Keep>> Create([FromBody] Keep keep)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                keep.CreatorId = userInfo.Id;
                Keep newKeep = _ks.Create(keep);
                keep.CreatedAt = DateTime.UtcNow;
                keep.UpdatedAt = DateTime.UtcNow;
                keep.Creator = userInfo;
                return Created($"/api/keeps/{newKeep.Id}", newKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Keep>> Edit([FromBody] Keep keep, int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                keep.CreatorId = userInfo.Id;
                keep.Id = id;
                Keep editedKeep = _ks.Edit(keep, userInfo.Id);
                return Ok(editedKeep);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<String>> Delete(int id)
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                _ks.Delete(id, userInfo.Id);
                return Ok("Delorted");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}