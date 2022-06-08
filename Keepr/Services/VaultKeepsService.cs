using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkrepo;

        public VaultKeepsService(VaultKeepsRepository vkrepo)
        {
            _vkrepo = vkrepo;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            // TODO you need to make sure that a user cant add a keep to another users vaults. SO - at this point we should have direct access to the vaultId, which we can use to go get that particular vault by its ID. Then we should check to see if that vault is ours.....
            return _vkrepo.Create(newVaultKeep);
        }

        internal VaultKeep Get(int id)
        {
            VaultKeep found = _vkrepo.Get(id);
            if (found == null)
            {
                throw new Exception("Invalid Id");
            }
            return found;
        }

        internal void Delete(int id)
        {
            // TODO see above - we aren't checking user identity
            Get(id);
            _vkrepo.Delete(id);
        }

        internal List<VaultKeepViewModel> GetKeeps(int vaultId, string id)
        {
            List<VaultKeepViewModel> keeps = _vkrepo.GetKeeps(vaultId, id);
            return keeps;
        }
    }
}