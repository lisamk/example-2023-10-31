using System.ComponentModel.DataAnnotations;
using Example.Web.Models.Database;

namespace Example.Web.ViewModels.Home;

public class IndexViewModelFactory
{
    private readonly ILogger<IndexViewModelFactory> _logger;
    private readonly Db _db;

    public IndexViewModelFactory(ILogger<IndexViewModelFactory> logger, Db db)
    {
        _logger = logger;
        _db = db;
    }

    public IndexViewModel Create()
    {
        return new IndexViewModel();
    }

    public async Task<IndexViewModel> HandleRegistration(IndexViewModel viewModel)
    {
        var registration = new Registration
        {
            FirstName = viewModel.FirstName, LastName = viewModel.LastName, CreatedUtc = DateTime.UtcNow
        };
        _db.Registrations.Add(registration);
        await _db.SaveChangesAsync();
        viewModel.ShowSuccessMessage = true;
        
        _logger.LogInformation($"Registration of {viewModel.FirstName} {viewModel.LastName} stored in database with ID {registration.Id}.");
        return viewModel;
    }
}

public class IndexViewModel
{
    [Required] public string FirstName { get; set; } = "";
    [Required] public string LastName { get; set; } = "";
    public bool ShowSuccessMessage { get; set; }
}