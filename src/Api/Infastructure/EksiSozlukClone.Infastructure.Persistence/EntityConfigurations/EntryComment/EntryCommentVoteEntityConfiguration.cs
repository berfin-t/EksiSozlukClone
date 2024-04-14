using EksiSozlukClone.Api.Domain.Models;
using EksiSozlukClone.Infastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Infastructure.Persistence.EntityConfigurations.EntryComment;

public class EntryCommentVoteEntityConfiguration : BaseEntityConfiguration<EntryCommentVote>
{
    public override void Configure(EntityTypeBuilder<EntryCommentVote> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycommentvote", Context.EksiSozlukCloneDbContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.EntryComment)
            .WithMany(x => x.EntryCommentVotes)
            .HasForeignKey(x => x.EntryCommentId);
    }
}
