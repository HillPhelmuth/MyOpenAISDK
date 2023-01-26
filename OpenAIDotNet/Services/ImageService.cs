using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using OpenAIDotNet.Extensions;
using OpenAIDotNet.Models;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;

namespace OpenAIDotNet.Services
{
    public class ImageService
    {
        private readonly HttpClient _httpClient;
        
        private readonly Endpoints _endPoints;
        public ImageService(HttpClient httpClient, Endpoints endPoints)
        {
            _httpClient = httpClient;
            _endPoints = endPoints;
        }

        public async Task<ImageResponseModel> Create(ImageCreateRequest request)
        {
            var url = _endPoints.ImageCreate;

            return await _httpClient.PostReadJsonAsync<ImageResponseModel>(url, request);
        }

        public async Task<ImageResponseModel> Edit(ImageEditRequest imageEditCreateRequest)
        {
            var url = _endPoints.ImageEditCreate;
            var multipartContent = new MultipartFormDataContent();
            if (imageEditCreateRequest.User != null) multipartContent.Add(new StringContent(imageEditCreateRequest.User), "user");
            if (imageEditCreateRequest.ResponseFormat != null) multipartContent.Add(new StringContent(imageEditCreateRequest.ResponseFormat), "response_format");
            if (imageEditCreateRequest.Size != null) multipartContent.Add(new StringContent(imageEditCreateRequest.Size), "size");
            if (imageEditCreateRequest.N != null) multipartContent.Add(new StringContent(imageEditCreateRequest.N.ToString()!), "n");

            multipartContent.Add(new StringContent(imageEditCreateRequest.Prompt), "prompt");
            multipartContent.Add(new ByteArrayContent(imageEditCreateRequest.Image), "image", imageEditCreateRequest.ImageName);
            multipartContent.Add(new ByteArrayContent(imageEditCreateRequest.Mask), "mask", imageEditCreateRequest.MaskName);
            return await _httpClient.PostFileReadJsonAsync<ImageResponseModel>(url, multipartContent);
        }
        public async Task<ImageResponseModel> CreateVariation(ImageEditRequest imageEditCreateRequest)
        {
            //_httpClient.DefaultRequestHeaders.Add("content-type", "multipart/form-data");
            var url = _endPoints.ImageVariationCreate;
            var multipartContent = new MultipartFormDataContent();
            if (imageEditCreateRequest.User != null) multipartContent.Add(new StringContent(imageEditCreateRequest.User), "user");
            if (imageEditCreateRequest.ResponseFormat != null) multipartContent.Add(new StringContent(imageEditCreateRequest.ResponseFormat), "response_format");
            if (imageEditCreateRequest.Size != null) multipartContent.Add(new StringContent(imageEditCreateRequest.Size), "size");
            if (imageEditCreateRequest.N != null) multipartContent.Add(new StringContent(imageEditCreateRequest.N.ToString()!), "n");

            multipartContent.Add(new ByteArrayContent(imageEditCreateRequest.Image), "image", imageEditCreateRequest.ImageName);

            //return await _httpClient.PostFileAndReadAsAsync<ImageCreateResponse>(_endpointProvider.ImageVariationCreate(), multipartContent);
            return await _httpClient.PostFileReadJsonAsync<ImageResponseModel>(url, multipartContent);
        }
    }
}
