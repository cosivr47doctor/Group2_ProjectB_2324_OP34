static class GenerateResvCode
{
    public static string GenerateCode()
    {
        Random random = new Random();
        // generates a code of 6 characters consisting of numbers and capital letters
        string code = "";
        for (int i = 0; i < 6; i++)
        {
            int num = random.Next(0, 36);
            if (num < 10)
            {
                code += num.ToString();
            }
            else
            {
                code += (char)(num + 55);
            }
        }
        return code;
    }
}