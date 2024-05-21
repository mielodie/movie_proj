﻿namespace movie_project.Payloads.Responses
{
    public class ResponseObject<T>
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public ResponseObject()
        {

        }
        public ResponseObject(int status, string message, T data)
        {
            Status = status;
            Message = message;
            Data = data;
        }
        public ResponseObject<T> ResponseObjectSuccess(string message, T data)
        {
            return new ResponseObject<T>(StatusCodes.Status200OK, message, data);
        }
        public ResponseObject<T> ResponseObjectError(int stutusCodes, string message, T data)
        {
            return new ResponseObject<T>(stutusCodes, message, data);
        }
    }
}
