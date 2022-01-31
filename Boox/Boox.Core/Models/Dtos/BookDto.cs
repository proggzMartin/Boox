using System.Text.Json.Serialization;

namespace Boox.Core.Models.Dtos
{
    public class BookDto : BookBase
    {
        [JsonPropertyName("publish_date")]
        public override DateTime Published { get; set; }
    }
}
