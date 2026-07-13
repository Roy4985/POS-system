class Location
{
    public Guid Id {get; private set;} = Guid.NewGuid();
    public string Name {get;set;}
    public LocationType LType {get;set;}

    public Location(string name, LocationType ltype)
    {
        Name = name;
        LType = ltype;
    }
}