using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EksiSozlukClone.Common.ViewModels;

namespace EksiSozlukClone.Api.Domain.Models;

public class EntryCommentVote: BaseEntity
{
    public Guid EntryCommentId { get; set; }
    public VoteType VoteType { get; set; }
    public Guid CreatedById { get; set; }

    public virtual EntryComment EntryComment { get; set; }
}
