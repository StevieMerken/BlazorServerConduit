namespace BlazorServerConduit.Models
{
    public record ApiResponse<T>(T? SuccessData, GenericErrorModel? Error) where T : class
    {
        public static ApiResponse<T> FromSucces(T? successData) => new ApiResponse<T>(successData, null);

        public static ApiResponse<T> FromError(GenericErrorModel error) => new ApiResponse<T>(null, error);

        public bool IsSuccess => SuccessData is not null;
    };
}