using System;
using System.ComponentModel.DataAnnotations;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Mentat.UI.Areas.Identity.Data
{
    [CollectionName("Users")]
    public class MentatUser : MongoIdentityUser<Guid>
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
