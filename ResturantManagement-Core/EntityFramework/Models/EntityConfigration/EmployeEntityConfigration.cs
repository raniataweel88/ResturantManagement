using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models.EntityConfigration
{
    public class EmployeEntityConfigration : IEntityTypeConfiguration<Employe>
    {
        void IEntityTypeConfiguration<Employe>.Configure(EntityTypeBuilder<Employe> builder)
        {
            builder.HasKey(x => x.EmployeId);
            builder.Property(x => x.EmployeId).UseIdentityColumn();
            builder.Property(x => x.Name).HasMaxLength(20);        }

    }
}
