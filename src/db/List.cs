
using System.Globalization;
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
    public int Index { get { return _index; } }
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
        this.Map(cell => Console.WriteLine(FileModifier.PeopleToLine(cell.value)));
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

                if (pos <= _count)
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
        if (_count == 0)
            return null;

        PeopleModel[] peoples = new PeopleModel[_count];

        Celula aux = primeiro.prox;
        for (int i = 0; i < _count; i++, aux = aux.prox)
        {
            peoples[i] = aux.value;
        }

        return peoples;
    }

    public void ToList(PeopleModel[] peoples)
    {
        Celula aux = primeiro.prox;
        for (int i = 0; i < _count; i++, aux = aux.prox)
        {
            aux.value = peoples[i];
        }
    }

    public bool Update(PeopleModel people)
    {
        Console.WriteLine("people.UserId: " + people.UserId);
        Console.WriteLine("people.Index: " + people.Index);
        int pos = 0;
        Celula first = primeiro.prox;

        while (first != null)
        {
            if (first.value.UserId == people.UserId)
            {
                first.value = people;

                return true;
            }
            Console.WriteLine("Posição: " + pos);
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

    public void Sort(Func<PeopleModel, string> condition)
    {
        var peopleList = ToArray();
        QuickSort(peopleList, 0, peopleList.Length - 1, condition);
        ToList(peopleList);
    }

    private void QuickSort(PeopleModel[] list, int left, int right, Func<PeopleModel, string> condition)
    {
        if (left < right)
        {
            int pivotIndex = Partition(list, left, right, condition);
            QuickSort(list, left, pivotIndex - 1, condition);
            QuickSort(list, pivotIndex + 1, right, condition);
        }
    }

    private int Partition(PeopleModel[] list, int left, int right, Func<PeopleModel, string> condition)
    {
        PeopleModel pivotValue = list[right];
        int i = left - 1;

        for (int j = left; j < right; j++)
        {
            if (condition(list[j]).CompareTo(condition(pivotValue)) <= 0)
            {
                i++;
                Swap(list, i, j);
            }
        }

        Swap(list, i + 1, right);
        return i + 1;
    }

    private void Swap(PeopleModel[] list, int i, int j)
    {
        PeopleModel temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

    public PeopleModel Search(Func<PeopleModel, string> property, string value)
    {
        var list = ToArray();
        QuickSort(list, 0, list.Length - 1, property);

        return BinarySearch(list, value, property);
    }

    public PeopleModel BinarySearch(PeopleModel[] list, string searchValue, Func<PeopleModel, string> property)
    {
        int low = 0;
        int high = list.Length - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            string midValue = property(list[mid]);

            int comparison = string.Compare(midValue, searchValue, StringComparison.Ordinal);
            if (comparison == 0)
            {
                return list[mid];
            }
            else if (comparison < 0)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }

        return null; // Elemento não encontrado
    }
}
