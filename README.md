# AgentFrameworkBestPractices

Çok projeli bir demo çözüm: ASP.NET Core API + Microsoft.Extensions.AI ile ajan senaryoları, EF Core/SQLite ile ToDo örneği, ve farklı kullanım biçimlerini gösteren yardımcı projeler.

## Neyi gösteriyor?
- ASP.NET Core host: `AgentFrameworkBestPractices.API` (program başlatma, DI, CORS, denetleyiciler).
- AI ajan entegrasyonları: Chat, function-calling, plugin, workflow, MCP client, multi-conversation örnekleri (ilgili projeler: `AgentAsFunctionTool`, `FunctionCalling`, `Plugins`, `Workflows`, `McpClientAsFunctionTool`, `MultiConversation`, `RAG`).
- ToDo senaryosu: EF Core + SQLite, CRUD araçları AI tool olarak kullanılıyor.

Çözümü aç: `AgentFrameworkBestPractices.slnx`.

## Proje haritası
- `AgentFrameworkBestPractices.API`
  - `Program.cs` – host ve servis kaydı.
  - `Extensions/ServiceExtensions.cs` – tüm ajan/ToDo servislerini DI’a ekler.
  - `Controllers/ChatController.cs` – `/[controller]/[action]` rotası; `/Chat/Chat` `[HttpPost]`, diğerleri GET (istek türü için `[HttpPost]` ekleyebilirsiniz).
  - `Controllers/ToDoController.cs` – şimdilik boş; ToDo HTTP uçlarını buraya ekleyebilirsiniz.
- `AgentFrameworkBestPractices.Projects` (ToDo örneği)
  - `ToDoManagerApp/Data/AppDbContext.cs` – DbContext; `DbSet<ToDo>`.
  - `Migrations/` – EF migrasyonları (ilk migrasyon `ToDos` tablosunu oluşturur).
  - `ToDoManagerApp/Tools/ToDoManagerTool.cs` – CRUD fonksiyonları AI tool olarak.
  - `ToDoManagerApp/Services/ToDoService.cs` – bu araçlarla ajan yaratır.
  - `ToDoManagerApp/Extensions/ToDoManagerExtension.cs` – DbContext, araç ve servislerin DI kaydı.
- `AgentFrameworkBestPractices.Common` – ortak tipler ve uzantılar.
- Diğer ajan projeleri – API’de ChatController aksiyonları ve DI kayıtları üzerinden kullanılıyor.

## Çalıştırma
```
dotnet run --project AgentFrameworkBestPractices.API/AgentFrameworkBestPractices.API.csproj
```
Varsayılan URL: `AgentFrameworkBestPractices.API/Properties/launchSettings.json` (http://localhost:5169). Debug için startup projesi olarak API’yi seçin; class library projeleri tek başına çalışmaz.

## ToDo (AI tool + EF Core) akışı
- Bağlantı: `appsettings.json` → `ConnectionStrings:ToDoApp` → `../AgentFrameworkBestPractices.Projects/toDoApp.db`.
- Tasarım zamanı: `ToDoManagerApp/Data/AppDbContextFactory.cs` aynı yolu kullanır (CLI ve runtime senkron).
- Araçlar: `ToDoManagerApp/Tools/ToDoManagerTool.cs` (`AddNewToDo`, `ListToDo`, `UpdateToDo`, `RemoveToDo`) AI tool olarak kullanılır.
- Ajan: `ToDoManagerApp/Services/ToDoService.cs` bu araçlarla `IAgentService` üzerinden ajan kurar.
- DI: `ToDoManagerApp/Extensions/ToDoManagerExtension.cs`, API’den `ServiceExtensions.cs` ile çağrılır.

### Veritabanı kurulumu
`SQLite Error 1: 'no such table: ToDos'` görürseniz:
```
dotnet ef database update \
  --project AgentFrameworkBestPractices.Projects/AgentFrameworkBestPractices.Projects.csproj \
  --startup-project AgentFrameworkBestPractices.API/AgentFrameworkBestPractices.API.csproj \
  --context AppDbContext
```
Temiz başlamak için `AgentFrameworkBestPractices.Projects/toDoApp.db*` dosyalarını silip komutu yeniden çalıştırabilirsiniz.

## Genişletme
- ToDo HTTP uçları eklemek için `AgentFrameworkBestPractices.API/Controllers/ToDoController.cs` içinde `IToDoService` veya `ToDoManagerTool` çağıran aksiyonlar yazın.
- Yeni migrasyon eklerken aynı SQLite yolunu koruyun (`appsettings.json` ve `AppDbContextFactory.cs`).
- Chat uçlarında istek türlerini netleştirmek için `[HttpPost]` ekleyin; route kalıbı `/[controller]/[action]`.
