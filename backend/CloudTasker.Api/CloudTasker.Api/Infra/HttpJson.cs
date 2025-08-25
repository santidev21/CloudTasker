using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CloudTasker.Api.Infra
{
    public static class HttpJson
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static async Task<T?> ReadJsonAsync<T>(this HttpRequestData req)
            => await JsonSerializer.DeserializeAsync<T>(req.Body, Options);

        public static async Task<HttpResponseData> JsonAsync(
            this HttpRequestData req, object payload, HttpStatusCode status = HttpStatusCode.OK)
        {
            var res = req.CreateResponse(status);
            res.Headers.Add("Content-Type", "application/json");
            await res.WriteStringAsync(JsonSerializer.Serialize(payload, Options));
            return res;
        }

        public static HttpResponseData Empty(this HttpRequestData req, HttpStatusCode status = HttpStatusCode.NoContent)
        {
            var res = req.CreateResponse(status);
            return res;
        }
    }
}
