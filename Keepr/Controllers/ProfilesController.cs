using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Services;
using Microsoft.AspNetCore.Mvc;

namespace Keepr.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProfilesController : ControllerBase
    {
        private readonly AccountService _acctService;



        private readonly VaultsService _vs;

        private readonly KeepsService _ks;

        public ProfilesController(AccountService acctService, VaultsService vs, KeepsService ks)
        {
            _acctService = acctService;
            _vs = vs;
            _ks = ks;
        }

        [HttpGet("{id}")]
        public ActionResult<Profile> Get(string id)
        {
            try
            {
                Profile profile = _acctService.GetProfileById(id);
                return Ok(profile);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/vaults")]
        public ActionResult<List<Vault>> GetVaults(string id)
        {
            try
            {
                _acctService.GetProfileById(id);
                List<Vault> vaults = _vs.GetVaultsByCreatorId(id);
                return Ok(vaults);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}/keeps")]
        public ActionResult<List<Keep>> GetKeeps(string id)
        {
            try
            {
                _acctService.GetProfileById(id);
                List<Keep> keeps = _ks.GetKeepsByCreatorId(id);
                return Ok(keeps);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}