using GorestApp.Entities.Concrete.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GorestApp.DataAccess.Configuration.UserConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).HasColumnName("Id").IsRequired();
            builder.Property(d => d.Name).HasColumnName("Name").HasMaxLength(150).IsRequired();
            builder.Property(d => d.Email).HasColumnName("Email").HasMaxLength(100).IsRequired();
            builder.Property(d => d.Gender).HasColumnName("Gender").HasMaxLength(10).IsRequired();
            builder.Property(d => d.Status).HasColumnName("Status").HasMaxLength(30).IsRequired();
            builder.Property(d => d.Deleted).HasColumnName("Deleted").IsRequired();
        }
    }
}
