using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Interfaces;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class VaultsRepository : IRepository<Vault, string>
    {
        private readonly IDbConnection _db;
        public VaultsRepository(IDbConnection db)
        {
            _db = db;
        }
        internal List<Vault> Get()
        {
            string sql = @"
            SELECT
                a.*,
                v.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id";
            return _db.Query<Account, Vault, Vault>(sql, (a, v) =>
            {
                v.Creator = a;
                return v;
            }).ToList();
        }

        internal Vault Get(int id)
        {
            string sql = @"
            SELECT
                a.*,
                v.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.id = @id";
            return _db.Query<Account, Vault, Vault>(sql, (a, v) =>
            {
                v.Creator = a;
                return v;
            }, new { id }).FirstOrDefault();
        }

        internal Vault Create(Vault vault)
        {
            string sql = @"
            INSERT INTO vaults
            (name, description, image, isPrivate, creatorId)
            VALUES
            (@Name, @Description, @Image, @IsPrivate, @CreatorId);
            SELECT LAST_INSERT_ID();
            ";
            int id = _db.ExecuteScalar<int>(sql, vault);
            vault.Id = id;
            return vault;
        }
        internal void Update(Vault original)
        {
            string sql = @"
            UPDATE vaults
            SET
                name = @Name,
                description = @Description
            WHERE id = @Id;
            ";
            _db.Execute(sql, original);
        }

        internal List<Vault> GetVaults(string id)
        {
            string sql = @"
            SELECT
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE v.creatorId = @id
            ";
            return _db.Query<Vault, Account, Vault>(sql, (v, a) =>
            {
                v.Creator = a;
                return v;
            }, new { id }).ToList();
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM vaults WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }




        public List<Vault> GetAll()
        {
            throw new NotImplementedException();
        }



        internal Profile GetVaultsByCreatorId(int id, string userId)
        {
            string sql = @"
            SELECT * from vaults
            WHERE id = @id AND creatorId = @userId
            ";
            return _db.QueryFirstOrDefault<Profile>(sql, new { id, userId });
        }

        public List<Vault> GetById(string id)
        {
            string sql = @"
            SELECT
                v.*,
                a.*
            FROM vaults v
            JOIN accounts a ON v.creatorId = a.id
            WHERE creatorId = @id";
            return _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
            {
                v.Creator = p;
                return v;
            }, new { id }).ToList();
        }

        Vault IRepository<Vault, string>.GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}