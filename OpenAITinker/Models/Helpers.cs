using System.Reflection;

namespace OpenAITinker.Models
{
    public static class Helpers
    {

        private static List<DemoContent>? _allContent;
        public static List<DemoContent>? GetAllContent()
        {
            if (_allContent is not null) return _allContent;
            var json = ReadResource("DemoContent.json");
            _allContent = DemoContent.FromJson(json);
            return _allContent;
        }

        public static DemoContent GetContent(string topic)
        {
            var allContent = GetAllContent();
            var result = allContent?.Find(x => x.Topic == topic);
            return result ?? new DemoContent
            {
                Topic = topic,
                Description = $"There's no description for {topic}",
                Short = $"There's no short for {topic}"
            };
        }
        public static string ReadResource(string name)
        {

            var assembly = Assembly.GetExecutingAssembly();
            var resourcePath = assembly.GetManifestResourceNames()
                .Single(str => str.EndsWith(name));
            using var stream = assembly.GetManifestResourceStream(resourcePath);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

    }
}
