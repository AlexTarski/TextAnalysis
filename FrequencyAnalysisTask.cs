namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {
        var result = new Dictionary<string, string>();
        var tempDictionary = new Dictionary<string, Dictionary<string, int>>();
        foreach (var sentence in text)
        {
            if (sentence.Count >= 2)
            {
                for (int i = 0; i <= sentence.Count - 2; i++)
                {
                    if (i == 0)
                    {
                        GetBigram(sentence, i, ref tempDictionary);
                    }
                    else
                    {
                        GetBigram(sentence, i, ref tempDictionary);
                        Get3gram(sentence, i, ref tempDictionary);
                    }
                }
            }
        }

        NgramsSorting(ref tempDictionary, ref result);
        return result;
    }

    private static void NgramsSorting (ref Dictionary<string, Dictionary<string, int>> tempDictionary, ref Dictionary<string, string> result)
    {
        Dictionary<string, int> sortedDictionary = new();

        foreach (var firstKey in tempDictionary) 
        {
            sortedDictionary.Clear();
            sortedDictionary.Add("", 0);
            string currentKey = string.Empty;
            foreach(var secondKey in firstKey.Value) 
            {
                if ((sortedDictionary[currentKey] == secondKey.Value && string.CompareOrdinal(currentKey, secondKey.Key) > 0)
                    || (sortedDictionary[currentKey] < secondKey.Value))
                {
                     sortedDictionary.Remove(currentKey);
                     sortedDictionary.Add(secondKey.Key, secondKey.Value);
                     currentKey = secondKey.Key;
                }
            }

            result.Add(firstKey.Key, currentKey);
        }
    }

    private static void GetBigram(in List<string> sentence, in int position, ref Dictionary<string, Dictionary<string, int>> tempDictionary)
    {
        if (!tempDictionary.ContainsKey(sentence[position]))
        {
            tempDictionary.Add(sentence[position], new Dictionary<string, int> { { sentence[position + 1], 1 } });
        }
        else
        {
            if (!tempDictionary[sentence[position]].ContainsKey(sentence[position + 1]))
            {
                tempDictionary[sentence[position]].Add(sentence[position + 1], 1);
            }
            else
            {
                tempDictionary[sentence[position]][sentence[position + 1]] = tempDictionary[sentence[position]][sentence[position + 1]] + 1;
            }
        }
    }

    private static void Get3gram(in List<string> sentence, in int position, ref Dictionary<string, Dictionary<string, int>> tempDictionary)
    {
        string key = string.Concat(sentence[position - 1], " ", sentence[position]);
        if (!tempDictionary.ContainsKey(key))
        {
            tempDictionary.Add(key, new Dictionary<string, int> { { sentence[position + 1], 1 } });
        }
        else
        {
            if (!tempDictionary[key].ContainsKey(sentence[position + 1]))
            {
                tempDictionary[key].Add(sentence[position + 1], 1);
            }
            else
            {
                tempDictionary[key][sentence[position + 1]] = tempDictionary[key][sentence[position + 1]] + 1;
            }
        }
    }
}