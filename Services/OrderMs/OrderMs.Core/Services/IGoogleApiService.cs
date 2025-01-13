
using OrderMs.Common.Dto.Response;
using OrderMs.Common.Dtos;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Core.Services
{
    public interface IGoogleApiService
    {
        Task<List<TowsAvaliable>> GetDistanceAvailableVehiclesToOrigin(
               List<GetTow> listVehicleAvailableDto,
               IncidentDestinyLocation incidentLocation
           );

        Task<DistanceComplete> GetDistanceCompleteRoute(string origin, string destiny);

    //     Task<VehicleDto?> GetDistanceVehicleToOrigin(
    //         VehicleDto? vehicleDto,
    //         double originLatitude,
    //         double originLongitude
    //     );


    }
}
