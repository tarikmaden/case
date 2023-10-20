using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Entity.Models;
using System.Linq;

namespace PersonService.Data
{
    public static class ContactTypeSeeder
    {
        public static void SeedData(IServiceProvider serviceProvider)
        {
            using (var context = new EntityContext(
                serviceProvider.GetRequiredService<DbContextOptions<EntityContext>>()))
            {
                // Veritabanında ContactType var mı kontrol et
                if (context.ContactTypes.Any())
                {
                    return; // Veriler zaten eklenmiş, seeder işlemine gerek yok.
                }

                // ContactType verilerini ekleyin
                context.ContactTypes.Add(new ContactType { Id = Guid.NewGuid(), Name = "Telefon Numarası",Slug = "phone" });
                context.ContactTypes.Add(new ContactType { Id = Guid.NewGuid(), Name = "E-mail Adresi" ,Slug = "email"});
                context.ContactTypes.Add(new ContactType { Id = Guid.NewGuid(), Name = "Konum" ,Slug = "location"});

                context.SaveChanges();
            }
        }
    }
}
