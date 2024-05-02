using DMSMVC.Models.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text.Json;

namespace DMSMVC.Context
{
    public class ChatFileContext
    {
        private readonly IWebHostEnvironment _environment;
        public static List<ChatContent> chatContents = new List<ChatContent>();

        private string filepath;
        public ChatFileContext(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public void WriteChatContentToFile()
        {
            filepath = Path.Combine(_environment.ContentRootPath, "ChatContentFile");
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }
            using(var streamWriter = new StreamWriter(filepath, true))
            {
                foreach(var chatcontent in chatContents)
                {
                    streamWriter.WriteLine(JsonSerializer.Serialize(chatcontent));
                }
            }
        }

        public void LoadAllChatContent()
        {
            try
            {
                var fileExist = File.Exists(filepath);
                if (!fileExist) //Here file doesnt exist which means a new file will be created
                {
                    File.Create(filepath);
                }
                var chatFiles = File.ReadAllLines(filepath);
                foreach (var file in chatFiles)
                {
                    chatContents.Add(JsonSerializer.Deserialize<ChatContent>(file));
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
