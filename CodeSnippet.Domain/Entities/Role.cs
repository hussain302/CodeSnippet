
using CodeSnippet.Domain.Aggregates;
using CodeSnippet.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeSnippet.Domain.Entities;

[Table("tblRoles")]

public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}
