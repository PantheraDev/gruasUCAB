using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderMs.Common.Dtos
{
    //* Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DistanceMatrixResponse
    {
        public List<string> destination_addresses { get; set; } 
        public List<string> origin_addresses { get; set; }
        public List<Row> rows { get; set; }
        public string status { get; set; }
    }
    public class Distance
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Duration
    {
        public string text { get; set; }
        public int value { get; set; }
    }

    public class Element
    {
        public Distance distance { get; set; }
        public Duration duration { get; set; }
        public string status { get; set; }
    }
    public class Row
    {
        public List<Element> elements { get; set; }
    }

}