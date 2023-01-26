﻿using OpenAIDotNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenAIDotNet.Extensions;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;

namespace OpenAIDotNet.Services
{
    public class TextEditService
    {
        private readonly HttpClient _httpClient;
        private readonly Endpoints _endPoints;

        public TextEditService(HttpClient httpClient, Endpoints endPoints)
        {
            _endPoints = endPoints;
            _httpClient = httpClient;
        }

        public async Task<EditResponseModel> RequestEdit(EditRequestModel editRequestModel)
        {
            var url = _endPoints.TextEdit;
            return await _httpClient.PostReadJsonAsync<EditResponseModel>(url, editRequestModel);
        }
    }
}
