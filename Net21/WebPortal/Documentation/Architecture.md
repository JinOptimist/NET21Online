## Архитектура проекта WebPortal

Этот документ описывает ключевые элементы архитектуры, слои и принятые подходы в проекте `WebPortal`.

### Обзор
- **Тип приложения**: ASP.NET Core MVC (NET 8)
- **Слои**:
  - Web (Controllers, Views, Hubs, Middleware)
  - Services (бизнес-правила, разрешения/permissions, файловые сервисы, аутентификация)
  - Data (DbContext, Repositories, Models, EF Core Configurations, Migrations)
  - Localization (ресурсы .resx и сгенерированные Designer.cs)
  - Static/Web assets (`wwwroot`)

### Запуск и конвейер HTTP
- Точка входа: `Program.cs`
  - Регистрация MVC: `AddControllersWithViews()`
  - Логирование HTTP: `AddHttpLogging()` и `UseHttpLogging()`
  - Аутентификация (2 cookie-схемы): `AuthNotesController.AUTH_KEY` и `AuthController.AUTH_KEY`
  - БД:
    - `WebPortalContext` (SQL Server) — основная доменная БД
    - `NotesDbContext` (PostgreSQL) — подсистема заметок
  - DI: ручная регистрация репозиториев и сервисов + авто-регистрация через `AutoRegisterService`
  - Миграции/инициализация данных: `SeedService.Seed()` при старте
  - Локализация: настраиваемые middleware — `CustomNotesLocalizationMiddleware` или `CustomLocalizationMiddleware` в зависимости от `Localization:Mode`
  - Endpoints SignalR: несколько `Hub` маршрутов
  - Маршрутизация MVC: `{controller=Home}/{action=Index}/{id?}`

### Слой Web
- **Controllers** (`Controllers/`): функциональные области (Notes, Cdek, CoffeShop, CompShop, Marketplace, Motorcycles, Tourism и др.). Контроллеры тонкие: маппинг запросов/ответов, валидация, делегирование в сервисы/репозитории.
- **CustomAuthorizeAttributes**: роли и доменные ограничения доступа (например, `RoleAttribute`, `RoleNotesAttribute`, `NoBadWordAttribute`).
- **Middleware** (`CustomMiddleware/`): переключаемая локализация (`CustomLocalizationMiddleware`, `CustomNotesLocalizationMiddleware`).
- **Views** (`Views/`): Razor `.cshtml`, соответствуют экшенам контроллеров.
- **SignalR Hubs** (`Hubs/`): реалтайм взаимодействие (чат, уведомления, новости космоса, туризм и т.д.). Интерфейсы клиентов лежат в `Hubs/Interfaces`.

### Сервисный слой
- **Services/**: инкапсуляция бизнес-логики, политики доступа (permissions), работа с файлами, экспортом и аутентификацией.
  - Аутентификация: `AuthService`, `AuthNotesService` (+ интерфейсы)
  - Файлы: `FileService`, `CoffeShopFileServices`, `CompShopFileService`, `CdekFileService`, `TourismFilesService`
  - Разрешения: `Services/Permissions/*` (например, `GirlPermission`, `SpaceNewsPermission`, `ITourPermission` и т.д.)
  - Прочее: `ExportService`, `PasswordService`, `SuperService`
- **AutoRegistrationInDI**: `AutoRegisterService` — автоматическая регистрация по соглашениям/атрибутам дополняет ручной DI.

### Данные и доступ к данным
- **DbContexts**
  - `WebPortalContext` (SQL Server): основные доменные сущности (пользователи, контентные модели, маркетплейс, комментарии, уведомления, CDEK и др.) с явно настроенными связями в `OnModelCreating` и загрузкой конфигураций `ApplyConfigurationsFromAssembly`.
  - `NotesDbContext` (PostgreSQL): сущности домена заметок (заметки, категории, теги, пользователи, уведомления) и их связи.
- **Repositories** (`DbStuff/Repositories/`): паттерн репозитория поверх EF Core, разделён по подсистемам (Notes, Marketplace, CompShop и др.), интерфейсы в `DbStuff/Repositories/Interfaces`.
- **Models**
  - POCO сущности доменов в `DbStuff/Models/*` (в том числе подкаталоги: CoffeShop, CompShop, Marketplace, Motorcycles, Tourism, Notifications и др.)
  - ViewModels/UI-модели в `Models/*` для маппинга между доменом и представлениями
- **Migrations** (`Migrations/`): миграции для БД, поддерево также для `NotesDb`.

### Локализация
- Ресурсы `.resx` в `Localizations/` с автогенерацией `*.Designer.cs` через настройки в `WebPortal.csproj`.
- Поддержка нескольких языков, в т.ч. раздельные ресурсы для подсистем.

### Конфигурация
- `appsettings.json` + профильные файлы (`appsettings.Development.json`, `appsettings.CdekProject.json`, `appsettings.NotesProject.json`).
- Переключение окружения и строк подключения (SQL Server / PostgreSQL). Для режима CDEK считывается `CdekDbConnection`.

### Безопасность и доступ
- Две независимые cookie-схемы аутентификации (общая и для Notes).
- Политики доступа на уровне атрибутов (роли) и сервисов разрешений.
- Кастомная валидация и фильтры (например, `NoBadWord`).

### Реалтайм
- SignalR хабы для уведомлений и чатов (`CdekChatHub`, `NotificationHub`, `SupportChatHub`, `SpaceNewsHub`, `TourNotificationHub`, и др.).

### Хранилище статических файлов
- `wwwroot/` — изображения, css/js, документы, загрузки пользователей.

### Поток запроса (пример)
1) Запрос -> ASP.NET Core pipeline
2) Middleware локализации
3) Аутентификация и авторизация
4) Маршрутизация -> Controller/Action
5) Валидация модели, вызовы Service/Permission
6) Доступ к данным через Repository -> EF Core (`DbContext`)
7) Маппинг доменных сущностей в ViewModel
8) Рендер Razor View / возврат JSON / взаимодействие через SignalR

### Принципы и соглашения
- Разделение подсистем по папкам и пространствам имён
- DI-ориентированная композиция зависимостей
- EF Core для ORM; чёткая настройка связей в `OnModelCreating`
- Тонкие контроллеры, логика — в сервисах/permissions
- Многобазовый подход: разные `DbContext` под разные домены (SQL Server и PostgreSQL)

### Как расширять
- Добавить сущность: модель в `DbStuff/Models`, конфигурацию (при необходимости) в `DbStuff/Configs`, DbSet в соответствующий `DbContext`, миграцию в `Migrations`, репозиторий и его интерфейс, регистрацию в DI.
- Добавить фичу Web: контроллер/хаб, ViewModels в `Models`, представления в `Views`, локализацию в `Localizations`.


