using EksiSozlukClone.Infastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Infastructure.Persistence.EntityConfigurations.Entry;

public class EntryEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.Entry>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.Entry> builder)
    {
        base.Configure(builder);

        builder.ToTable("entry", EksiSozlukCloneContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.Entries)
            .HasForeignKey(x => x.CreatedById);
    }
}
