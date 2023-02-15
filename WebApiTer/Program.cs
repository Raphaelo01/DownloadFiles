var builder = WebApplication.CreateBuilder(args);
#region Swagger services configuration
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion
var app = builder.Build();
#region Configure the HTTP request pipeline
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI();}
#endregion
app.UseHttpsRedirection();
app.MapGet("/DownloadFile", async () => {
    var path = @"ArchivoZip.ZIP";
    var resultfile = File.Exists(path);
    Thread.Sleep(5000);
    if (!System.IO.File.Exists(path))
    {
        return Results.NotFound("File no found");
    }
    var pathResult = $"ArchivoZip{DateTime.Now.Hour.ToString()}-{DateTime.Now.Minute.ToString()}.zip";
    var contentType = "application/octet-stream";
    var bytes = await System.IO.File.ReadAllBytesAsync(path);
    return Results.File(bytes, contentType, Path.GetFileName(pathResult));
});
app.Run();

