namespace BookStore.Shared;

public class Result
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public Result(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public static Result Ok()
    {
        return new Result(true, "Operation was Ok");
    }

    public static Result Ok(string message)
    {
        return new Result(true, message);
    }

    public static Result Error()
    {
        return new Result(false, "Operation was Failed");
    }

    public static Result Error(string message)
    {
        return new Result(false, message);
    }
}