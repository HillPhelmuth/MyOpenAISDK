namespace OpenAIDotNet
{
    public static class GptModels
    {
        public enum BaseModel
        {
            Ada,
            Babbage,
            Curie,
            Davinci,
            Cushman
        }

        public enum Model
        {
            Ada,
            Babbage,
            Curie,
            Davinci,

            TextAdaV1,
            TextBabbageV1,
            TextCurieV1,
            TextDavinciV1,

            TextDavinciV2,
            TextDavinciV3,

            CurieInstructBeta,
            DavinciInstructBeta,

            CurieSimilarityFast,

            TextSimilarityAdaV1,
            TextSimilarityBabbageV1,
            TextSimilarityCurieV1,
            TextSimilarityDavinciV1,

            TextSearchAdaDocV1,
            TextSearchBabbageDocV1,
            TextSearchCurieDocV1,
            TextSearchDavinciDocV1,

            TextSearchAdaQueryV1,
            TextSearchBabbageQueryV1,
            TextSearchCurieQueryV1,
            TextSearchDavinciQueryV1,

            TextEditDavinciV1,
            CodeEditDavinciV1,

            CodeSearchAdaCodeV1,
            CodeSearchBabbageCodeV1,

            CodeSearchAdaTextV1,
            CodeSearchBabbageTextV1,

            CodeDavinciV1,
            CodeCushmanV1,

            CodeDavinciV2
        }

        public enum Subject
        {
            Text,
            Code
        }

        public static string Ada => "ada";
        public static string Babbage => "babbage";
        public static string Curie => "curie";
        public static string Davinci => "davinci";
        public static string Cushman => "cushman";

      

        public static string TextDavinciV1 => GetModelName(BaseModel.Davinci, Subject.Text, "001");
        public static string TextDavinciV2 => GetModelName(BaseModel.Davinci, Subject.Text, "002");
        public static string TextDavinciV3 => GetModelName(BaseModel.Davinci, Subject.Text, "003");
        public static string TextAdaV1 => GetModelName(BaseModel.Ada, Subject.Text, "001");
        public static string TextBabbageV1 => GetModelName(BaseModel.Babbage, Subject.Text, "001");
        public static string TextCurieV1 => GetModelName(BaseModel.Curie, Subject.Text, "001");

        public static string CodeDavinciV1 => GetModelName(BaseModel.Davinci, Subject.Code, "001");
        public static string CodeCushmanV1 => GetModelName(BaseModel.Cushman, Subject.Code, "001");
        public static string CodeDavinciV2 => GetModelName(BaseModel.Davinci, Subject.Code, "002");

        

        public static string GetModelName(this BaseModel baseModel, Subject? subject = null, string? version = null)
        {
            return GetModelName(baseModel.EnumToString(), subject?.EnumToString(baseModel.EnumToString()), version);
        }

        public static string GetModelName(string baseEngine, string? subject, string? version)
        {
            var response = subject ?? $"{baseEngine}";

            if (!string.IsNullOrEmpty(version))
            {
                response += $"-{version}";
            }

            return response;
        }


       

        private static string EnumToString(this BaseModel baseEngine)
        {
            return baseEngine switch
            {
                BaseModel.Ada => Ada,
                BaseModel.Babbage => Babbage,
                BaseModel.Curie => Curie,
                BaseModel.Davinci => Davinci,
                BaseModel.Cushman => Cushman,
                _ => throw new ArgumentOutOfRangeException(nameof(baseEngine), baseEngine, null)
            };
        }

        public static string EnumToString(this Subject subject, string baseEngine)
        {
            return string.Format(subject switch
            {
                //{0}-{1}
                Subject.Text => "text-{0}",
                Subject.Code => "code-{0}",
                _ => throw new ArgumentOutOfRangeException(nameof(subject), subject, null)
            }, baseEngine);
        }
    }
}