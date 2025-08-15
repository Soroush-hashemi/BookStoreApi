namespace BookStore.Shared;

public class Response<T>
{
    public T Data { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }

    public Response(bool success, string message, T data)
    {
        Success = success;
        Message = message;
        Data = data;
    }

    public static Response<T> Ok(T data, string message) =>
        new Response<T>(true, message, data);

    public static Response<T> Ok(T data) =>
        new Response<T>(true, "Operation was Ok", data);

    public static Response<T> Error(string message) =>
        new Response<T>(false, message, default);

    public static Response<T> Error() =>
        new Response<T>(false, "Operation was Failed", default);
}