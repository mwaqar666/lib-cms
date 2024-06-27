using LibCMS.Base;

namespace LibCMS.Entities;

public class User(string name) : BaseEntity
{
    public string Name { get; set; } = name;
}