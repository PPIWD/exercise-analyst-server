using API.Domain.Models;
using API.Persistence;
using API.Services.Common;
using API.Services.Measurements.Dtos.Requests;
using API.Domain.Utils;
using API.Services.Measurements.Dtos_ml;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace API.Services.Measurements
{
    public class MeasurementsService : IMeasurementsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;

        public MeasurementsService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<Response> CreateMeasurementAsync(CreateMeasurementRequest request)
        {
            var userName = _httpContextAccessor.HttpContext.User?.Claims?
                    .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?
                    .Value;

            ApplicationUser user = null;

            if (userName != null)
                user = await _context.Users.FirstOrDefaultAsync(x => string.Equals(x.UserName.ToLower(), userName.ToLower()));

            if (user == null)
                return new Response(HttpStatusCode.NotFound, new[] { "Nie znaleziono użytkownika w bazie" });
            
            HttpResponseMessage responseMessage;
            try{
            var acc = Newtonsoft.Json.JsonConvert.SerializeObject(request.AccelerometerMeasEntities.Select(x => (MLAccelerometer)x));//Newtonsoft.Json.JsonConvert.SerializeObject(request.AccelerometerMeasEntities.Take(1));
            var gyro = Newtonsoft.Json.JsonConvert.SerializeObject(request.GyroscopeMeasEntities.Select(x => (MLGyroscope)x));//Newtonsoft.Json.JsonConvert.SerializeObject(request.GyroscopeMeasEntities.Take(1));
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("accelerometerMeasEntities", acc),
                new KeyValuePair<string, string>("gyroscopeMeasEntities", gyro)
            });
            responseMessage = await _httpClient.PostAsync(_httpClient.BaseAddress.ToString(), data);
            }
            catch(Exception){
                return new Response(HttpStatusCode.NotFound, new[] { "Błąd Komunikacji z serwerem ML" });
            }
            string activity = await responseMessage.Content.ReadAsStringAsync();
            int repetitions = 0;


            var exercise = new Exercise()
            {
                Activity = activity,
                Repetitions = repetitions,
                User = user,
                DateTimeStart = GetDateTimeStart(request),
                DateTimeEnd = GetDateTimeEnd(request),
            };

            _context.Exercises.Add(exercise);

            var result = await _context.SaveChangesAsync();

            if (result > 0)
                return new Response(HttpStatusCode.Created);

           return new Response(HttpStatusCode.NotFound, new[] { "Błąd zapisu w bazie danych" });
        }

        private DateTime GetDateTimeEnd(CreateMeasurementRequest request)
        {
            try
            {
                var maxAccelerometerUtc = request.AccelerometerMeasEntities.Max(x => x.TimestampUtc);
                var maxGyroscopeUtc = request.GyroscopeMeasEntities.Max(x => x.TimestampUtc);

                var max = maxAccelerometerUtc > maxGyroscopeUtc ? maxAccelerometerUtc : maxGyroscopeUtc;

                return UnixTimeStampToDateTime(max);

            }
            catch (Exception)
            {
                return DateTime.UtcNow;
            }
        }

        private DateTime GetDateTimeStart(CreateMeasurementRequest request)
        {
            try
            {
                var minAccelerometerUtc = request.AccelerometerMeasEntities.Min(x => x.TimestampUtc);
                var minGyroscopeUtc = request.GyroscopeMeasEntities.Min(x => x.TimestampUtc);

                var min = minAccelerometerUtc < minGyroscopeUtc ? minAccelerometerUtc : minGyroscopeUtc;

                return UnixTimeStampToDateTime(min);
            }
            catch (Exception)
            {
                return DateTime.UtcNow;
            }
        }

        private static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
