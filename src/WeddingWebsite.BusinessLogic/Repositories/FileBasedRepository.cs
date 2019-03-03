using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace WeddingWebsite.BusinessLogic.Repositories
{
    public abstract class FileBasedRepository<T> : IRepository<T>
    {
        private readonly object mutex = new object();
        private readonly IRepositorySettings settings;

        private List<T> items = new List<T>();

        protected FileBasedRepository(IRepositorySettingsProvider settingsProvider)
        {
            this.settings = settingsProvider.Get(typeof(T).Name);
            LoadRepository();
        }

        public bool TryStore(T item)
        {
            lock (mutex)
            {
                items.Add(item);
            }

            return SaveRepository();
        }

        public IReadOnlyList<T> All()
        {
            lock (mutex)
            {
                return items;
            }
        }

        public T Find(Func<T, bool> predicate)
        {
            lock (mutex)
            {
                return items.FirstOrDefault(predicate);
            }
        }

        public void Replace(T oldItem, T newItem)
        {
            lock (mutex)
            {
                var index = this.items.IndexOf(oldItem);
                this.items[index] = newItem;
                SaveRepository();
            }
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

        private List<T> ParseRepositoryData(string data)
        {
            return JsonConvert.DeserializeObject<List<T>>(data);
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