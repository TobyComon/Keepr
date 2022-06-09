using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultKeepsService
    {
        private readonly VaultKeepsRepository _vkrepo;
        private readonly VaultsService _vs;
        private Vault found;

        public VaultKeepsService(VaultKeepsRepository vkrepo, VaultsService vs)
        {
            _vkrepo = vkrepo;
            _vs = vs;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            // TODO you need to make sure that a user cant add a keep to another users vaults. SO - at this point we should have direct access to the vaultId, which we can use to go get that particular vault by its ID. Then we should check to see if that vault is ours.....
            found = _vs.Get(newVaultKeep.VaultId, newVaultKeep.CreatorId);
            if (found.CreatorId != newVaultKeep.CreatorId)
            {
                throw new Exception("THATS MY PURSE I DONT KNOW U");
            }
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

        internal void Delete(int id, string userId)
        {
            // TODO see above - we aren't checking user identity
            // Maybe go get the whole vaultkeep?

            VaultKeep foundVK = Get(id);
            // now go get the whole vault object and pass through foundVK.vaultID
            Vault foundV = _vs.Get(foundVK.VaultId, userId);
            if (foundV.CreatorId != userId)
            {
                throw new Exception("THATS MY PURSE I DONT KNOW U");
            }
            // Get(id);
            _vkrepo.Delete(id, foundVK.KeepId);
        }

        internal List<VaultKeepViewModel> GetKeeps(int vaultId, string id)
        {
            List<VaultKeepViewModel> keeps = _vkrepo.GetKeeps(vaultId, id);

            found = _vs.Get(vaultId, id);

            if (found.IsPrivate == true && found.CreatorId != id)
            {
                throw new Exception("ACCESS DENIED");
            }


            return keeps;
        }
    }
}