class Location
{
    public Guid Id {get; private set;} = Guid.NewGuid();
    public string Name {get;set;} = null!;
    public LocationType LType {get;set;}

    private Location() {}

    public Location(string name, LocationType ltype)
    {
        Name = name;
        LType = ltype;
    }
}