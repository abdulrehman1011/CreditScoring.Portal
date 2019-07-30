using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditScoring.Portal.Models
{
    
    public class ApiModel
    {
        public string RequestId { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Access_Token { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Expires_In { get; set; }
    }
}
