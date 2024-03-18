// IGNORE FOR THE TIME BEING

public class BannedWords
{
    public string[] bannedWords = new string[] {"fuck", "shit", "clit", "vagina", "tit", "boob", "cunt", "asshole", "penis", "weiner"};

    // Constructor to initialize banned words
    public BannedWords(string[] words)
    {
        bannedWords = words;
    }

    // Method to check if any of the given words contain banned words
    public bool CheckHiddenBannedWord(string[] words)
    {
        foreach (string word in words)
        {
            foreach (string bannedWord in bannedWords)
            {
                if (word.Contains(bannedWord))
                {
                    return true; // Word contains a banned word
                }
            }
        }
        return false; // No banned words found
    }
}
