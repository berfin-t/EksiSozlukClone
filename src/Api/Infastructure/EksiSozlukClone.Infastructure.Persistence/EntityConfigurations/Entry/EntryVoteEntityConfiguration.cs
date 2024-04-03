using EksiSozlukClone.Api.Domain.Models;
using EksiSozlukClone.Infastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Infastructure.Persistence.EntityConfigurations.Entry;

public class EntryVoteEntityConfiguration : BaseEntityConfiguration<EntryVote>
{
    public override void Configure(EntityTypeBuilder<EntryVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("entryvote", EksiSozlukCloneContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.Entry)
            .WithMany(x => x.EntryVotes)
            .HasForeignKey(x => x.EntryId);
    }
}
{
}
