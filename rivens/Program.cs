using System.Net;
using System.Text.Json;


CreateExportFromJson(GetData(), Environment.CurrentDirectory + @"\export.csv");



void CreateExportFromJson(string jsonContent, string exportpath)
{
    var items = JsonSerializer.Deserialize(GetData(), typeof(List<Item>), JsonSerializerOptions.Default) as List<Item>;

    var export = "weapon name; tradechat last 5 days; avg price; min price; max price;";
    
    if (items != null)
        foreach (var item in items)
        {
            export += $"\n{item.name};{item.data.Length};{Math.Floor(item.data.Average(x => x.price))};{item.data.Min(x => x.price)};{item.data.Max(x => x.price)}";
        }
    
    File.WriteAllText(exportpath, export);
}

string GetData()
{
    return new WebClient().DownloadString("https://10o.io/pricehistory.json?v2");
}