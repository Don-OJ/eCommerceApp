namespace eCommerceApp.Application.Services.Interface.Logging
{
    public interface IAppLogger<T>
    {
        void LogInformation(string message);
        void LogWarning(string message);
        //void LogDebug(string message , params object[] args);
        void LogError(Exception ex,string message);
    }
}
