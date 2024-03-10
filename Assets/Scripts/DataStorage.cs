using System.Collections.Generic;

public class DataStorage
{
    private static DataStorage instance;

    public static DataStorage Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DataStorage();
            }
            return instance;
        }
    }

    // Dictionary to store various types of data
    private Dictionary<string, object> dataDictionary = new Dictionary<string, object>();

    // Prevent external instantiation
    private DataStorage() { }

    public void StoreData(string key, object data)
    {
        if (dataDictionary.ContainsKey(key))
        {
            dataDictionary[key] = data;
        }
        else
        {
            dataDictionary.Add(key, data);
        }
    }

    public T GetData<T>(string key)
    {
        if (dataDictionary.ContainsKey(key))
        {
            return (T)dataDictionary[key];
        }
        else
        {
            return default(T);
        }
    }
}
