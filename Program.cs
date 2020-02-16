using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

            Console.WriteLine("************************************");

            var jsonString =
                "{\"gyumolcs\":{\"alma\":{\"szin\":\"piros\",\"alak\":\"kerek\"},\"korte\":{\"szin\":\"zold\",\"alak\":\"korte\",\"darab\":2},\"szilva\":{\"szin\":\"lila\",\"alak\":\"ovalis\",\"foo\":\"bar\"}},\"zoldseg\":{\"paprika\":{\"szin\":\"sarga\"}}}";

            var flatJson = FlatteningJson(jsonString);


            foreach (var kvp in flatJson)
            {
                Console.WriteLine($"{kvp.Key} : {kvp.Value}");
            }


            Console.Read();
        }

        public static Dictionary<string,string> FlatteningJson(string jsonString)
        {
            JObject jsonObject = JObject.Parse(jsonString);
            IEnumerable<JToken> jTokens = jsonObject.Descendants().Where(p => p.Count() == 0);
            Dictionary<string, string> results = jTokens.Aggregate(new Dictionary<string, string>(), (properties, jToken) =>
            {
                properties.Add(jToken.Path, jToken.ToString());
                return properties;
            });

            return results;
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
