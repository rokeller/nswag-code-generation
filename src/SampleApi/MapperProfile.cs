using AutoMapper;
using Sample.Api.Models;

namespace Sample.Api;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Forecast, Controllers.V1.Forecast>()
            .ForMember(
                dest => dest.TemperatureC,
                opt => opt.MapFrom<ConvertToCelsius>())
            .ForMember(
                dest => dest.TemperatureF,
                opt => opt.MapFrom<ConvertToFahrenheit>())
            ;

        CreateMap<Forecast, Controllers.V2.Forecast>();
        CreateMap<WeatherSummary, Controllers.V2.WeatherSummary>();
    }

    private sealed class ConvertToCelsius
        : IValueResolver<Forecast, Controllers.V1.Forecast, int>
    {
        public int Resolve(
            Forecast source,
            Controllers.V1.Forecast destination,
            int destMember,
            ResolutionContext context)
        {
            return source.TemperatureK - 273;
        }
    }

    private sealed class ConvertToFahrenheit
        : IValueResolver<Forecast, Controllers.V1.Forecast, int>
    {
        public int Resolve(
            Forecast source,
            Controllers.V1.Forecast destination,
            int destMember,
            ResolutionContext context)
        {
            return Convert.ToInt32(9f / 5f * source.TemperatureK - 459.67f);
        }
    }
}
