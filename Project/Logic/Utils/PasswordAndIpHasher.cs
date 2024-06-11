using BCrypt.Net;

public static class Hasher
{
    public static string HashPasswordOrIp(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
    public static bool ValidatePassword(string password, string correctHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, correctHash);
    }
    public static bool StoreIp(string modelItemStr)
    {
        List<LocalIpModel> _list = GenericAccess<LocalIpModel>.LoadAll();
        LocalIpModel IP = new(modelItemStr);
        if (!_list.Any(ipm => ipm.LocalIP == IP.LocalIP)) {_list.Add(IP); return false;}
        GenericAccess<LocalIpModel>.WriteAll(_list);
        return true;
    }
    // public static bool CheckAdminIp()
}