using Demeter.Abstract;

namespace Demeter.Entities
{
    using FreshnessLevels = StuffType.FreshnessLevels;

    public class Stuff : BaseItem
    {
        public Stuff()
        {
        }

        public Stuff(StuffType type) : base(type, type.Name, type.Description)
        {
        }

        public Stuff(StuffType type, string name) : base(type, name, type.Description)
        {
        }

        public Stuff(StuffType type, string name, string description) : base(type, name, description)
        {
        }

        public Stuff(Stuff copy) : base(copy.ItemType, copy.Name, copy.Description)
        {
            Quantity = copy.Quantity;
            Freshness = copy.Freshness;
        }

        #region BaseItem members

        public override bool IsNull => Quantity == 0;

        public override IItem Clone() => new Stuff(this);

        #endregion

        public double Quantity { get; set; }

        public double Freshness { get; set; }

        public StuffType StuffType => (StuffType)base.ItemType;

        public FreshnessLevels FreshnessLevel => StuffType.FressnessToLevel(Freshness);
    }
}
