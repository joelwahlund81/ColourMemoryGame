using ColourMemoryGame2.Models;

namespace ColourMemoryGame2.Services.Interfaces
{
    public interface IDataService
    {
        void SaveData(ColourMemoryGame data);
        ColourMemoryGame LoadData(Guid id);
    }
}
