using System.Text;

static class FileModifier
{
    private const string PATH_TO_FILES = "DataCSV/";

    public static void Create(string file, PeopleModel pessoa)
    {
        string pathToFile = PATH_TO_FILES + file;

        using (StreamWriter sw = new StreamWriter(pathToFile, true))
        {
            sw.WriteLine(PeopleToLine(pessoa));
        }
    }

    public static string PeopleToLine(PeopleModel pessoa)
    {
        var line = new StringBuilder();

        line.Append($"{pessoa.Index},");
        line.Append($"{pessoa.UserId},");
        line.Append($"{pessoa.FirstName},");
        line.Append($"{pessoa.LastName},");
        line.Append($"{pessoa.Sex},");
        line.Append($"{pessoa.Email},");
        line.Append($"{pessoa.Phone},");
        line.Append($"{pessoa.DateOfBirth.ToShortDateString()},");
        line.Append($"{pessoa.JobTitle}");

        return line.ToString();
    }

    public static void ReadFile(string file)
    {
        string pathToFile = PATH_TO_FILES + file;
        DB.LIST = new Lista();

        using (StreamReader sw = new StreamReader(pathToFile))
        {
            string line = sw.ReadLine();
            while ((line = sw.ReadLine()) != null)
            {
                DB.LIST.Add(LineToPeople(line));
            }
        }
        Console.WriteLine("lista carregada, tamanho:" + DB.LIST.Count);
    }

    public static PeopleModel LineToPeople(string line)
    {
        string[] peopleData = line.Split(",");
        int index = int.Parse(peopleData[0]);
        string userId = peopleData[1];
        string firtName = peopleData[2];
        string lastName = peopleData[3];
        string sex = peopleData[4];
        string email = peopleData[5];
        string phone = peopleData[6];
        string dateOfBirth = peopleData[7];
        string jobTitle;

        if (peopleData.Length > 9)
        {
            StringBuilder aux = new StringBuilder();
            for (int i = 8; i < peopleData.Length - 1; i++)
            {
                aux.Append(peopleData[i]);
                aux.Append(',');

            }
            aux.Append(peopleData[peopleData.Length - 1]);
            jobTitle = aux.ToString();
        }
        else
        {
            jobTitle = peopleData[8];
        }

        return new PeopleModel
        {
            Index = index,
            UserId = userId,
            FirstName = firtName,
            LastName = lastName,
            Sex = sex,
            Email = email,
            Phone = phone,
            DateOfBirth = DateTime.Parse(dateOfBirth),
            JobTitle = jobTitle
        };
    }

    public static void DeleteLine(string file, string userID)
    {
        string pathToFile = PATH_TO_FILES + file;
        RemoveSpecificLine(pathToFile, (l) => l.Contains(userID + ","));
    }

    public static void ReWriteFile(string file, PeopleModel[] peoples)
    {
        string pathToFile = PATH_TO_FILES + file;
        File.Delete(pathToFile);

        using StreamWriter sw = new StreamWriter(pathToFile);
        for (int i = 0; i < peoples.Length; i++)
        {
            sw.WriteLine(PeopleToLine(peoples[i]));
        }
        
    }

    public static void RemoveSpecificLine(string filePath, Func<string, bool> lineCondition)
    {
        // Read all lines from the file
        var lines = File.ReadAllLines(filePath).ToList();

        // Remove the line that matches the condition
        lines.RemoveAll(line => lineCondition(line));

        // Write the remaining lines back to the file
        File.WriteAllLines(filePath, lines);
    }
}