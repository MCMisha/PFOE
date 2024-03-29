using WebApi.Models;
using WebApi.Repositories;

namespace WebApi.Services;

public class SettingsService
{
    private readonly SettingsRepository _settingsRepository;

    public SettingsService(IConfiguration configuration)
    {
        _settingsRepository = new SettingsRepository(configuration);
    }

    public Setting? Get(int userId)
    {
        return _settingsRepository.GetByUserId(userId);
    }

    public void Add(Setting settings)
    {
        _settingsRepository.Add(settings);
    }

    public void Update(Setting settings)
    {
        if (_settingsRepository.GetByUserId(settings.UserId) != null)
        {
            _settingsRepository.Update(settings);
        }
    }
}