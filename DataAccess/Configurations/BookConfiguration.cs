using EVABookShop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EVABookShop.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books", "MasterSchema");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Description)
                .HasMaxLength(250)
                .IsRequired(false); // Explicitly allow null

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Price)
                .IsRequired()
                .HasColumnName("BookPrice")
                .HasPrecision(18, 2);

            // Enforce price range at the database level
            builder.ToTable("Books", t =>
            {
                t.HasCheckConstraint("CK_Books_Price_Range", "[BookPrice] >= 1 AND [BookPrice] <= 1000");
            });

            builder.HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
