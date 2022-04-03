using Microsoft.Extensions.Logging;


namespace CF_Utility
{
    public class CatFactManager : ICatFactManager
    {
        private readonly HttpClient _client;
        private readonly ILogger<CatFactManager> _logger;

        public CatFactManager(ILoggerFactory loggerFactory)
        {
            _client = new HttpClient();
            _logger = loggerFactory.CreateLogger<CatFactManager>();
           
        }

        public async Task<CatFact> GetCatFactAsync()
        {

            CatFact fact = new CatFact(string.Empty, 0); 

            try
            {
                var response = await _client.GetAsync("https://catfact.ninja/fact");
                response.EnsureSuccessStatusCode();
                fact = await response.Content.ReadAsAsync<CatFact>();
            }
            catch(Exception ex)
            {
                
                _logger.LogError("GetCatFactAsync: " + ex.Message);
               
            }



            return fact;
        }

        public async Task SaveCatFactAcync(CatFact catFact, string filePath)
        {
            if (string.IsNullOrEmpty(catFact.Fact) || !filePath.EndsWith(".txt"))
                return;
            
            using (StreamWriter writer = File.AppendText(filePath))
            {
                try
                {
                    await writer.WriteLineAsync(catFact.Fact);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                
            }

        }

       
    }
}
