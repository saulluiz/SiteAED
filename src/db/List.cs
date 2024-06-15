
public class Celula
{
    public Celula prox;
    public PeopleModel value;
    public Celula(PeopleModel? value = null)
    {
        this.prox = null;
        this.value = value;
    }
}

public class Lista
{
    public Celula primeiro, ultimo;
    private int _index;
    public int Index { get {return _index;} }
    private int _count;
    public int Count
    {
        get { return _count; }
    }

    public PeopleModel this[int index]
    {
        get { return ElementoNaPosicao(index).value; }
    }

    public Lista()
    {
        primeiro = new Celula();
        ultimo = primeiro;
        this._count = 0;
        this._index = 1;
    }

    private Celula ElementoNaPosicao(int pos)
    {
        Celula aux = primeiro.prox;
        for (int i = 0; i != pos; i++, aux = aux.prox) ;
        return aux;
    }

    public void InserirInicio(PeopleModel? value = null)
    {
        Celula aux = primeiro.prox;
        primeiro.prox = new Celula(value);
        primeiro.prox.prox = aux;


        _index++;
        _count++;
    }

    public void Add(PeopleModel value)
    {
        ultimo.prox = new Celula(value);
        ultimo = ultimo.prox;

        _index++;
        _count++;
    }

    public void InserirMeio(PeopleModel value, int pos)
    {
        if (pos < 0 || pos > _count)
        {
            throw new Exception("Pos Nao existe");
        }
        if (pos == 0)
        {
            InserirInicio(value);
            return;
        }
        if (pos == _count)
        {
            Add(value);
            return;
        }
        Celula j = this.primeiro;
        for (int i = 0; i < pos; i++, j = j.prox) ;
        Celula aux = j.prox;
        j.prox = new Celula(value);

        j.prox.prox = aux;
        _index++;
        _count++;
    }

    public void Imprimir()
    {
        this.Map(cell => Console.WriteLine(cell.value));
    }

    public PeopleModel Remove(int pos)
    {
        if (pos < 0 || pos > _count - 1)
        {
            throw new Exception("Posicao nao existe");
        }

        Celula aux = primeiro.prox;
        if (pos == 0)
        {
            primeiro.prox = primeiro.prox.prox;
            _count--;
            return aux.value;
        }

        Celula j = primeiro;
        for (int i = 1; i < pos; i++, j = j.prox) ;
        aux = j.prox;
        j.prox = j.prox.prox;
        if (pos == _count - 1)
        {
            ultimo = j.prox;
        }
        _count--;
        return aux.value;
    }

    public bool Remove(string userID)
    {
        int pos = 0;
        Celula first = primeiro.prox;

        while (first != null)
        {
            if (first.value.UserId == userID)
            {
                Console.WriteLine("Posição a ser deletada: " + pos);

                if(pos <= _count)
                    Remove(pos);
                else
                    break;
                
                Console.WriteLine($"Removido: {first.value.UserId}, {first.value.FirstName}");
                _count--;
                return true;
            }
            first = first.prox;
            pos++;
        };

        return false;
    }

    public PeopleModel RemoveFim()
    {
        return Remove(_count - 1);
    }

    public PeopleModel Removeinicio()
    {
        return Remove(0);
    }

    public void Map(Action<Celula> func)
    {
        Celula aux = primeiro.prox;
        while (aux != null)
        {
            func(aux);
            aux = aux.prox;
        }
    }

    public PeopleModel[] ToArray()
    {
        PeopleModel[] peoples = new PeopleModel[_count];

        Celula aux = primeiro.prox;
        for (int i = 0; i < _count; i++, aux = aux.prox)
        {
            peoples[i] = aux.value;
        }

        return peoples;
    }

    public static Lista ToList(PeopleModel[] peoples)
    {
        Lista list = new Lista();

        foreach (var people in peoples)
        {
            list.Add(people);
        }

        return list;
    }

    public bool Update(PeopleModel people)
    {
        int pos = 0;
        Celula first = primeiro.prox;

        while (first != null)
        {
            if (first.value.UserId == people.UserId)
            {
                first.value = people;
                return true;
            }
            first = first.prox;
            pos++;
        };

        return false;
    }
  
    public PeopleModel search(string userId)
    {
        PeopleModel p = new PeopleModel();
        Map((people) =>
        {
            if (people.value.UserId == userId)
                p = people.value;

        });
        return p;
    }
}
