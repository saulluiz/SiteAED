public static class DB
{
    public static Lista LIST = new Lista();
    private const int PAGE_SIZE = 10;
    public static string CurrentFile;
    

    public static void Create(PeopleModel pessoa, int file)
    {
        pessoa.Index = LIST.Index;
        pessoa.DateOfBirth = pessoa.DateOfBirth.Date;
        LIST.Add(pessoa);
        FileModifier.Create(PeopleFiles.GetFile(file), pessoa);
    }

    public static bool Delete(string userID, int file)
    {
        if(LIST.Count == 0)
            FileModifier.ReadFile(PeopleFiles.GetFile(file));

        if (LIST.Remove(userID))
        {
            Console.WriteLine("Deletado");
            FileModifier.DeleteLine(PeopleFiles.GetFile(file), userID);
            return true;
        }
        
        Console.WriteLine("Não deletado");
        return false;
    }

    public static bool Update(PeopleModel pessoa, int file)
    {
        if(LIST.Count == 0)
            FileModifier.ReadFile(PeopleFiles.GetFile(file));

        if (LIST.Update(pessoa))
        {
            Console.WriteLine("To conseguindo fazer");
            FileModifier.ReWriteFile(PeopleFiles.GetFile(file), LIST.ToArray());
            return true;
        }
        return false;
    }

    public static PeopleModel[] Read(int file)
    {
        return LIST.ToArray();
    }

    public static PeopleModel[] Read(int file, int page)
    {
        var peoples = LIST.ToArray();
        var currentPage = new PeopleModel[PAGE_SIZE];

        for (int i = (page - 1) * PAGE_SIZE; i < i + PAGE_SIZE; i++)
        {
            if (i >= peoples.Length)
                break;

            currentPage[i] = peoples[i];
        }

        return peoples;
    }

}