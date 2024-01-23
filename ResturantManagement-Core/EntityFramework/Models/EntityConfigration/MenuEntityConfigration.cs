using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models.EntityConfigration
{
    public class MenuEntityConfigration : IEntityTypeConfiguration<Menu>
    {
        void IEntityTypeConfiguration<Menu>.Configure(EntityTypeBuilder<Menu> builder)
        {
            builder.HasKey(x => x.MenuId);
            builder.Property(x => x.MenuId).UseIdentityColumn();
        }
    }
}
