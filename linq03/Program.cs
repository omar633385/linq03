using System.Collections;
using static linq03.ListGenerator;
namespace linq03
{
    internal class Program
    {
        static void PrintCollection(IEnumerable collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }
        static void Main(string[] args)
        {
            #region SetOperators

            #region 1. Find the unique Category names from Product List

            var UniqueCategories= ProductsList.Select(p => p.Category).Distinct();
            PrintCollection(UniqueCategories);
            #endregion
            #endregion
        }
    }
}
