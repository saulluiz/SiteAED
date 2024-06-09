using System.Text;

static class DB
{
    private const string PATH_TO_FILES = "DataCSV/";

    public static void Create(string file, PeopleModel pessoa)
    {
        string pathToFile = PATH_TO_FILES + file;

        using (StreamWriter sw = new StreamWriter(pathToFile, true))
        {
            sw.WriteLine( PeopleToLine(pessoa) );
        }
    }

    public static string PeopleToLine(PeopleModel pessoa)
    {
        var line = new StringBuilder();

        line.Append($"{pessoa.UserId},");
        line.Append($"{pessoa.FirstName},");
        line.Append($"{pessoa.LastName},");
        line.Append($"{pessoa.Sex},");
        line.Append($"{pessoa.Email},");
        line.Append($"{pessoa.Phone},");
        line.Append($"{pessoa.DateOfBirth}");
        line.Append($"{pessoa.JobTitle},");

        return line.ToString();
    }
}