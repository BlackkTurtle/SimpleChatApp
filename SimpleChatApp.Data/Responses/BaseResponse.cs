using SimpleChatApp.Data.Responses.Enums;
using SimpleChatApp.Data.Responses.Interfaces;

namespace SimpleChatApp.Data.Responses;

public class BaseResponse<T> : IBaseResponse<T>
{
    public string Description { get; set; } = null!;
    public StatusCode StatusCode { get; set; }
    public int ResultsCount { get; set; }
    public T? Data { get; set; }
}