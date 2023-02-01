using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAIDotNet.Models.Requests
{
    public class FileList
    {
        public byte[]? File { get; set; }
        public string Purpose { get; set; }
    }
}
