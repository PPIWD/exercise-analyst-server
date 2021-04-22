using API.Domain.Models;
using API.Services.Auth.Dtos;
using API.Services.MeasurementsDev.Dtos;
using AutoMapper;

namespace API.Infrastructure.AutoMapper
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            MapsForMeasurementsDev();
            MapsForAuth();
        }

        private void MapsForMeasurementsDev()
        {
            CreateMap<AccelerometerMeasEntity, AccelerometerMeasurement>()
                //IGNORE MeasurementId->MeasurementId
                .ForMember(acc => acc.MeasurementId,
                    opt => opt.Ignore())

                //Vector.X->X
                .ForMember(acc => acc.X,
                    opt => opt
                        .MapFrom(accDto => accDto.Vector.X))

                //Vector.Y->Y
                .ForMember(acc => acc.Y,
                    opt => opt
                        .MapFrom(accDto => accDto.Vector.Y))

                //Vector.Z->Z
                .ForMember(acc => acc.Z,
                    opt => opt
                        .MapFrom(accDto => accDto.Vector.Z));


            CreateMap<GyroscopeMeasEntity, GyroscopeMeasurement>()
                //IGNORE MeasurementId->MeasurementId
                .ForMember(gyro => gyro.MeasurementId,
                    opt => opt.Ignore())

                //Vector.X->X
                .ForMember(gyro => gyro.X,
                    opt => opt
                        .MapFrom(gyroDto => gyroDto.Vector.X))

                //Vector.Y->Y
                .ForMember(gyro => gyro.Y,
                    opt => opt
                        .MapFrom(gyroDto => gyroDto.Vector.Y))
                //Vector.Z->Z
                .ForMember(gyro => gyro.Z,
                    opt => opt
                        .MapFrom(gyroDto => gyroDto.Vector.Z));


            CreateMap<CreateMeasurementDevRequest, Measurement>()
                //GyroscopeMeasEntites->GyroscopeMeasurements
                .ForMember(mes => mes.GyroscopeMeasurements,
                    opt => opt
                        .MapFrom(mesDto => mesDto.GyroscopeMeasEntities))

                //AccelerometerMeasEntities->AccelerometerMeasurements
                .ForMember(mes => mes.AccelerometerMeasurements,
                    opt => opt
                        .MapFrom(mesDto => mesDto.AccelerometerMeasEntities))

                //Session.Activity->Activity
                .ForMember(mes => mes.Activity,
                    opt => opt
                        .MapFrom(mesDto => mesDto.SessionEntity.Activity))

                //Session.Repetitions->Repetitions
                .ForMember(mes => mes.Repetitions,
                    opt => opt
                        .MapFrom(mesDto => mesDto.SessionEntity.Repetitions))

                //Session.Id->IdFromMobile
                .ForMember(mes => mes.IdFromMobile,
                    opt => opt
                        .MapFrom(mesDto => mesDto.SessionEntity.Id));
        }


        private void MapsForAuth()
        {
            CreateMap<ApplicationUser, LoginResponse>();
            CreateMap<ApplicationUser, RegisterResponse>();
        }
    }
}