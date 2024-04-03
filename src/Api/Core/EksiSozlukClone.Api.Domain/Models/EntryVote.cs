using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksiSozlukClone.Common.ViewModels;

namespace EksiSozlukClone.Api.Domain.Models;

public class EntryVote: BaseEntity
{
    public Guid EntryId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedById { get; set; }

    public virtual Entry Entry { get; set; }
}
