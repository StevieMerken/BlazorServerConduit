namespace BlazorServerConduit.Models
{
    public record ApiResponse<T>(T? SuccessData, GenericErrorModel? Error)
    {
        public bool IsSuccess => SuccessData is not null;
    };
}