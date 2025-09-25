namespace eBar.DataStorage.Providers.EntityAttributeProvider
{
    public interface IEntityAttributeProvider
    {
        public string GetTableName<T>();
        public Dictionary<string, string> GetColumnAndModelPropertyNames<T>(bool addKey = true);
        public (string, string) GetKeyColumnAndPropertyName<T>();
        public string GetFormattedColumnAndModelPropertyNames<T>(Dictionary<string, string> columnPropertyNames, string format);
    }
}
