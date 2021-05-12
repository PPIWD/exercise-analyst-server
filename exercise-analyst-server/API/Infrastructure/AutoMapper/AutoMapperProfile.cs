using API.Domain.Models;
using API.Services.Auth.Dtos.Responses;
using API.Services.Exercises.Dtos.Responses;
using API.Services.MeasurementsDev.Dtos.Requests;
using API.Services.MeasurementsDev.Dtos.Responses;

using AutoMapper;

namespace API.Infrastructure.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            MapsForMeasurementsDev();
            MapsForAuth();
            MapsForExercises();
        }

        private void MapsForExercises()
        {
            CreateMap<Exercise, ExerciseForGetExercises>();
        }

        private void MapsForMeasurementsDev()
        {
            CreateMap<AccelerometerMeasEntity, AccelerometerMeasurement>()
                .ForMember(acc => acc.MeasurementId,
                    opt => opt.Ignore());

            CreateMap<GyroscopeMeasEntity, GyroscopeMeasurement>()
                .ForMember(gyro => gyro.MeasurementId,
                    opt => opt.Ignore());

            CreateMap<CreateMeasurementDevRequest, Measurement>()
                .ForMember(mes => mes.IdFromMobile,
                    opt => opt
                        .MapFrom(mesDto => mesDto.Id))
                .ForMember(mes => mes.Id,
                    opt => opt.Ignore())
                //GyroscopeMeasEntites->GyroscopeMeasurements
                .ForMember(mes => mes.GyroscopeMeasurements,
                    opt => opt
                        .MapFrom(mesDto => mesDto.GyroscopeMeasEntities))
                //AccelerometerMeasEntities->AccelerometerMeasurements
                .ForMember(mes => mes.AccelerometerMeasurements,
                    opt => opt
                        .MapFrom(mesDto => mesDto.AccelerometerMeasEntities));

            CreateMap<Measurement, MeasurementForGetMeasurementsResponse>();

            CreateMap<Measurement, GetMeasurementResponse>();
            CreateMap<AccelerometerMeasurement, AccelerometerMeasurementForGetMeasurementsResponse>();
            CreateMap<GyroscopeMeasurement, GyroscopeMeasurementForGetMeasurementsResponse>();
        }


        private void MapsForAuth()
        {
            CreateMap<ApplicationUser, LoginResponse>();
            CreateMap<ApplicationUser, RegisterResponse>();
        }
    }
}