namespace Demeter.Inventory
{
    public class Bin : StorageType
    {
        #region Properties

        public override string Name
        {
            get { return "Bin"; }
        }

        #endregion

        #region Methods

        public override void Add(Freshie freshie)
        {
            freshie.Type.Instances.Remove(freshie);
            base.Add(freshie);
        }

        #endregion
    }
}
