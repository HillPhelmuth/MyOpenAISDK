using System.ComponentModel.DataAnnotations;
using ImageEditor.Models;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using OpenAITinker.Services;
using SkiaSharp;


namespace OpenAITinker.Pages
{
    public partial class OpenAiImagePage
    {
        [Inject] private HttpClient HttpClient { get; set; } = default!;
        [Inject] private ImageService ImageService { get; set; } = default!;
        private const string UploadedImageStringBase = "data:image/png;base64,";
        private string? _uploadedImageBase64;
        private string? _uploadedImageSrc;
        private string? _uploadedImageName;
        private List<string> _retreivedFiles = new();
        private byte[]? _fileBytes;
        private string? _modifiedImageSrc;
        private List<ImageSize> _imageSizes = Enum.GetValues<ImageSize>().ToList();
        private AlterImageForm _imageForm = new();
        private const int MaxSize = 3 * 1000 * 1024;
        private SKRectI[]? _imageMods;
        private bool _showModifiedImage;
        private bool _isWaiting;
        private class AlterImageForm
        {
            public string? ImageName { get; set; }
            public ImageSize RequestSize { get; set; }
            [Required]
            public string? Description { get; set; }

            public byte[]? FileBytes { get; set; }

            [Required]
            public string? FileBase64 { get; set; }
            [Required]
            public SKRectI[]? ImageMods { get; set; }
        }
       
        private async void HandleChange(string src)
        {
            if (string.IsNullOrEmpty(src)) return;
            _uploadedImageBase64 = src.Replace("data:image/png;base64,","");
            var initBytes = Convert.FromBase64String(_uploadedImageBase64);
            _imageForm.FileBytes = await ImageService.Resize(initBytes, 500, 500);
            var resizedBase64 = Convert.ToBase64String(_imageForm.FileBytes);
            _uploadedImageSrc = $"{UploadedImageStringBase}{resizedBase64}";
            Console.WriteLine($"File byte[] length OnChange: {src.Length}");
            StateHasChanged();
        }

        private async void HandleSubmit(AlterImageForm imageForm)
        {
            _retreivedFiles = new List<string>();
            _isWaiting = true;
            StateHasChanged();
            await Task.Delay(1);
            if (imageForm.ImageMods == null) return;
            var cutoutRect = imageForm.ImageMods;
            _imageForm.ImageName ??= "ImageName";
            var mask = await ImageService.GetModifiedImageData(_imageForm.FileBytes!, cutoutRect);
            _modifiedImageSrc = $"{UploadedImageStringBase}{Convert.ToBase64String(mask)}";
            StateHasChanged();
            await Task.Delay(1);
            var response = await ImageService.ModifyImageAsync(_imageForm.FileBytes!, _imageForm.ImageName, cutoutRect, _imageForm.Description!);
            _retreivedFiles = response.Results.Select(x => $"{UploadedImageStringBase}{x.B64}").ToList();
            _isWaiting = false;
            StateHasChanged();
        }

        private async void HandleModifiedImage(SKRectI[] cutoutRect)
        {
            if (_imageForm.FileBytes == null || string.IsNullOrWhiteSpace(_imageForm.Description)) return;
            _imageForm.ImageName ??= "ImageName";
            var mask = await ImageService.GetModifiedImageData(_imageForm.FileBytes, cutoutRect);
            _modifiedImageSrc = $"{UploadedImageStringBase}{Convert.ToBase64String(mask)}";
            StateHasChanged();
            await Task.Delay(1);
            var response = await ImageService.ModifyImageAsync(_imageForm.FileBytes, _imageForm.ImageName, cutoutRect, _imageForm.Description);
            _retreivedFiles = response.Results.Select(x => $"{UploadedImageStringBase}{x.B64}").ToList();
            StateHasChanged();
        }

        private async void CreateOriginal()
        {
            if (string.IsNullOrWhiteSpace(_imageForm.Description)) return;
            _isWaiting = true;
            StateHasChanged();
            await Task.Delay(1);
            var response = await ImageService.GetOriginalAsync(_imageForm.Description);
            _retreivedFiles = response.Results.Select(x => $"{UploadedImageStringBase}{x.B64}").ToList();
            _isWaiting = false;
            StateHasChanged();
        }
        private async Task HandleGetAltered()
        {
            if (_imageForm.FileBytes == null) return;
            _isWaiting = true;
            StateHasChanged();
            await Task.Delay(1);
            var response = await ImageService.GetVariationAsync(_imageForm.FileBytes);
            Console.WriteLine($"Image Create Response Json:\r\n{JsonConvert.SerializeObject(response, Formatting.Indented)}");
            _retreivedFiles = response.Results.Select(x => $"{UploadedImageStringBase}{x.B64}").ToList();
            _isWaiting = false;
            StateHasChanged();
        }
    }
}
