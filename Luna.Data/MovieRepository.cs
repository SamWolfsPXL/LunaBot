using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Luna.Data.DomainClasses;
using Microsoft.Extensions.Configuration;

namespace Luna.Data
{
    public class MovieRepository
    {
        private readonly HttpClient _httpClient;
        private readonly IConfigurationRoot _config;

        public MovieRepository(string baseUrl, IConfigurationRoot config)
        {
            _httpClient = new HttpClient{
                BaseAddress = new Uri(baseUrl)};
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _config = config;
        }

        public async Task<Movie> GetMovieByNameAsync(string name)
        {
            var movieUrl = $"/?t={name}&apikey=" + _config["tokens:omdb"];
            Movie movie = new Movie();
            HttpResponseMessage reponse = await _httpClient.GetAsync(movieUrl);
            if (reponse.IsSuccessStatusCode)
            {
                return await reponse.Content.ReadAsAsync<Movie>();
            }
            throw new HttpRequestException(reponse.ReasonPhrase);
        }
    }
}
