namespace MultiSearch.Models;

public interface IResult
{
    bool Success { get; set; }
    List<string> Messages { get; set; }
    string Message { get; }
    Exception Exception { get; set; }
    int ErrorCode { get; set; }
}

public interface IResult<T> : IResult
{
    T Data { get; set; }
}

public class Result : IResult
{
        public static IResult OK()
        {
            return new Result();
        }

        public static IResult Error(string message)
        {
            return new Result(false, message);
        }

        public Result()
        {
            Success = true;
            Messages = new List<string>();
        }

        public Result(bool success)
        {
            Success = success;
            Messages = new List<string>();
        }

        public Result(bool success, string message)
        {
            Success = success;
            Messages = new List<string> { message };
        }

        public Result(bool success, List<string> messages)
        {
            Success = success;
            Messages = messages;
        }

        public Result(Exception exception)
        {
            Success = false;
            Exception = exception;
            Messages = new List<string>{Exception?.Message??""};
        }

        public void AddError(string message)
        {
            Messages.Add(message);
            Success = false;
        }

        public void AddError(Exception exc)
        {
            Messages.Add(exc.Message);
            Exception = exc;
            Success = false;
        }

        public bool Success { get; set; }

        public List<string> Messages { get; set; }

        public string Message
        {
            get => Messages != null ? string.Join(Environment.NewLine, Messages) : string.Empty;
            private set => Messages = new List<string> { value };
        }

        public int ErrorCode { get; set; }

        public Exception Exception { get; set; }
    }

    public class Result<T> : IResult<T>
    {
        public static IResult<T> OK(T data)
        {
            return new Result<T>(data);
        }

        public static IResult<T> Error(string message, T data)
        {
            return new Result<T>(false, message, data);
        }

        public static IResult<T> Error(int code, string message, T data)
        {
            return new Result<T>(code, message);
        }

        public Result()
        {
            Success = true;
            Messages = new List<string>();
        }

        public Result(bool success)
        {
            Success = success;
            Messages = new List<string>();
        }

        public Result(T data)
        {
            Success = true;
            Messages = new List<string>();
            Data = data;
        }

        public Result(bool success, string message)
        {
            Success = success;
            Messages = new List<string> { message };
        }

        public Result(int errorcode,  string message)
        {
            Success = false;
            ErrorCode = errorcode;
            Messages = new List<string> { message };
        }

        public Result(bool success, List<string> messages, T data)
        {
            Success = success;
            Messages = messages;
            Data = data;
        }

        public Result(bool success, string message, T data)
        {
            Success = success;
            Messages = new List<string> { message };
            Data = data;
        }

        public Result(Exception exception)
        {
            Success = false;
            Exception = exception;
            Messages = new List<string>{Exception?.Message??""};
        }

        public Result(Exception exception, T data)
        {
            Success = false;
            Exception = exception;
            Messages = new List<string>{Exception?.Message??""};
            Data = data;
        }

        public void AddError(string message)
        {
            Messages.Add(message);
            Success = false;
        }


        public bool Success { get; set; }


        public List<string> Messages { get; set; }


        public T Data { get; set; }


        public Exception Exception { get; set; }
        public int ErrorCode { get; set; }


        public string Message
        {
            get => Messages != null ? string.Join(Environment.NewLine, Messages) : string.Empty; 
            private set => Messages = new List<string> { value };
        }
}

