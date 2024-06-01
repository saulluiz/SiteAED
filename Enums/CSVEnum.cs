public static class PeopleFiles
{
    public const string p1 = "people-100.csv";
    public const string p2 = "people-1000.csv";
    public const string p3 = "people-10000.csv";
    public const string p4 = "people-100000.csv";

    public static string GetFile(int id)
    {
        switch (id)
        {
            case 1:
                return p1;
            case 2:
                return p2;
            case 3:
                return p3;
            case 4:
                return p4;
            default:
                return p1;
        }
    }
}