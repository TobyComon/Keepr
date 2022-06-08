using System;
using System.Collections.Generic;
using Keepr.Models;
using Keepr.Repositories;

namespace Keepr.Services
{
    public class KeepsService
    {
        private readonly KeepsRepository _keepsrepo;

        public KeepsService(KeepsRepository keepsrepo)
        {
            _keepsrepo = keepsrepo;
        }
        internal List<VaultKeep> GetKeepsByVaultId(int id)
        {
            throw new NotImplementedException();
        }

        internal List<Keep> Get()
        {
            return _keepsrepo.Get();
        }

        internal Keep Get(int id)
        {
            Keep foundKeep = _keepsrepo.Get(id);
            if (foundKeep == null)
            {
                throw new Exception("Invalid Id");
            }
            return foundKeep;
        }

        internal Keep Create(Keep keep)
        {
            Keep newKeep = _keepsrepo.Create(keep);
            return newKeep;
        }

        internal Keep Edit(Keep keep, string userId)
        {
            Keep original = Get(keep.Id);
            IsCreator(original.CreatorId, keep.CreatorId, userId);
            original.Name = keep.Name ?? original.Name;
            original.Description = keep.Description ?? original.Description;
            _keepsrepo.Edit(original);
            return original;
        }

        internal void Delete(int id, string userId)
        {
            Keep foundKeep = Get(id);
            IsCreator(foundKeep.CreatorId, userId);
            _keepsrepo.Delete(id);
        }

        private void IsCreator(string creatorId, string userId)
        {
            if (creatorId != userId)
            {
                throw new Exception("ACCESS DENIED");
            }
        }

        private static void IsCreator(string creatorId, string userId, string userId1)
        {
            if (creatorId != userId)
            {
                throw new Exception("ACCESS DENIED");
            }
        }

        internal List<Keep> GetByCreatorId(string id)
        {
            return _keepsrepo.GetByCreatorId(id);
        }

        internal List<Keep> GetKeepsByCreatorId(string id)
        {
            return _keepsrepo.GetByCreatorId(id);
        }
    }
}