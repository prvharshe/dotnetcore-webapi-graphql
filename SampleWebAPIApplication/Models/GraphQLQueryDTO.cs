using Newtonsoft.Json.Linq;

namespace SampleWebAPIApplication.Models
{
    public class GraphQLQueryDTO
    {
        public string OperationName { get; set; }
        public string NamedQuery { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; }
    }
}
