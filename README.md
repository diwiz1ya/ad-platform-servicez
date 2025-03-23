# Ad‑Platform‑Service

Простой веб‑сервис на C# для подбора рекламных площадок по заданной локации.

## 📂 Название репозитория

**ad-platform-service**

## 📝 Описание проекта

Сервис хранит список рекламных площадок в памяти и позволяет быстро искать те, которые работают в заданной локации.

- **POST** `/api/adplatform/upload` — загрузка/обновление данных из текстового файла (скрыт в Swagger).
- **GET** `/api/adplatform/search?location={location}` — поиск площадок по локации.

## ⚙️ Требования

- .NET 9 SDK
- Windows / macOS / Linux

## 🚀 Быстрый старт

### Клонирование репозитория

```bash
git clone https://github.com/diwiz1ya/ad-platform-service.git
cd ad-platform-service
```

### Сборка и запуск

```bash
dotnet build
dotnet run
```

Сервис доступен по адресу: http://localhost:5220

## 🔍 Тестирование через Swagger

1. Откройте http://localhost:5220/swagger
2. Выберите GET `/api/adplatform/search`
3. Нажмите "Try it out", введите `/ru/msk` и нажмите "Execute"

## 📂 Формат файла platforms.txt

```
Яндекс.Директ:/ru
Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik
Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl
Крутая реклама:/ru/svrd
```

## ⚙️ Загрузка файла (Postman)

1. POST http://localhost:5220/api/adplatform/upload
2. Body → form-data → Key: `file` (тип File)
3. Select File → выберите `platforms.txt`
4. Send → ответ `200 OK`

## ✅ Проверка поиска после загрузки

GET `/api/adplatform/search?location=/ru/svrd/revda`


