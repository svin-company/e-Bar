using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using eBar.DataStorage.Exceptions;


namespace eBar.DataStorage.Providers.EntityAttributeProvider
{
    public class EntityAttributeProvider:IEntityAttributeProvider
    {
        public string GetTableName<T>()
        {
            var attributes = typeof(T).GetCustomAttributes(typeof(TableAttribute), true);
            if (attributes.Length > 0)
            {
                var tableAttribute = (TableAttribute)attributes[0];
                return tableAttribute.Name;
            }

            throw new ClassEmptyColumnAttributeException($"У класса {typeof(T)} отсутствует атрибут с названием таблицы");
        }

        public Dictionary<string, string> GetColumnAndModelPropertyNames<T>(bool addKey = true)
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            Dictionary<string, string> columnNameProperyName = new Dictionary<string, string>();

            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault()
                    is ColumnAttribute columnAttribute)
                {
                    if (addKey || propertyInfo.GetCustomAttributes(typeof(KeyAttribute), true).Length == 0)
                    {
                        if (columnAttribute.Name != null)
                        {
                            columnNameProperyName.Add(columnAttribute.Name, propertyInfo.Name);
                        }
                    }
                }
                else
                {
                    throw new PropertyEmptyAttributeException($"У свойства {propertyInfo.Name} отсутствует соответствующий атритбут");
                }
            }
            return columnNameProperyName;
        }

        public (string, string) GetKeyColumnAndPropertyName<T>()
        {
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (propertyInfo.GetCustomAttributes(typeof(KeyAttribute), true).Length > 0 &&
                    propertyInfo.GetCustomAttributes(typeof(ColumnAttribute), true).FirstOrDefault() is ColumnAttribute columnAttribute)
                {
                    return (columnAttribute.Name, propertyInfo.Name);
                }
            }

            throw new ClassEmptyKeyAttributeException($"У класса {typeof(T)} отсутствует атрибут ключа");
        }

        public string GetFormattedColumnAndModelPropertyNames<T>(Dictionary<string, string> columnPropertyNames, string format)
        {
            var result = string.Join(", ", columnPropertyNames.
                Select(columnPropertyNames => string.Format(format, columnPropertyNames.Key, columnPropertyNames.Value)));
            return result;
        }
    }
}
