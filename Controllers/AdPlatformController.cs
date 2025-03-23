using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AdPlatformServiceApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdPlatformController : ControllerBase
    {
        private readonly IAdPlatformService _service;

        public AdPlatformController(IAdPlatformService service)
        {
            _service = service;
        }

        /// <summary>
        /// Метод загрузки рекламных площадок из файла.
        /// Принимает файл в формате multipart/form-data и полностью перезаписывает данные.
        /// </summary>
        /// <param name="file">Файл с данными рекламных площадок</param>
        /// <returns>Сообщение о результате операции</returns>
        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не выбран или пустой.");

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var content = reader.ReadToEnd();
                _service.LoadFromFile(content);
            }
            return Ok("Данные успешно загружены.");
        }

        /// <summary>
        /// Метод поиска рекламных площадок для заданной локации.
        /// В запросе указывается параметр location.
        /// </summary>
        /// <param name="location">Искомая локация (например, /ru/msk)</param>
        /// <returns>Список названий подходящих рекламных площадок</returns>
        [HttpGet("search")]
        public IActionResult Search([FromQuery] string location)
        {
            if (string.IsNullOrEmpty(location))
                return BadRequest("Параметр location обязателен.");

            var platforms = _service.GetPlatformsForLocation(location);
            return Ok(platforms);
        }
    }
}
