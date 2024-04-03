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

public class EntryCommentFavoriteEntityConfiguration :BaseEntityConfiguration<EntryCommentFavorite>
{
    public override void Configure(EntityTypeBuilder<EntryCommentFavorite> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycommentfavorite", EksiSozlukCloneContext.DEFAULT_SCHEMA);

        builder.HasOne(x => x.EntryComment)
            .WithMany(x => x.EntryCommentFavorites)
            .HasForeignKey(x => x.EntryComentId);

        builder.HasOne(x => x.CreatedUser)
            .WithMany(x => x.EntryCommentFavorites)
            .HasForeignKey(x => x.CreatedById);
    }
}
