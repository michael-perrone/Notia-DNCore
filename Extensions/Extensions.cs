using Microsoft.AspNetCore.Http;

public static class Extensions 
{
    public static void AddApplicationError(this HttpResponse response, string message)
    {
        response.Headers.Add("Application-Error", message);
        response.Headers.Add("Access-Control-Exposes-Headers", "Application-Error");
        response.Headers.Add("Access-Control-Allow-Origin", "*");
    }
}