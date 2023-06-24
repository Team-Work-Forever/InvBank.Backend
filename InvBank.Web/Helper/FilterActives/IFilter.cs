namespace InvBank.Web.Helper.FilterActives;

public interface IFilter<T>
{
    T filter(T list, string searchResult);
}