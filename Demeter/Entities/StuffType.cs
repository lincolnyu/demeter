namespace Demeter.Entities
{
    public class StuffType : BaseType<Stuff>
    {
        public enum FreshnessLevels
        {
            Fresh,
            Okish,
            Aged,
            Off,
        }

        public StuffType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get;  }
        public string Description { get; set; }
        
        public virtual Stuff Create(string name)
        {
            return new Stuff(this, name);
        }

        public virtual FreshnessLevels FressnessToLevel(double freshness)
        {
            if (freshness > 0.8) return FreshnessLevels.Fresh;
            else if (freshness > 0.5) return FreshnessLevels.Okish;
            else if (freshness > 0.3) return FreshnessLevels.Aged;
            else return FreshnessLevels.Off;
        }
    }
}
