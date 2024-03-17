[System.Serializable]
public class UserData
{
    public string authToken;
    public string email;
    public string name;
    public string message;
    public Data data;
}

[System.Serializable]
public class Data
{
    public string _id;
    public string firstName;
    public string lastName;
    public string email;
    public string password;
    public string username;
    public string uid;
    public string[] roles;
    public bool validated;
    public bool isActive;
    public string createdAt;
    public string lastLogin;
    public string subscriptionLevel;
    public string subscriptionExpiresOn;
    public string paymentMethod;
    public string paymentID;
    public string[] scopes;
}
