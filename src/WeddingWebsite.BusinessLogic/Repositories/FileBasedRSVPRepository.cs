using System.Collections.Generic;
using Newtonsoft.Json;
using WeddingWebsite.BusinessLogic.Models;

namespace WeddingWebsite.BusinessLogic.Repositories
{
    public class FileBasedRSVPRepository : IRSVPRepository
    {
        private readonly object mutex = new object();
        private readonly IRepositorySettings settings;

        private List<RSVP> items = new List<RSVP>();

        public FileBasedRSVPRepository(IRepositorySettingsProvider settingsProvider)
        {
            this.settings = settingsProvider.Get("RSVP");
            LoadRepository();
        }

        public bool TryStore(RSVP item)
        {
            lock (mutex)
            {
                items.Add(item);
            }

            return SaveRepository();
        }

        private void LoadRepository()
        {
            lock (mutex)
            {
                var sourceFolder = System.IO.Path.GetDirectoryName(this.settings.Source);
                if (!System.IO.Directory.Exists(sourceFolder))
                {
                    System.IO.Directory.CreateDirectory(sourceFolder);
                    return;
                }

                if (!System.IO.File.Exists(this.settings.Source))
                {
                    return;
                }

                var data = System.IO.File.ReadAllText(this.settings.Source);
                this.items = ParseRepositoryData(data);
            }
        }

        private bool SaveRepository()
        {
            try
            {
                lock (mutex)
                {
                    var sourceFolder = System.IO.Path.GetDirectoryName(this.settings.Source);
                    if (!System.IO.Directory.Exists(sourceFolder))
                    {
                        System.IO.Directory.CreateDirectory(sourceFolder);
                    }

                    var data = GetRepositoryData();
                    System.IO.File.WriteAllText(this.settings.Source, data);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private List<RSVP> ParseRepositoryData(string data)
        {
            return JsonConvert.DeserializeObject<List<RSVP>>(data);
        }

        private string GetRepositoryData()
        {
            lock (mutex)
            {
                return JsonConvert.SerializeObject(this.items);
            }
        }
    }
}
