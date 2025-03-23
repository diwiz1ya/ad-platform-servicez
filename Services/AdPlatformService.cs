using System;
using System.Collections.Generic;
using System.Linq;

namespace AdPlatformServiceApp
{
    public class AdPlatformService : IAdPlatformService
    {
        private Dictionary<string, List<string>> _platforms = new Dictionary<string, List<string>>();
        private readonly object _lock = new object();

        // Конструктор с предзагруженными тестовыми данными для проверки работы поиска
        public AdPlatformService()
        {
            _platforms = new Dictionary<string, List<string>>
            {
                { "Яндекс.Директ", new List<string> { "/ru" } },
                { "Ревдинский рабочий", new List<string> { "/ru/svrd/revda", "/ru/svrd/pervik" } },
                { "Газета уральских москвичей", new List<string> { "/ru/msk", "/ru/permobl", "/ru/chelobl" } },
                { "Крутая реклама", new List<string> { "/ru/svrd" } }
            };
        }

        // Метод загрузки данных из текстового файла. Если будет вызван, предзагруженные данные заменятся.
        public void LoadFromFile(string fileContent)
        {
            var newPlatforms = new Dictionary<string, List<string>>();
            var lines = fileContent.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length != 2)
                    continue;

                var platform = parts[0].Trim();
                var locations = parts[1]
                    .Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(l => l.Trim())
                    .Where(l => !string.IsNullOrEmpty(l))
                    .ToList();

                if (!string.IsNullOrEmpty(platform) && locations.Any())
                {
                    newPlatforms[platform] = locations;
                }
            }
            lock (_lock)
            {
                _platforms = newPlatforms;
            }
        }

        // Метод поиска рекламных площадок для заданной локации.
        // Если любая из локаций площадки является префиксом запрашиваемой, площадка добавляется в результат.
        public List<string> GetPlatformsForLocation(string location)
        {
            var result = new HashSet<string>();
            lock (_lock)
            {
                foreach (var kvp in _platforms)
                {
                    foreach (var loc in kvp.Value)
                    {
                        if (location.StartsWith(loc, StringComparison.OrdinalIgnoreCase))
                        {
                            result.Add(kvp.Key);
                            break;
                        }
                    }
                }
            }
            return result.ToList();
        }
    }
}
