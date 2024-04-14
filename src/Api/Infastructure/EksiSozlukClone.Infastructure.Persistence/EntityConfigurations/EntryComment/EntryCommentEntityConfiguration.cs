using EksiSozlukClone.Infastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Infastructure.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentEntityConfiguration : BaseEntityConfiguration<Api.Domain.Models.EntryComment>
{
    public override void Configure(EntityTypeBuilder<Api.Domain.Models.EntryComment> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycomment", Context.EksiSozlukCloneDbContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.CreatedBy)
            .WithMany(x => x.EntryComments)
            .HasForeignKey(x => x.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Entry)
            .WithMany(x => x.EntryComments)
            .HasForeignKey(x => x.EntryId);
    }
}
 
