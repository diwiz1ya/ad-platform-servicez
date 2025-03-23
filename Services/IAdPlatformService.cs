using System.Collections.Generic;

namespace AdPlatformServiceApp
{
    public interface IAdPlatformService
    {
        /// <summary>
        /// Загружает данные рекламных площадок из текстового файла.
        /// </summary>
        /// <param name="fileContent">Содержимое файла</param>
        void LoadFromFile(string fileContent);

        /// <summary>
        /// Возвращает список рекламных площадок для заданной локации.
        /// </summary>
        /// <param name="location">Запрашиваемая локация</param>
        /// <returns>Список названий рекламных площадок</returns>
        List<string> GetPlatformsForLocation(string location);
    }
}
