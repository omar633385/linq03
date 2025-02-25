using System.Collections;
using System.Data;
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

            var UniqueCategories = ProductsList.Select(p => p.Category).Distinct();
            //PrintCollection(UniqueCategories);
            #endregion

            #region 2. Produce a Sequence containing the unique first letter from both product and customer names
            var FirstLetterOfProducts = ProductsList.Select(p => p.ProductName.FirstOrDefault()).Where(c => c != '\0');
            var FirstLetterOfCustomers = CustomersList.Select(c => c.CustomerName.FirstOrDefault()).Where(c => c != '\0');
            var uniqueLetters = FirstLetterOfProducts.Concat(FirstLetterOfCustomers).Distinct();
            //foreach (var item in uniqueLetters)
            //{
            //    Console.Write($"{item} ");   
            //}
            #endregion


            #region 3. Create one sequence that contains the common first letter from both product and customer names.
            var CommonLetters = FirstLetterOfCustomers.Intersect(FirstLetterOfProducts);

            //foreach (var item in CommonLetters)
            //{
            //    Console.Write($"{item} ");
            //}
            #endregion

            #region 4. Create one sequence that contains the first letters of product names that are not also first letters of customer names.
            var letters = FirstLetterOfProducts.Except(FirstLetterOfCustomers);
            //foreach (var item in letters)
            //{
            //    Console.Write($"{item} ");
            //}

            #endregion

            #region 5. Create one sequence that contains the last Three Characters in each name of all customers and products, including any duplicates
            var LastThreeLettersOfProductsName = ProductsList.Select(p => new string(p.ProductName.TakeLast(3).ToArray())).Where(s => !string.IsNullOrEmpty(s));
            var LastThreeLettersOfCustomersName = CustomersList.Select(c => new string(c.CustomerName.TakeLast(3).ToArray())).Where(s => !string.IsNullOrEmpty(s));
            var result = LastThreeLettersOfCustomersName.Union(LastThreeLettersOfProductsName);
            //foreach (var item in result)
            //{
            //    Console.Write($"{item+" "}");   
            //}
            #endregion

            #endregion

            #region LINQ - Partitioning Operators

            #region 1. Get the first 3 orders from customers in Washington
            var ThreeOrders = CustomersList.Where(c => c.City == "Washington").SelectMany(c => c.Orders).Take(3);
            //PrintCollection(ThreeOrders);
            #endregion

            #region 2. Get all but the first 2 orders from customers in Washington.
            var TwoOrders = CustomersList.Where(c => c.City == "Washington").Select(c => new
            {
                c.CustomerName,
                orders = c.Orders.Take(2)
            });



            #endregion

            #region 3. Return elements starting from the beginning of the array until a number is hit that is less than its position in the array.
                          //  0  1  2  3  4  5  6  7  8  9
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            //var result=numbers.SkipWhile((n, i) => n > i);
            //PrintCollection(result);


            #endregion

            #region 4.Get the elements of the array starting from the first element divisible by 3.
            var result01 = numbers.SkipWhile(n=>n%3!=0).SkipWhile(n => n % 3 == 0);
            //PrintCollection(result01);
            #endregion

            #region 5. Get the elements of the array starting from the first element less than its position.
            var result02 = numbers.SkipWhile((n, i) => n > i);
            //PrintCollection(result02);
            #endregion
        }
        #endregion
    }

}

