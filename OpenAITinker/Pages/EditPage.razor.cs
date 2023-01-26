using Microsoft.AspNetCore.Components;
using OpenAIDotNet;
using OpenAIDotNet.Models.Requests;
using OpenAIDotNet.Models.Responses;

namespace OpenAITinker.Pages
{
    public partial class EditPage
    {
        [Inject] private OpenAIDotNetService OpenAIDotNetService { get; set; } = default!;
        private class EditRequestForm
        {
            public string? Input { get; set; }
            public string? Instruction { get; set; }
            public string? Model { get; set; } = GptModels.TextEditDavinciV1;
            public float? Temperature { get; set; }
            public int Number { get; set; } = 1;
        }

        private EditRequestForm _editRequestForm = new();
        private EditRequestModel _editRequestModel = new() {Model = GptModels.TextEditDavinciV1};
        private EditResponseModel? _editResponse;
        private bool _isBusy;
        private async Task HandleSubmit(EditRequestForm editRequestForm)
        {
            _editRequestModel.Input = editRequestForm.Input;
            _editRequestModel.Instruction = editRequestForm.Instruction;
            _editRequestModel.Temperature = editRequestForm.Temperature;
            _editRequestModel.N = editRequestForm.Number;
            _isBusy = true;
            StateHasChanged();
            await Task.Delay(1);

            _editResponse = await OpenAIDotNetService.TextEditService.RequestEdit(_editRequestModel);
            _isBusy = false;
            StateHasChanged();
        }
    }
}
