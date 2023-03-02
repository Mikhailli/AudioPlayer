#nullable enable
using Flurl;
using AudioPlayer.Models.ApiRequestModels;

namespace AudioPlayer.Client.Services;

public static class Api
{
    public static class Audios
    {
        public static string GetAudios(string baseUrl, AudiosSelectParameters? selectParameters = null)
        {
            var url = new Url($"{baseUrl}/api/Home/get");
            if (selectParameters is not null)
            {
                url.SetQueryParam("start", selectParameters.Start);
                url.SetQueryParam("length", selectParameters.Length);
                url.SetQueryParam("searchPattern", selectParameters.SearchPattern ?? "*");
                url.SetQueryParam("orderColumnName", selectParameters.OrderColumnName);
                url.SetQueryParam("isAscending", selectParameters.IsAscending);
            }

            return url.ToString();
        }

        public static string GetAudio(string baseUrl, int audioId) => $"{baseUrl}/api/Home/{audioId}";
        public static string DeleteAudio(string baseUrl, int audioId) => $"{baseUrl}/api/Home/{audioId}";
        public static string UpdateAudio(string baseUrl, int audioId) => $"{baseUrl}/api/Home/{audioId}";
    }
}