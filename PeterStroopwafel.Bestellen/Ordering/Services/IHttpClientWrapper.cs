using System;
using System.Net.Http;

namespace Ordering.Services
{
    public interface IHttpClientWrapper
    {
        
        /// <summary>
        /// Get request
        /// </summary>
        /// <exception cref="HttpRequestException">When the HTTP status code is not successful</exception>
        HttpContent Get(HttpRequestMessage request);

        
        /// <summary>
        /// POST request
        /// </summary>
        /// <exception cref="HttpRequestException">When the HTTP status code is not successful</exception>
        HttpResponseMessage Post(Uri requestUri, HttpContent content);
    }
}
