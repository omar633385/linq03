using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using static linq03.ListGenerator;
using static System.Net.Mime.MediaTypeNames;
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
            var result01 = numbers.SkipWhile(n => n % 3 != 0).SkipWhile(n => n % 3 == 0);
            //PrintCollection(result01);
            #endregion

            #region 5. Get the elements of the array starting from the first element less than its position.
            var result02 = numbers.SkipWhile((n, i) => n > i);
            //PrintCollection(result02);
            #endregion

            #endregion

            #region LINQ - Quantifiers

            #region 1. Determine if any of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First) contain the substring 'ei'.

            string[] words = File.ReadAllLines("dictionary_english.txt");
            Console.WriteLine(words.Any(w => w.Contains("ei")));
            

            #endregion

            #region 2. Return a grouped a list of products only for categories that have at least one product that is out of stock
            var groupedOutOfStock = from p in ProductsList
                                    group p by p.Category into g
                                    where g.Any(g => g.UnitsInStock ==0)
                                    select new{category=g.Key,products= string.Join(", ", g.Select(p=>p.ProductName))};

            //PrintCollection(groupedOutOfStock);

            #endregion

            #region 3. Return a grouped a list of products only for categories that have all of their products in stock.

            var groupedProducts = from p in ProductsList
                                  group p by p.Category into g
                                  where g.All(g => g.UnitsInStock>0)
                                  select new{category=g.Key,products= string.Join(", ", g.Select(p=>p.ProductName))};
            //PrintCollection(groupedProducts);


            #endregion
            #endregion

            #region LINQ – Grouping Operators

            #region 1.	Use group by to partition a list of numbers by their remainder when divided by 5
            List<int> numbers01 = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            var result03 = from n in numbers01
                           group n by n % 5
                           into g
                           select new { g.Key,values= g.Select(m => m).ToList()};


            //foreach (var item in result03)
            //{
            //    Console.WriteLine($"Numbers with a remainder of {item.Key} when divided by 5:");
            //    foreach (var item1 in item.values)
            //    {
            //        Console.WriteLine(item1);
            //    }
            //}

            #endregion

            #region 2.	Uses group by to partition a list of words by their first letter.Use dictionary_english.txt for Input
            var result04 = from n in words
                           group n[0] by words into g
                           select new { g.Key, words = g.ToList() };
            //foreach (var item in result04)
            //{
            //    Console.WriteLine($"{item.Key}:{string.Join(", ", item.words)}");
            //}

            #endregion

            #region 3. Use Group By with a custom comparer that matches words that are consists of the same Characters Together
            String[] Arr = { "from", "salt", "earn", " last", "near", "form" };

            var result05 = Arr.GroupBy(word=>word,new CustomComparer());
            //foreach (var group in result05)
            //{
            //    Console.WriteLine($"{string.Join(", ", group)}");
            //}
            #endregion
            #endregion

        }
    }
}

