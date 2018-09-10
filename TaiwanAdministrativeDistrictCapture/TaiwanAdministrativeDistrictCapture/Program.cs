using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaiwanAdministrativeDistrictCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isContinue = true;
            var filePath = "TaiwanAdministrativeDistrict.json";
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("找不到行政區設定檔");
            }
            List<string> compareKeys = new List<string>();
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                var taiwanAdministrativeDistrictDictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
                foreach (var keyValPair in taiwanAdministrativeDistrictDictionary)
                {
                    foreach (var val in keyValPair.Value)
                    {
                        compareKeys.Add($"{keyValPair.Key}{val}");
                    }
                }
            }
            while (isContinue)
            {
                Console.WriteLine("請輸入地址或輸入空白帶入預設地址");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    input = "110臺北市信義區信義路五段7號";
                    Console.WriteLine($"範例地址:{input}");
                }

                input = input.Replace("台", "臺");


                var found = compareKeys.FirstOrDefault(c => input.Contains(c));
                if (found != null)
                {
                    Console.WriteLine($"地址屬於:{found}");
                }
                else
                {
                    Console.WriteLine("未比對出符合行政區");
                    continue;
                }
                Console.WriteLine();
            }


        }
    }
}
