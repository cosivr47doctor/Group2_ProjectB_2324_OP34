public class MutablePair<T1, T2> : IPairable<T1, T2>
{
    public T1 Item1 {get; set;}
    public T2 Item2 {get; set;}

    public MutablePair(T1 it1, T2 it2)
    {
        Item1 = it1;
        Item2 = it2;
    }
}
