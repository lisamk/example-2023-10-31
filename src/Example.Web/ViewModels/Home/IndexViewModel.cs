using System.ComponentModel.DataAnnotations;

namespace Example.Web.ViewModels.Home;

public class IndexViewModel
{
    [Required] public string FirstName { get; set; } = "";
    [Required] public string LastName { get; set; } = "";
    [Required] public string Email { get; set; } = "";
    public bool ShowSuccessMessage { get; set; }
    public string? ErrorMessage { get; set; }
}
