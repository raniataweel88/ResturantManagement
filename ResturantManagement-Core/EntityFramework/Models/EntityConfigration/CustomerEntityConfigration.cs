using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models.EntityConfigration
{
    public class CustomerEntityConfigration: IEntityTypeConfiguration<Customer>
    {
        void IEntityTypeConfiguration<Customer>.Configure(EntityTypeBuilder<Customer> builder) { 
        builder.HasKey(x=>x.CustomerId);
        builder.Property(x=>x.CustomerId).UseIdentityColumn();
        builder.Property(x => x.Name).HasMaxLength(20);
        builder.Property(x => x.Phone).HasMaxLength(10);
       
        }
    }
}
