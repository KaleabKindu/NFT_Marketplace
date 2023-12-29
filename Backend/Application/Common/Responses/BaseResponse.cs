namespace Application.Common.Responses{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";
        public string Error { get; set; } = "";
        public T? Value { get; set; }

        // public BaseResponse(T? Value)
        // {
        //     this.Value = Value;
        // }
    }
}
