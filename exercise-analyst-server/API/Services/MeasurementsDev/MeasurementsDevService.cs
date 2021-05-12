using System.Net;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Persistence;
using API.Services.Common;
using API.Services.MeasurementsDev.Dtos.Requests;
using API.Services.MeasurementsDev.Dtos.Responses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services.MeasurementsDev
{
    public class MeasurementsDevService: IMeasurementsDevService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MeasurementsDevService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<int>> CreateMeasurementAsync(CreateMeasurementDevRequest request)
        {
            var measurement = _mapper.Map<Measurement>(request);

            _context.Add(measurement);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
                return new Response<int>(){
                    HttpStatusCode = HttpStatusCode.Created,
                    Payload = measurement.Id
                };
            return new Response<int>(){
                HttpStatusCode = HttpStatusCode.BadRequest,
                Errors = new[] { "No data has been saved" }
            };
        }

        public async Task<Response<GetMeasurementsResponse>> GetMeasurementsAsync()
        {
            var measurements = await _context.Measurements
                .ProjectTo<MeasurementForGetMeasurementsResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var payload = new GetMeasurementsResponse
            {
                Measurements = measurements
            };

            var response = new Response<GetMeasurementsResponse>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Payload = payload
            };

            return response;
        }

        public async Task<Response<GetMeasurementResponse>> GetMeasurementAsync(int measurementId)
        {
            var measurement = await _context.Measurements
                .Include(m => m.AccelerometerMeasurements)
                .Include(m => m.GyroscopeMeasurements)
                .ProjectTo<GetMeasurementResponse>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(m => m.Id == measurementId);

            if (measurement == null)
            {
                return new Response<GetMeasurementResponse>
                {
                    HttpStatusCode = HttpStatusCode.NotFound,
                    Errors = new[] {$"No measurement with id {measurementId} was found"}
                };
            }

            var response = new Response<GetMeasurementResponse>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Payload = measurement
            };

            return response;
        }
    }
}
