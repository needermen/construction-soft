namespace Cs.Service.Common.Models
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        
        public bool ErrorOccured { get; set; }

        public string Message { get; set; }
        
        public T Data { get; set; }
        
        public static ServiceResult<T> Ok()
        {
            return new ServiceResult<T>
            {
                Success = true,
            };
        }

        public static ServiceResult<T> Ok(T data)
        {            
            return new ServiceResult<T>
            {
                Data = data,
                Success = true,
            };
        }

        public static ServiceResult<T> Fail(string message)
        {
            return new ServiceResult<T>()
            {
                Success = false,
                Message = message
            };
        }
        
        public static ServiceResult<T> Error(string message)
        {
            return new ServiceResult<T>()
            {
                ErrorOccured = true,
                Message = message
            };
        }
    }
}