public static class PeopleFiles
{
    public const string PEOPLE_100 = "people-100.csv";
    public const string PEOPLE_1000 = "people-1000.csv";
    public const string PEOPLE_10000 = "people-10000.csv";
    public const string PEOPLE_1000000 = "people-1000000.csv";

    public static string GetFile(int id)
    {
        switch (id)
        {
            case 1:
                return PEOPLE_100;
            case 2:
                return PEOPLE_1000;
            case 3:
                return PEOPLE_10000;
            case 4:
                return PEOPLE_1000000;
            default:
                return PEOPLE_100;
        }
    }
}