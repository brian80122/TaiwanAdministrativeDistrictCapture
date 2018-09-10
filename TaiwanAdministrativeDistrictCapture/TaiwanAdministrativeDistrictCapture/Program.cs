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
            Dictionary<string, string[]> taiwanAdministrativeDistrictDictionary;
            using (StreamReader r = new StreamReader(filePath))
            {
                var json = r.ReadToEnd();
                taiwanAdministrativeDistrictDictionary = JsonConvert.DeserializeObject<Dictionary<string, string[]>>(json);
            }
            while (isContinue)
            {
                Console.WriteLine("請輸入地址或輸入空白帶入預設地址");
                var input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                {
                    input = "110臺北市信義區信義路五段7號";
                }

                var key = taiwanAdministrativeDistrictDictionary.Keys.FirstOrDefault(c => input.Contains(c));
                if (key == null)
                {
                    Console.WriteLine("未比對出符合行政區");
                    continue;
                }

                var comapareDistricts = taiwanAdministrativeDistrictDictionary[key];
                var found = comapareDistricts.FirstOrDefault(c => input.Contains($"{key}{c}"));
                if (found != null)
                {
                    Console.WriteLine($"地址屬於:{key}{found}");
                }
                else
                {
                    Console.WriteLine("未比對出符合行政區");
                    continue;
                }
            }


        }
    }
}
