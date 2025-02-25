﻿using System.Collections;
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

            var UniqueCategories= ProductsList.Select(p => p.Category).Distinct();
            //PrintCollection(UniqueCategories);
            #endregion

            #region 2. Produce a Sequence containing the unique first letter from both product and customer names
           var FirstLetterOfProducts=ProductsList.Select(p=>p.ProductName.FirstOrDefault()).Where(c=>c!= '\0');
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
            var letters= FirstLetterOfProducts.Except(FirstLetterOfCustomers);
            //foreach (var item in letters)
            //{
            //    Console.Write($"{item} ");
            //}

            #endregion

            #region 5. Create one sequence that contains the last Three Characters in each name of all customers and products, including any duplicates
            var LastThreeLettersOfProductsName= ProductsList.Select(p=> new string( p.ProductName.TakeLast(3).ToArray())).Where(s => !string.IsNullOrEmpty(s));
            var LastThreeLettersOfCustomersName = CustomersList.Select(c => new string(c.CustomerName.TakeLast(3).ToArray())).Where(s => !string.IsNullOrEmpty(s));
           var result= LastThreeLettersOfCustomersName.Union(LastThreeLettersOfProductsName);
            foreach (var item in result)
            {
                Console.Write($"{item+" "}");   
            }
            #endregion

            #endregion
        }
    }
}
