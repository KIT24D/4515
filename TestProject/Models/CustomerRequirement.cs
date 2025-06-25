using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class CustomerRequirement
    {
        public int RequirementID { get; set; }
        public int UserID { get; set; }
        [Required] // 非空约束（若需要）
        public string RequirementType { get; set; }
        [Required]
        public string RequirementDesc { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
    }
}
