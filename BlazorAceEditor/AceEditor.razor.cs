using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorAceEditor.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorAceEditor
{
    public partial class AceEditor
    {
        [Parameter] 
        public string Id { get; set; } = default!;
        [Parameter] 
        public AceEditorOptions Options { get; set; } = default!;
        [Parameter]
        public string? Style { get; set; }

        [Inject] private AceEditorJsInterop AceEditorInterop { get; set; } = default!;

        protected override Task OnParametersSetAsync()
        {
            if (string.IsNullOrEmpty(Id))
                Id = $"ace-editor-{Guid.NewGuid()}";
            if (string.IsNullOrEmpty(Style))
                Style = "height:30rem; width:50rem; padding:1rem";
            return base.OnParametersSetAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await AceEditorInterop.Init(Id, Options);
            await base.OnAfterRenderAsync(firstRender);
        }

        public async Task<string> GetValue()
        {
            return await AceEditorInterop.GetValue();
        }

        public async Task SetValue(string text)
        {
            await AceEditorInterop.SetValue(text);
        }
    }
}
