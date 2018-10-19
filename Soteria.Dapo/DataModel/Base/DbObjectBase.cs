namespace Soteria.DataComponents.DataModel
{
    public class DbObjectBase
    {
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DbObjectBase()
        {
            IsActive = true;
            IsDeleted = false;
        }
    }
}