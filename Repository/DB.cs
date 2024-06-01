using System.Text;

class DB
{
    private const string PATH_TO_FILES = "DataCSV/";
    private string pathToFile;

    public DB(string csvFile)
    {
        this.pathToFile = PATH_TO_FILES + csvFile;
    }

    public void Create(PeopleModel pessoa)
    {
        using (StreamWriter sw = new StreamWriter(pathToFile, true))
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

            sw.WriteLine(line.ToString());
        }
    }
}