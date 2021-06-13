using API.Services.Common;
using API.Services.Measurements.Dtos_ml;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using API.Services.Measurements.Dtos.Requests;

namespace API.Infrastructure.MachineLearning
{
    public class MachineLearning: IMachineLearning
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public MachineLearning(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<Response<(string, int)>> Predict(CreateMeasurementRequest request)
        {
            HttpResponseMessage responseMessage;
            try
            {
                var acc = Newtonsoft.Json.JsonConvert.SerializeObject(request.AccelerometerMeasEntities.Select(x => (MLAccelerometer)x));//Newtonsoft.Json.JsonConvert.SerializeObject(request.AccelerometerMeasEntities.Take(1));
                var gyro = Newtonsoft.Json.JsonConvert.SerializeObject(request.GyroscopeMeasEntities.Select(x => (MLGyroscope)x));//Newtonsoft.Json.JsonConvert.SerializeObject(request.GyroscopeMeasEntities.Take(1));
                var data = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("accelerometerMeasEntities", acc),
new KeyValuePair<string, string>("gyroscopeMeasEntities", gyro)
});
                responseMessage = await _httpClient.PostAsync(_httpClient.BaseAddress.ToString(), data);
            }
            catch (Exception)
            {
                return new Response<(string, int)>(HttpStatusCode.NotFound, new[] { "Błąd Komunikacji z serwerem ML" });
            }
            string activity = await responseMessage.Content.ReadAsStringAsync();
            int repetitions = 0;

            var response = new Response<(string, int)>();
            response.Payload = (activity, repetitions);

            return response;
        }

    }
}
