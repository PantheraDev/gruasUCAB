

namespace OrderMs.Common.Dto.Response
{
    public class TowsAvaliable
    {

        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string TowLocation { get; set; }
        public string TowAvailability { get; set; }
        public string TowType { get; set; }
        public Guid ProviderId { get; set; }
        public Guid? TowDriver { get; set; }
        public string Distance { get; set; }
        public int DistanceValue { get; set; }
        public string Eta { get; set; }
        public int EtaValue { get; set; }

        public TowsAvaliable() { }
        public TowsAvaliable(Guid id, string licensePlate, string towLocation, string towAvailability, string towType, Guid providerId, Guid? towDriver, string distance, int distanceValue, string eta, int etaValue)
        {
            Id = id;
            LicensePlate = licensePlate;
            TowLocation = towLocation;
            TowAvailability = towAvailability;
            TowType = towType;
            ProviderId = providerId;
            TowDriver = towDriver;
            Distance = distance;
            DistanceValue = distanceValue;
            Eta = eta;
            EtaValue = etaValue;
        }
    }
}
