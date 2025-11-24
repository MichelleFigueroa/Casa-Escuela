namespace CasaEscuela.AppWebMVC.Models.Utils
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    public static class TelegramLogger
    {
        private static readonly string botToken = "8000118385:AAEUqLbElSHDlrBySNritqpg8Am-oa9GV0A";
        private static readonly string chatId = "6464175685";

        public static async Task SendLogAsync(string message)
        {
            using var client = new HttpClient();
            var url = $"https://api.telegram.org/bot{botToken}/sendMessage";

            var data = new
            {
                chat_id = chatId,
                text = message
            };

            var json = System.Text.Json.JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString);
        }
        public static async Task SendDocumentBytesAsync(byte[] fileBytes, string fileName, string caption)
        {
            using var client = new HttpClient();
            var url = $"https://api.telegram.org/bot{botToken}/sendDocument";

            using var form = new MultipartFormDataContent();
            form.Add(new StringContent(chatId), "chat_id");
            form.Add(new StringContent(caption), "caption");

            var fileContent = new ByteArrayContent(fileBytes);
            fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
            form.Add(fileContent, "document", fileName);

            var response = await client.PostAsync(url, form);
            var responseText = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseText);
        }
    }

}
