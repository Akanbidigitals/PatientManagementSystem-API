using System.Text.Json.Serialization;

namespace PatientManagementSystem_API.Domain.Models
{
    public class PatientRecord
    {
        public int Id { get; set; }
        public string? Diagnosis { get; set;}
        public string? Treatment {  get; set;}
        public string? UpdatedAt {  get; set;}
        public string? CreatedAt { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");
        public int PatientId {  get; set; }
        [JsonIgnore]
        public Patient? Patient { get; set; } //Adding this will serve as Navigation property

    }
}
