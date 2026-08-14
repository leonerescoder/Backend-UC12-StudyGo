public class Company
{
    public int id { get; set; }
    public string name { get; set; }

    public Company() { }

    public Company(int id)
    {
        this.id = id;
    }

    public Company(int id, string name)
    {
        this.id = id;
        this.name = name;
    }
}
