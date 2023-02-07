using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using OpenAIDotNet.Extensions;
using OpenAIDotNet.Models;
using OpenAIDotNet.Models.Responses;
using File = OpenAIDotNet.Models.Responses.File;

namespace OpenAIDotNet.Services
{
    public class FileService
    {
        private readonly HttpClient _httpClient;

        public FileService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<FileListData?> GetFileList()
        {
            return await _httpClient.GetFromJsonAsync<FileListData>(Endpoints.FileListOrUpload);
        }

        public async Task<File> UploadFile(byte[] file, string filename, string purpose = "fine-tune")
        {
            var multipartContent = new MultipartFormDataContent();
            multipartContent.Add(new StringContent(purpose), "purpose");
            multipartContent.Add(new ByteArrayContent(file), "file", filename);
            return await _httpClient.PostFileReadJsonAsync<File>(Endpoints.FileListOrUpload, multipartContent);
        }

        public async Task<File?> GetFileInfo(string fileId)
        {
            return await _httpClient.GetFromJsonAsync<File>(Endpoints.FileGetOrDelete(fileId));
        }

        public async Task<FileDeleteResponse?> DeleteFile(string fileId)
        {
            return await _httpClient.DeleteFromJsonAsync<FileDeleteResponse>(Endpoints.FileGetOrDelete(fileId));
        }
    }
}
