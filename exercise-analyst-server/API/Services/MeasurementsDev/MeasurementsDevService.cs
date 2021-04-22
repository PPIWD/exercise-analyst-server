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

        public async Task<Response> CreateMeasurementAsync(CreateMeasurementDevRequest request)
        {
            var measurement = _mapper.Map<Measurement>(request);

            _context.Add(measurement);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
                return new Response(HttpStatusCode.NoContent);
            return new Response(HttpStatusCode.BadRequest, new[] { "No data has been saved" });
        }
        
        public async Task<Response<GetMeasurementsCsvResponse>> GetMeasurementsCsvAsync()
        {
            var measurements = await _context.Measurements
                .ProjectTo<MeasurementForGetMeasurementsCsvResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();
            
            var payload = new GetMeasurementsCsvResponse
            {
                Measurements = measurements
            };
            
            var response = new Response<GetMeasurementsCsvResponse>
            {
                HttpStatusCode = HttpStatusCode.OK,
                Payload = payload
            };
            
            return response;
        }
    }
}
