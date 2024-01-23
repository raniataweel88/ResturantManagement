using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResturantManagement_Core.EntityFramework.Models.EntityConfigration
{
    public class TableEntityConfigration : IEntityTypeConfiguration<Table>
    {
        void IEntityTypeConfiguration<Table>.Configure(EntityTypeBuilder<Table> builder)
        {
            builder.HasKey(x => x.TableId);
            builder.Property(x => x.TableId).UseIdentityColumn();
        }
    }
    }
