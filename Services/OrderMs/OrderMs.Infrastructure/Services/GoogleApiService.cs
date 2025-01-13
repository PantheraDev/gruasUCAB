
using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using OrderMs.Common.Dto.Response;
using OrderMs.Common.Dtos;
using OrderMs.Core.Services;
using OrderMs.Domain.ValueObjects;

namespace OrderMs.Infrastructure.Services
{
    public class GoogleApiService : IGoogleApiService
    {
        private readonly ILogger<GoogleApiService> _logger;
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "AIzaSyCP7Tpre3yvWJBXBDcrXkNA4tjOX-e_IfA";

        public GoogleApiService(HttpClient httpClient, ILogger<GoogleApiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            //* Configuracion del HttpClient
            _httpClient.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/");
        }

        public async Task<List<TowsAvaliable>> GetDistanceAvailableVehiclesToOrigin(List<GetTow> listVehicleAvailableDto, IncidentDestinyLocation incidentLocation)
        {
            try
            {
                var vehiclesLocationBuilder = new StringBuilder();
                foreach (var item in listVehicleAvailableDto)
                {
                    if (vehiclesLocationBuilder.Length > 0)
                    {
                        vehiclesLocationBuilder.Append('|');
                    }
                    vehiclesLocationBuilder.AppendFormat("{0}", item.towLocation);
                }

                var vehiclesLocation = vehiclesLocationBuilder.ToString();

                var response = await _httpClient.GetAsync($"distancematrix/json?origins={vehiclesLocation}&destinations={incidentLocation.Value}&key={_apiKey}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();

                var distanceMatrix = JsonSerializer.Deserialize<DistanceMatrixResponse>(content);
                var TowsAvaliable = new List<TowsAvaliable>();
                for (var i = 0; i < listVehicleAvailableDto.Count; i++)
                {
                    var row = distanceMatrix!.rows[i];
                    if (row.elements[0].status == "OK")
                    {
                        var tow = new TowsAvaliable();
                        tow.Id = listVehicleAvailableDto[i].id;
                        tow.LicensePlate = listVehicleAvailableDto[i].licensePlate;
                        tow.TowLocation = listVehicleAvailableDto[i].towLocation;
                        tow.TowAvailability = (listVehicleAvailableDto[i].towAvailability == 0) ? "Disponible" : "No Disponible";
                        if (listVehicleAvailableDto[i].towType == 0)
                            tow.TowType = "Small";
                        else if (listVehicleAvailableDto[i].towType == 1)
                            tow.TowType = "Medium";
                        else
                            tow.TowType = "Tall";
                        tow.ProviderId = Guid.Parse(listVehicleAvailableDto[i].providerId);
                        tow.TowDriver = (listVehicleAvailableDto[i].towDriver != null) ? Guid.Parse(listVehicleAvailableDto[i].towDriver!) : null;

                        //*Distance and Eta
                        tow.Distance = row.elements[0].distance.text;
                        tow.DistanceValue = row.elements[0].distance.value;
                        tow.Eta = row.elements[0].duration.text;
                        tow.EtaValue = row.elements[0].duration.value;

                        TowsAvaliable.Add(tow);
                    }
                    else
                        _logger.LogWarning(
                            "There is an issue when trying assign distances with google map api, the status of the distance is: {Status}",
                            row.elements[0].status
                        );
                }

                return TowsAvaliable;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error when trying to calculate distances with google map api. {Message}",
                    ex.Message
                );
                throw;
            }
        }


        public async Task<DistanceComplete> GetDistanceCompleteRoute(string origin, string destiny)
        {
            try
            {
                var response = await _httpClient.GetAsync($"distancematrix/json?origins={origin}&destinations={destiny}&key={_apiKey}");
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStreamAsync();

                var distanceMatrix = JsonSerializer.Deserialize<DistanceMatrixResponse>(content);
                var distanceComplete = new DistanceComplete();
                var row = distanceMatrix!.rows[0];
                if (row.elements[0].status == "OK")
                {
                    distanceComplete.Distance = row.elements[0].distance.text;
                    distanceComplete.DistanceValue = row.elements[0].distance.value;
                    distanceComplete.Eta = row.elements[0].duration.text;
                }
                else
                    _logger.LogWarning(
                        "There is an issue when trying assing the distance for the crane service with google map api, the status of the distance is: {Status}",
                        row.elements[0].status
                    );

                return distanceComplete;
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error when trying to calculate distance for the crane service with google map api. {Message}",
                    ex.Message
                );
                throw;
            }
        }
    }
}