using SimpleJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace rivens
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateExportFromJson(GetData(), Environment.CurrentDirectory + @"\export.csv");
        }
        static void CreateExportFromJson(string jsonContent, string exportpath)
        {
            JSONNode json = JSONNode.Parse(jsonContent);
            Dictionary<string, object[]> weapons = new Dictionary<string, object[]>();

            foreach (JSONNode weapon in json)
            {
                List<int> prices = new List<int>();
                foreach(JSONNode data in weapon)
                {
                    foreach(JSONNode riven in data)
                    {
                        prices.Add(int.Parse(riven["price"]));
                    }
                }

                object[] dataPoints = new object[] { weapon["data"].Count, Math.Round(prices.Average()), prices.Min(), prices.Max()};
                weapons.Add(weapon["name"].ToString(), dataPoints);
            }

            string export = "weapon name; tradechat last 5 days; avg price; min price; max price;";
            foreach (KeyValuePair<string, object[]> weapon in weapons.OrderBy(key => key.Value[0]))
            {
                export += $"\n{weapon.Key.Trim('\"')};{weapon.Value[0]};{weapon.Value[1]};{weapon.Value[2]};{weapon.Value[3]}";
            }
            System.IO.File.WriteAllText(exportpath, export);
        }
        static string GetData()
        {
            return new WebClient().DownloadString("https://10o.io/pricehistory.json?v2");
        }
    }
}