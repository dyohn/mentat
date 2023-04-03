using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;

namespace Mentat.UI.Areas.Identity.Data
{
    [CollectionName("Roles")]
    public class MentatUserRole : MongoIdentityRole<Guid>
    {

    }
}
