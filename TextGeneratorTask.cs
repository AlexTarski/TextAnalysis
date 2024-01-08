namespace TextAnalysis;

static class TextGeneratorTask
{
    public static string ContinuePhrase(
        Dictionary<string, string> nextWords,
        string phraseBeginning,
        int wordsCount)
    {
        List<string> phrase = new();
        CreatePhrase(phraseBeginning, phrase);

        while (wordsCount > 0)
        {
            if (phrase.Count > 1)
            {
                string nextWord;

                if (nextWords.TryGetValue(string.Concat(phrase[^2], " ", phrase[^1]), out nextWord))
                {
                    phrase.Add(nextWord);
                    wordsCount--;
                }
                else if (nextWords.TryGetValue(phrase[^1], out nextWord))
                {
                    phrase.Add(nextWord);
                    wordsCount--;
                }
                else
                {
                    break;
                }
            }
            else if (phrase.Count == 1)
            {
                string nextWord;

                if (nextWords.TryGetValue(phrase[^1], out nextWord))
                {
                    phrase.Add(nextWord);
                    wordsCount--;
                }
                else
                {
                    break;
                }
            }
            else { break; }
        }

        string result = string.Join(" ", phrase);
        return result;
    }

    private static void CreatePhrase(string phraseBeginning, List<string> phrase)
    {
        if (phraseBeginning.Contains(' '))
        {
            phrase.AddRange(phraseBeginning.Split(' ', StringSplitOptions.RemoveEmptyEntries));
        }
        else
        {
            phrase.Add(phraseBeginning);
        }
    }
}