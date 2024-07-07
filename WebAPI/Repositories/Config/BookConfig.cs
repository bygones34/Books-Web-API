using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebAPI.Models;

namespace WebAPI.Repositories.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book { Id = 1, Title = "Karagöz ve Hacivat", Price = 75},
                new Book { Id = 2, Title = "Hacivat ve Karagöz", Price = 175},
                new Book { Id = 3, Title = "Vadideki Zambak", Price = 275}
                );
        }
    }
}