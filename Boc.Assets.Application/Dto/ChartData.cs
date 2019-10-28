namespace Boc.Assets.Application.Dto
{
    public class ChartData
    {
        public ChartData(string name, string value, string description)
        {
            Name = name;
            Value = value;
            Description = description;
        }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}