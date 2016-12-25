namespace Demeter.Inventory
{
    /// <summary>
    ///  Storage type
    /// </summary>
    public class StorageType
    {
        #region Properties

        /// <summary>
        ///  The name of the storage type
        /// </summary>
        public virtual string Name { get; set; }

        #endregion

        #region Methods

        public virtual void Add(Freshie freshie)
        {
            freshie.CurrentStorage = this;
        }

        public virtual void Remove(Freshie freshie)
        {
            freshie.CurrentStorage = null;
        }

        #endregion
    }
}
