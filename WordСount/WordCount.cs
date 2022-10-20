using System.Collections.Concurrent;

namespace WordСount
{
    public class WordCount
    {
        public string? line;

        public Dictionary<string, int> result = new Dictionary<string, int>();
        public ConcurrentDictionary<string, int> result2 = new ConcurrentDictionary<string, int>();

        private Dictionary<string, int> WordCountUniq(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                if ((line = sr.ReadToEnd()) != null)
                {
                    var words = line.Split(' ', '-', ':', '.', '"', '\'', '!', '?', ')', '(', ',', '\t', '\n')
                                    .Where(q => !string.IsNullOrEmpty(q));

                    foreach (string word in words)
                    {
                        string w = word.Trim().ToLower();

                        if (!result.ContainsKey(w))
                        {
                            result.Add(w, 1);
                        }
                        else
                        {
                            result[w]++;
                        }
                    }
                }
            }
            return result.OrderByDescending(w => w.Value).ToList().ToDictionary(key => key.Key, value => value.Value);
        }

        public ConcurrentDictionary<string, int> WordCountUniq2(string path)
        {
            Parallel.ForEach(File.ReadLines(path), (line, _, lineNumber) =>
            {
                var words = line.Split(' ', '-', ':', '.', '"', '\'', '!', '?', ')', '(', ',', '\t', '\n')
                            .Where(q => !string.IsNullOrEmpty(q));

                foreach (string word in words)
                {
                    string w = word.Trim().ToLower();

                    if (!result2.ContainsKey(w))
                    {
                        result2.TryAdd(w, 1);
                    }
                    else
                    {
                        result2[w]++;
                    }
                }
            });

            var res1 = result2.OrderByDescending(w => w.Value).ToList().ToDictionary(key => key.Key, value => value.Value);

            var res2 = new ConcurrentDictionary<string, int>(res1);

            return res2;
        }
    }
}