using System.ComponentModel.DataAnnotations;

namespace DotNetNinja.ApiTemplate.Controllers.v1
{
    public class StateModel
    {
        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }
    }
}