using Example.Web.Models.Database;
using Example.Web.ViewModels.Home;
using Adliance.Buddy;
using Adliance.Buddy.Crypto;
using Microsoft.EntityFrameworkCore;

namespace Example.Web.Factories.ViewModels.Home;

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

    public async Task<IndexViewModel> HandleRegistrationAsync(IndexViewModel viewModel)
    {
        var registration = _db.Registrations.AsEnumerable().FirstOrDefault(r => Crypto.VerifyHashV2(viewModel.Email, r.EmailHash, r.EmailHashSalt));
        
        if (registration == null)
        {
            var emailHashSalt = Guid.NewGuid();
            var emailHash = Crypto.HashV2(viewModel.Email, emailHashSalt);
            registration = new Registration
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                CreatedUtc = DateTime.UtcNow,
                EmailHash = emailHash,
                EmailHashSalt = emailHashSalt
            };
            _db.Registrations.Add(registration);
            await _db.SaveChangesAsync();
            viewModel.ShowSuccessMessage = true;
            _logger.LogInformation($"Registration of {viewModel.FirstName} {viewModel.LastName} stored in database with ID {registration.Id}.");
        }
        else
        {
            viewModel.ErrorMessage = "This email address is already registered.";
            _logger.LogInformation($"Registration of {viewModel.Email} already exists in database with ID {registration.Id}.");
        }

        return viewModel;
    }
}
