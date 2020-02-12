using System;
using System.Collections;
using System.Reflection;

namespace reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            var ass = Assembly.GetEntryAssembly();
            var obj = new {
                gyumolcs = new {
                    alma = new {
                        szin = "piros",
                        alak = "kerek"
                    },
                    korte = new {
                        szin = "zold",
                        alak = "korte",
                        darab = 2
                    },
                    szilva = new {
                        szin = "lila",
                        alak = "ovalis",
                        valami = "asd"
                    }
                },
                zoldseg = new {
                    paprika = new {
                        szin = "sarga"
                    }
                }
            };

            // var type = obj.GetType().GetProperties();

            // foreach (var item in type)
            // {
            //    Console.WriteLine($"{item.Name}");
            //    foreach (var i in item.GetType().GetProperties())
            //    {
            //        System.Console.WriteLine($"\t{i.Name}");
            //    }          
            // }

            PrintProps(obj);            

        }

        public static void PrintProps(object obj, string link = null)
        {
            if(obj == null) return;
            var type = obj.GetType();
            var propInfoArray = type.GetProperties();
            System.Console.WriteLine("x");
            foreach (var prop in propInfoArray)
            {   
                object propValues = prop.GetValue(obj);
                var elements = propValues.GetType().GetProperties(); // itt van pl. gyumolcs.alma, gyumolcs.korte, gyumolcs.szilva
                if (elements != null) {
                    Console.WriteLine(link);
                    foreach (var cp in elements)
                    {
                        PrintProps(cp, $"{prop.Name}.");
                    }
                }
                else return;
            }

        }
    }
}
