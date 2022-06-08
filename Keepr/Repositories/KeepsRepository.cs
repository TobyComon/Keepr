using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Keepr.Models;

namespace Keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Keep Create(Keep keep)
        {
            string sql = @"
            INSERT INTO keeps
            (name, description, img, views, kept, creatorId )
            VALUES
            (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
            SELECT LAST_INSERT_ID();";
            keep.Id = _db.ExecuteScalar<int>(sql, keep);
            return keep;
        }

        internal Keep Get(int id)
        {
            string sql = @"
            SELECT
            a.*,
            k.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE k.id = @id";
            return _db.Query<Account, Keep, Keep>(sql, (a, k) =>
            {
                k.Creator = a;
                return k;
            }, new { id }).FirstOrDefault();
        }

        internal List<Keep> Get()
        {
            string sql = @"
            SELECT
            k.*, a.* FROM keeps k JOIN accounts a ON k.creatorId = a.id";
            return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
            {
                k.Creator = p;
                return k;
            }).ToList();
        }

        internal List<Keep> GetByCreatorId(string id)
        {
            string sql = @"
            SELECT
                k.*,
                a.*
            FROM keeps k
            JOIN accounts a ON k.creatorId = a.id
            WHERE creatorId = @id";
            return _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
            {
                k.Creator = p;
                return k;
            }, new { id }).ToList();
        }

        internal void Edit(Keep original)
        {
            string sql = @"
            UPDATE keeps
            SET
                name = @Name,
                description = @Description
            WHERE id = @Id;";
            _db.Execute(sql, original);
        }

        internal void Delete(int id)
        {
            string sql = "DELETE FROM keeps WHERE id = @id LIMIT 1";
            _db.Execute(sql, new { id });
        }
    }
}