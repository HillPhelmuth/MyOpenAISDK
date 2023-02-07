using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models
{
    public class Endpoints
    {
        
        public static string Completion => "/v1/completions";
        public static string ImageCreate => "/v1/images/generations";

        public static string ImageEditCreate => "/v1/images/edits";

        public static string ImageVariationCreate => "/v1/images/variations";

        public static string Moderation => "/v1/moderations";

        public static string TextEdit => "/v1/edits";
        public static string Embedding => "/v1/embeddings";
        public static string FineTuneCreate => "/v1/fine-tunes";
        public static string FileListOrUpload => "/v1/files";
        public static string FileGetOrDelete(string fileId) => $"/v1/files/{fileId}";
    }
}
