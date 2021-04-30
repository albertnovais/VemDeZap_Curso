using Microsoft.OpenApi.Models;

namespace VemDeZap.Api
{
    internal class Info : OpenApiInfo
    {
        public string Title { get; set; }
        public string Version { get; set; }
    }
}