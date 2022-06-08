using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultKeepsRepository
    {
        private readonly IDbConnection _db;

        public VaultKeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal VaultKeep Create(VaultKeep newVaultKeep)
        {
            string sql = @"
            INSERT INTO vaultkeeps
                (vaultId, keepId, creatorId)
            VALUES
                (@VaultId, @KeepId, @CreatorId);
            SELECT LAST_INSERT_ID()";
            newVaultKeep.Id = _db.ExecuteScalar<int>(sql, newVaultKeep);
            return newVaultKeep;
        }

        internal VaultKeep Get(int id)
        {
            string sql = @"
            SELECT
             vk.*,
             a.*
            FROM vaultkeeps vk
            JOIN accounts a ON vk.creatorId = a.id
             WHERE vk.id = @id";
            return _db.Query<VaultKeep, Profile, VaultKeep>(sql, (vk, p) =>
            {
                vk.Creator = p;
                return vk;
            }, new { id }).FirstOrDefault();
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaultkeeps WHERE id = @id";
            _db.Execute(sql, new { id });
        }

        internal List<VaultKeepViewModel> GetKeeps(int vaultId, string userId)
        {
            string sql = @"
            SELECT
            k.*,
            vk.id AS vaultKeepId,
            a.*
            FROM vaultkeeps vk
            JOIN keeps k ON vk.keepId = k.id
            JOIN accounts a ON vk.creatorId = a.id
            WHERE vk.vaultId = @vaultId
            ";
            return _db.Query<VaultKeepViewModel, Profile, VaultKeepViewModel>(sql, (vk, p) =>
            {
                vk.Creator = p;
                return vk;
            }, new { vaultId }).ToList();
        }
    }
}