using eBar.Configuration;

namespace eBar.DataStorage
{
    public class DbConfigReader
    {
        public string Connection { get; set; }
        public DbConfigReader(IConfigReader configReader) 
        {
            Connection = configReader.GetConfigValue<string>("DataBase:DBConnection");
        }
    }
}
