namespace OpenAITinker.Models
{
    public static class Helpers
    {
        public static double GetCosineSimilarity(List<double> v1, List<double> v2)
        {
            var n = v2.Count < v1.Count ? v2.Count : v1.Count;
            var dot = 0.0d;
            var mag1 = 0.0d;
            var mag2 = 0.0d;
            for (var i = 0; i < n; i++)
            {
                dot += v1[i] * v2[i];
                mag1 += Math.Pow(v1[i], 2);
                mag2 += Math.Pow(v2[i], 2);
            }

            return dot / (Math.Sqrt(mag1) * Math.Sqrt(mag2));
        }
    }
}
