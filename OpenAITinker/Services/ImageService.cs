using System.Diagnostics;
using Newtonsoft.Json;
using OpenAIDotNet;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;
using SkiaSharp;
//using System.Drawing;
using ImageSize = OpenAIDotNet.Models.Requests.ImageSize;

//using TinkerWithGpt3.Shared.Models;

namespace OpenAITinker.Services
{
    public class ImageService
    {
        //private readonly IOpenAIService _openAiService;
        private readonly OpenAIDotNetService _openAIDotNetService;
        public ImageService(OpenAIDotNetService openAiDotNetService)
        {
            //_openAiService = openAiService;
            _openAIDotNetService = openAiDotNetService;
        }

        public async Task<ImageResponseModel> GetOriginalAsync(string prompt, ImageSize size = ImageSize.Size512)
        {
            
            var result = await _openAIDotNetService.ImageService.Create(new OpenAIDotNet.Models.Requests.ImageCreateRequest
            {
                Prompt = prompt,
                ImageResponseFormat = ImageFormat.Base64,
                N = 2,
                ImageSize = size
            });
           
            return result;
        }

        public async Task<ImageResponseModel> GetVariationAsync(byte[] originalImage, ImageSize size = ImageSize.Size512)
        {
           
            var result = await _openAIDotNetService.ImageService.CreateVariation(new ImageEditRequest()
            {
                Image = originalImage,
                ImageResponseFormat = ImageFormat.Base64,
                ImageName = "Variation.png",
                N = 2,
                ImageSize = size,
                User = "TestUser"
            });
            
            return result;
        }
        private async Task<ImageResponseModel> GetImageAltered(byte[] originalFile, string originalFileName, byte[]? maskFile, string promptText = "", ImageSize size = ImageSize.Size512)
        {
            var maskFileName = $"Mask_{originalFileName}";
            try
            {
                ImageResponseModel imageResult = await _openAIDotNetService.ImageService.Edit(new ImageEditRequest()
                {
                    Image = originalFile,
                    ImageName = originalFileName,
                    Mask = maskFile,
                    MaskName = maskFileName,
                    Prompt = promptText,
                    N = 2,
                    ImageSize = size,
                    ImageResponseFormat = ImageFormat.Base64,
                    User = "TestUser"
                });
                

                if (imageResult.Successful)
                {
                    Console.WriteLine($"Image Request Successful ({imageResult.CreatedAt})");
                }
                else
                {
                    if (imageResult.Error == null)
                    {
                        throw new Exception("Unknown Error");
                    }

                    Console.WriteLine($"{imageResult.Error.Code}: {imageResult.Error.Message}");
                    throw new Exception(
                        $"Image Request Failed\nMessage: {imageResult.Error?.Message}\nError code: {imageResult.Error?.Code}");
                }
                return imageResult;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

           
        }

        public async Task<ImageResponseModel> ModifyImageAsync(byte[] imageData, string imageName,
            SKRectI[] rectanges, string prompt)
        {
            var imageBmp = SKBitmap.Decode(imageData);
            Console.WriteLine($"Image to modify size:\nWidth: {imageBmp.Width}\nHeight: {imageBmp.Height}");
            var mask = await GetModifiedImageData(imageData, rectanges);
            var maskBmp = SKBitmap.Decode(mask);
            Console.WriteLine($"Modified image (mask) size:\nWidth: {maskBmp.Width}\nHeight: {maskBmp.Height}");
            var result = await GetImageAltered(imageData, imageName, mask, prompt);
            return result;
        }

        public Task<byte[]> GetModifiedImageData(byte[] imageData, SKRectI[] rectanges)
        {
           var result = imageData;
            foreach (var rect in rectanges)
            {
                result = ClearRect(result, rect);
            }
            return Task.FromResult(result);
        }

        private byte[] ClearRect(byte[] imageData, SKRectI rectange)
        {
            var bmp = SKBitmap.Decode(imageData);
          
            bmp.Erase(SKColor.Empty, rectange);

            var image = SKImage.FromBitmap(bmp);
            var data = image.Encode();
            return data.ToArray();
           
        }
        private Task<byte[]> ClearSquare(byte[] imageData, SKRectI square)
        {
            var bmp = SKBitmap.Decode(imageData);
            var sw = Stopwatch.StartNew();
            bmp.Erase(SKColor.Empty, square);
            
            var image = SKImage.FromBitmap(bmp);
            var data = image.Encode();
            var result = data.ToArray();
            sw.Stop();
            Console.WriteLine($"Deleted bitmap section. ({sw.ElapsedMilliseconds}ms)\nImage specs: H:{image.Height} W:{image.Width}\nSquare removed:\n {JsonConvert.SerializeObject(square, Formatting.Indented, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })}");
            return Task.FromResult(result);
            
        }
        public Task<byte[]> Resize(byte[] fileContents, int width, int height,
            SKFilterQuality quality = SKFilterQuality.Medium)
        {
            using var sourceBitmap = SKBitmap.Decode(fileContents);

            using var scaledBitmap = sourceBitmap.Resize(new SKImageInfo(width, height), quality);
            using var scaledImage = SKImage.FromBitmap(scaledBitmap);
            using var data = scaledImage.Encode();

            return Task.FromResult(data.ToArray());
        }

    }
}
