using API.Domain.Models;
using API.Persistence;
using API.Services.Common;
using API.Services.Measurements.Dtos.Requests;
using API.Domain.Utils;

using AutoMapper;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace API.Services.Measurements
{
    public class MeasurementsService : IMeasurementsService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MeasurementsService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
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

            //TODO DODAĆ - TUTAJ POWINIEN ODBYĆ SIĘ STRZAŁ I POWINNIŚMY DOSTAĆ PREDICTION OD ML ALE JESZCZE TEGO NIE MA
            //try
            //{
            //    var json = JsonConvert.SerializeObject(request);
            //    var data = new StringContent(json, Encoding.UTF8, "application/json");
            //    var url = "https://link-do-modelu-ML.pl/predict";
            //    using var client = new HttpClient();
            //    var response = await client.PostAsync(url, data);
            //    string result = response.Content.ReadAsStringAsync().Result;
            //}
            //catch(Exception e)
            //{

            //}

            //TODO USUNĄĆ - zapisujemy i zwracamy jakieś losowe dane dopóki nie mamy danych z ML
            string activity = Activities.ActivitesList[new Random().Next(Activities.ActivitesList.Count)];
            int repetitions = new Random().Next(10, 30);

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
            catch (Exception e)
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
            catch (Exception e)
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
