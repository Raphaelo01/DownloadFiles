using System.Net;



var generatePdfsRetrieveUrl = @"https://localhost:7281/DownloadFile"; 
var fileSave = $@"ArchivosDescargados/newZipFile{Guid.NewGuid()}.zip";

// Comente o descomente el metodo que desee usar
//Metodo 1
Console.WriteLine("Ejecutando metodo 1 y guardando en ArchivosDescargados del proyecto");
HttpClient client = new HttpClient();
var response = await client.GetAsync(generatePdfsRetrieveUrl);

response.EnsureSuccessStatusCode();

if (response.StatusCode != HttpStatusCode.OK)
    return;
var content = response.Content;
var contentStream = await content.ReadAsStreamAsync();

using (var fs = new FileStream( fileSave,
           FileMode.CreateNew))
{
    await response.Content.CopyToAsync(fs);

}
Console.WriteLine("Fin de la Ejecucion del Metodo 1");


//Metodo 2
/*
 Console.WriteLine("Inicia todo el proceso!");

string Myurl= @"https://localhost:7281/DownloadFile", MysaveAs= @"C:\Users\LGonzalez\Downloads\test\mufile";

Task.Run(() =>   Download( Myurl, MysaveAs)).Wait();


async Task Download(string url, string saveAs)
{
    var httpClient = new HttpClient();
    var response = await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Head, url));
    var parallelDownloadSuported = response.Headers.AcceptRanges.Contains("bytes");
    var contentLength = response.Content.Headers.ContentLength ?? 0;
    Console.WriteLine("Empieza a hacer la consulta");
    if (parallelDownloadSuported)
    {
        Console.WriteLine("Empieza el paralelismo");
        const double numberOfParts = 5.0;
        var tasks = new List<Task>();
        var partSize = (long)Math.Ceiling(contentLength / numberOfParts);

        File.Create(saveAs).Dispose();

        for (var i = 0; i < numberOfParts; i++)
        {
            var start = i * partSize + Math.Min(1, i);
            var end = Math.Min((i + 1) * partSize, contentLength);

            tasks.Add(
                Task.Run(() => DownloadPart(url, saveAs, start, end))
                );
        }
        Console.WriteLine("LLamamos la tarea");
        await Task.WhenAll(tasks);
    }
    Console.WriteLine("Finaliza todo el proceso");
}


async void DownloadPart(string url, string saveAs, long start, long end)
{
    using (var httpClient = new HttpClient())
    using (var fileStream = new FileStream(saveAs, FileMode.Open, FileAccess.Write, FileShare.Write))
    {
        var message = new HttpRequestMessage(HttpMethod.Get, url);
        message.Headers.Add("Range", string.Format("bytes={0}-{1}", start, end));

        fileStream.Position = start;
        await httpClient.SendAsync(message).Result.Content.CopyToAsync(fileStream);
        Console.WriteLine("Finaliza la descarga del sistema");
    }
}
 */