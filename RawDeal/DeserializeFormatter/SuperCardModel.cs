namespace RawDeal.DeserializeFormatter;

public abstract class SuperCardModel
{
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public int HandSize { get; set; }
        public int SuperstarValue { get; set; }
        public string? SuperstarAbility { get; set;}
}