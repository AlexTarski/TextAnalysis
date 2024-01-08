using NUnit.Framework.Internal.Execution;
using static System.Net.Mime.MediaTypeNames;

namespace TextAnalysis;

static class SentencesParserTask
{
    public static List<List<string>> ParseSentences(string text)
    {
        var sentencesList = new List<List<string>>();
        List<string> sentences = Split(text, new char[] { '.', '!', '?', ';', ':', '(', ')' });
        List <char> wordsDelimiters = new();

        foreach (var sentence in sentences)
        {
            foreach (var character in sentence) 
            {
                if (!char.IsLetter(character) && character != '\'')
                {
                    wordsDelimiters.Add(character);
                }
            }
            List<string> sentenceToAdd = Split(sentence, wordsDelimiters.ToArray());
            if (sentenceToAdd.Any())
            {
                sentenceToAdd = sentenceToAdd.ConvertAll(d => d.ToLower());
                sentencesList.Add(sentenceToAdd);
            }
        }

        return sentencesList;
    }

    private static List<string> Split (string stringToSplit, char[] delimiters)
    {
        List<string> splittedString = stringToSplit.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();

        return splittedString;
    }
}