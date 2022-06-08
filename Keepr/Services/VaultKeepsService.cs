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