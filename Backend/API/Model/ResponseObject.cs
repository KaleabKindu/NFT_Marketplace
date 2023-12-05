using System;

namespace API.Model
{
    public class ResponseObject
    {
        public string Message { get; set;}
        public Object Error { get; set; }

        public static ResponseObject Factory(string message) => new ResponseObject { Message = message };
        public static ResponseObject FactoryWithError(string message,Object error) => new ResponseObject{ Message = message,Error = error };
       

    }
}
