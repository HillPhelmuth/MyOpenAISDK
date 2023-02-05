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

        public const string ImageDescription =
            "OpenAI's GPT-3 API includes the \"DALL·E\" service, which allows developers to generate unique and creative images from textual descriptions. This service leverages the generative abilities of GPT-3 to produce high-quality, diverse images based on natural language input. The DALL·E service provides a powerful tool for developers looking to add a creative touch to their applications, and it demonstrates the versatility of GPT-3 in handling a wide range of tasks beyond language processing. Overall, the GPT-3 API's image services offer a flexible and versatile solution for developers looking to enhance their applications with advanced AI capabilities.";

        public const string CompletionsDescription =
            "OpenAI's GPT-3 Completions API is a powerful tool for generating text completions in a wide range of applications. The API leverages the advanced language understanding capabilities of GPT-3 to generate relevant, accurate, and human-like completions for a given prompt. The API is easy to use and can be integrated into a variety of applications, from generating product descriptions to writing entire articles. With its ability to handle a wide range of text generation tasks, the GPT-3 Completions API provides a flexible and efficient solution for developers looking to enhance their applications with AI-powered text generation capabilities.";

        public const string CodeDescription =
            "OpenAI's Codex models, as part of the GPT-3 API, provide a powerful solution for code generation and explanation. The Codex code completion model can generate relevant and accurate code completions for a given prompt, while also explaining code in human language. This makes it a valuable tool for developers looking to enhance their productivity and understanding of code, regardless of their skill level. The Codex code completion model leverages the advanced language understanding capabilities of GPT-3 to provide clear, concise, and human-like explanations of code constructs and programming concepts, making it an efficient solution for developers looking to integrate AI-powered code generation and explanation capabilities into their applications.";

        public const string ModerationDescription =
            "OpenAI's GPT-3 API includes moderation services that allow developers to automatically detect and filter out inappropriate or toxic content in text. The API leverages the advanced language understanding capabilities of GPT-3 to accurately classify text based on its content and context. The moderation services can be used to screen comments, messages, and other forms of user-generated content, ensuring that they meet specified standards for quality and appropriateness. The API provides developers with a flexible and efficient solution for moderating large volumes of text data, reducing the need for manual moderation and enabling the creation of safer, more user-friendly applications. With its ability to accurately classify text, the GPT-3 API moderation services provide a valuable tool for developers looking to enhance their applications with advanced AI-powered moderation capabilities.";
    }
}
