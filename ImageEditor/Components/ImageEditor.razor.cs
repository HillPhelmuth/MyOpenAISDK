using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageEditor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SkiaSharp;

namespace ImageEditor.Components
{
    public partial class ImageEditor
    {
        [Parameter]
        [EditorRequired]
        public string? ImageSrc { get; set; }

        [Parameter]
        public EventCallback<SKRectI[]> ImageModsChanged { get; set; }
        [Parameter]
        public SKRectI[]? ImageMods { get; set; }
        [Parameter]
        public string? Description { get; set; }
        [Parameter]
        public EventCallback<string> DescriptionChanged { get; set; }

        [Inject] private IJSRuntime JsRuntime { get; set; } = default!;
        private ImageCanvasInterop ImageCanvasInterop => new(JsRuntime);
        private ElementReference _imageRef;
        
        private string? _image;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await ImageCanvasInterop.DrawImageToCanvas(_imageRef);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task GrabImage()
        {
            var modImage = await ImageCanvasInterop.GrabImageFromCanvas();
            //_image = modImage;
            ImageMods = modImage.ToSkRectIArray();
            await ImageModsChanged.InvokeAsync(ImageMods);
        }

        private async Task Clear()
        {
            await ImageCanvasInterop.ClearSelection();
        }
        private async Task Undo()
        {
            await ImageCanvasInterop.RemoveLast();
        }
    }
}
