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
    public class MachineLearningService : IMachineLearningService
    {
        private readonly HttpClient _httpClient;

        public MachineLearningService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response<(string, int)>> Predict(CreateMeasurementRequest request)
        {
            HttpResponseMessage responseMessage;
            try
            {
                var acc = Newtonsoft.Json.JsonConvert.SerializeObject(request.AccelerometerMeasEntities.Select(x => (MLAccelerometer)x));
                var gyro = Newtonsoft.Json.JsonConvert.SerializeObject(request.GyroscopeMeasEntities.Select(x => (MLGyroscope)x));
                var data = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("accelerometerMeasEntities", acc),
                    new KeyValuePair<string, string>("gyroscopeMeasEntities", gyro)
                });
                responseMessage = await _httpClient.PostAsync(_httpClient.BaseAddress.ToString(), data);
            }
            catch (Exception)
            {
                return new Response<(string, int)>(HttpStatusCode.NotFound, new[] { "Communication error with ML server" });
            }
            string activity = await responseMessage.Content.ReadAsStringAsync();
            int repetitions = 0;

            var response = new Response<(string, int)>();
            response.Payload = (activity, repetitions);

            return response;
        }

    }
}
