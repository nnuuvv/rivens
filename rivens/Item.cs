class Item
{
    public int itemid { get; set; }
    public ItemData[] data { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public float disposition { get; set; }
}
class ItemData
{
    public int price { get; set; }
    public string lastseen { get; set; }
    public Buffs[] buffs { get; set; }
    public int rolls { get; set; }
}
class Buffs
{
    public string tag { get; set; }
    public bool curse { get; set; }
}