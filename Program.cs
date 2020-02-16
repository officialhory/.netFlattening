using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var obj = new
            {
                gyumolcs = new
                {
                    alma = new
                    {
                        szin = "piros",
                        alak = "kerek"
                    },
                    korte = new
                    {
                        szin = "zold",
                        alak = "korte",
                        darab = 2
                    },
                    szilva = new
                    {
                        szin = "lila",
                        alak = "ovalis",
                        foo = "bar"
                    }
                },
                zoldseg = new
                {
                    paprika = new
                    {
                        szin = "sarga"
                    }
                }
            };

            var dictionary = new Dictionary<string, string>();

            PrintPropsToDictionary(obj, dictionary);
            Console.WriteLine("***********************************");
            foreach (var kvp in dictionary)
            {
                Console.WriteLine($"{kvp.Key} : {kvp.Value}");
            }

            Console.Read();
        }

        public static void PrintPropsToDictionary(object obj, Dictionary<string, string> dic, string link = null)
        {
            var type = obj.GetType();

            if (type.Name.Contains("Anonymous"))
            {
                var props = type.GetProperties().ToList();
                foreach (var p in props.Where(p => props.Count > 0))
                {
                    Console.WriteLine((link == null) ? $"{p.Name}" : $"{link}.{p.Name}");
                    PrintPropsToDictionary(p.GetValue(obj), dic, (link == null) ? $"{p.Name}" : $"{link}.{p.Name}");
                }

            }
            else
            { 
                if (link == null) return;
                dic.Add(link, obj.ToString());
                Console.WriteLine($"{link}.{obj}");
            }
        }
    }
}
