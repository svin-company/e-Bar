namespace eBar.Configuration;

public interface IConfigReader 
{
    public T? GetConfigValue<T>(string section);
}