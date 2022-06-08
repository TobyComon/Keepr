using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class VaultsService
    {
        private readonly VaultsRepository _vaultsrepo;
        private readonly KeepsRepository _keepsrepo;

        public VaultsService(VaultsRepository vaultsrepo, KeepsRepository keepsrepo)
        {
            _vaultsrepo = vaultsrepo;
            _keepsrepo = keepsrepo;
        }
        internal List<Vault> Get()
        {
            List<Vault> vaults = _vaultsrepo.Get();
            vaults = vaults.FindAll(v => v.IsPrivate == false);
            return vaults;
        }

        private Vault Get(int id)
        {
            Vault found = _vaultsrepo.Get(id);
            if (found == null)
            {
                throw new Exception("Vault not found");
            }
            return found;
        }

        public Vault Get(int id, string userId)
        {
            Vault found = _vaultsrepo.Get(id);
            // NOTE handles if no Vault
            if (found == null)
            {
                throw new Exception("Group not found");
            }
            // NOTE handles if vault is private and you are not the creator
            if (found.IsPrivate && found.CreatorId != userId)
            {
                Profile hasAccess = _vaultsrepo.GetVaultsByCreatorId(id, userId);
                if (hasAccess == null)
                {
                    throw new Exception("You do not have access to this vault :(");
                }
            }
            return found;
        }

        internal List<Vault> GetVaultsByCreatorId(string id)
        {
            return _vaultsrepo.GetById(id);
        }

        internal List<Vault> GetByCreatorId(string id)
        {
            List<Vault> vaults = _vaultsrepo.GetVaults(id);
            return vaults;
        }

        internal Vault Create(Vault vault)
        {
            Vault newVault = _vaultsrepo.Create(vault);
            return newVault;
        }

        internal Vault Update(Vault update)
        {
            Vault original = Get(update.Id);
            IsCreator(original.CreatorId, update.CreatorId);
            original.Name = update.Name ?? original.Name;
            original.Description = update.Description ?? original.Description;
            original.Image = update.Image ?? original.Image;
            _vaultsrepo.Update(original);
            return original;
        }

        internal void Delete(int id, string userName)
        {
            Vault vault = Get(id);
            if (vault.CreatorId != userName)
            {
                throw new Exception("You are not the creator of this Vault!");
            }
            _vaultsrepo.Delete(id);
        }

        private static void IsCreator(string creatorId, string userId)
        {
            if (creatorId != userId)
            {
                throw new Exception("YOU DO NOT HAVE ACCESS");
            }
        }



    }
}