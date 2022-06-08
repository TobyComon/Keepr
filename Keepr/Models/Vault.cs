using System;
using Keepr.Interfaces;

namespace Keepr.Models
{
    public class Vault : IRepoItem<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public string Image { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}