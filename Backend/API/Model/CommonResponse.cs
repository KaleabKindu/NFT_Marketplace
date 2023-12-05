namespace Application.Model
{
    public class CommonResponse<T>
    {
        public bool Success { get; set; }
        public object Error { get; set; }
        public T Value { get; set; }
        public string Message { get; set; }

        public static CommonResponse<T> Successful(T value) => new CommonResponse<T> { Success = true, Value = value };
        public static CommonResponse<T> Successful(T value,string message) => new CommonResponse<T> { Success = true, Value = value ,Message = message};
        public static CommonResponse<T> Failed(Object error) => new CommonResponse<T> { Success = false, Error = error };
        public static CommonResponse<T> Failed(Object error,string message) => new CommonResponse<T> { Success = false, Error = error,Message= message};

    }
}
