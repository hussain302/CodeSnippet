using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnippet.Domain.Entities;

public class Blog
{

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string BlogContent { get; set; }
    public string ImageUrl { get; set; }

}
