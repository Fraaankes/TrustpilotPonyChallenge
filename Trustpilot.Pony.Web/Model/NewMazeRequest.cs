using System.ComponentModel.DataAnnotations;

namespace Trustpilot.Pony.Web.Model
{
    public class NewMazeRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
