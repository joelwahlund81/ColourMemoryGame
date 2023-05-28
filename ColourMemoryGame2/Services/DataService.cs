using ColourMemoryGame2.Models;
using ColourMemoryGame2.Services.Interfaces;
using Newtonsoft.Json;

namespace ColourMemoryGame2.Services
{
    public class DataService : IDataService
    {
        private string filePath = "games.json";

        public ColourMemoryGame LoadData(Guid id)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);

                    var games = JsonConvert.DeserializeObject<ColourMemoryGame>(jsonData);
                    //var games = JsonConvert.DeserializeObject<List<ColourMemoryGame>>(jsonData);

                    if (games != null)
                    {
                        if (games.Id == id)
                        {
                            return games;
                        }

                        //var game = games.FirstOrDefault(g => g.Id == id);

                        //if (game != null)
                        //{
                        //    return game;
                        //}
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            
            return null;
        }

        public void SaveData(ColourMemoryGame data)
        {
            // Save as a array
            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, jsonData);
        }
    }
}
