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
    public class VaultsController : ControllerBase
    {
        private readonly VaultsService _vs;
        private readonly KeepsService _ks;

        private readonly VaultKeepsService _vks;

        public VaultsController(VaultsService vs, KeepsService ks, VaultKeepsService vks)
        {
            _vs = vs;
            _ks = ks;
            _vks = vks;
        }

        [HttpGet]
        public ActionResult<List<Vault>> Get()
        {
            try
            {
                List<Vault> vaults = _vs.Get();
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Vault>> Get(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Vault vault = _vs.Get(id, userInfo?.Id);
                return Ok(vault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<VaultKeepViewModel>> GetKeeps(int id, string userId)
        {
            try
            {
                List<VaultKeepViewModel> keeps = _vks.GetKeeps(id, userId);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Vault>> Post([FromBody] Vault vault)
        {
            try
            {
                Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
                vault.CreatorId = profile.Id;
                Vault newVault = _vs.Create(vault);
                newVault.Creator = profile;
                return Created($"/api/vaults/{newVault.Id}", newVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Put(int id, [FromBody] Vault vault)
        {
            try
            {
                Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
                vault.CreatorId = profile.Id;
                vault.Id = id;
                Vault updatedVault = _vs.Update(vault);
                return Ok(updatedVault);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Vault>> Delete(int id)
        {
            try
            {
                Profile profile = await HttpContext.GetUserInfoAsync<Profile>();
                _vs.Delete(id, profile.Id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}