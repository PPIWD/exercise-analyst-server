using System;
using System.Net;
using System.Threading.Tasks;
using API.Domain.Models;
using API.Infrastructure.Services.MeasurementsDev.Dtos.Requests;
using API.Persistence;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace API.Infrastructure.Services.MeasurementsDev
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

        public async Task<Tuple<HttpStatusCode, string>> CreateMeasurementAsync(CreateMeasurementDevRequest request)
        {
            var measurement = _mapper.Map<Measurement>(request);

            _context.Add(measurement);
            var result = await _context.SaveChangesAsync();

            if(result > 0)
                return new Tuple<HttpStatusCode, string>(HttpStatusCode.Created, "Saved measurement");

            return new Tuple<HttpStatusCode, string>(HttpStatusCode.BadRequest, "No data has been saved");
        }
    }
}
