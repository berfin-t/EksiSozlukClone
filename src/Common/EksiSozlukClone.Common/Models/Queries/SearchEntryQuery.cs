﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksiSozlukClone.Common.Models.Queries;

public class SearchEntryQuery: IRequest<List<SearchEntryViewModel>>
{
    public SearchEntryQuery(string searchText)
    {
        SearchText = searchText;
    }
    public SearchEntryQuery() 
    {

    }

    public string SearchText {  get; set; }
    
}
