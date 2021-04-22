using System.Net;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Persistence;
using API.Services.Common;
using API.Services.MeasurementsDev.Dtos;
using AutoMapper;
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

        public async Task<Response<Measurement>> GetMeasurementAsync(int id)
        {
            var measurement = await _context.Measurements
                .Include(x => x.AccelerometerMeasurements)
                .Include(x => x.GyroscopeMeasurements)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (measurement != null)
                return new Response<Measurement>(){
                    HttpStatusCode = HttpStatusCode.OK,
                    Payload = measurement
                };
            return new Response<Measurement>(){
                HttpStatusCode = HttpStatusCode.NotFound,
            };
        }
    }
}
