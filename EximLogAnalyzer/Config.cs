using System.IO;
using System.Xml.Serialization;

namespace EximLogAnalyzer
{
    public class Config
    {
        public string Hostname;

        public string Login;

        public string Password;

        public string LogPath;

        public Config()
        {
            LogPath = "/var/log/exim4";
        }

        public static Config LoadConfig(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
            if (File.Exists(fileName))
            {
                using (FileStream fs = File.OpenRead(fileName))
                    return (Config)xmlSerializer.Deserialize(fs);
            }
            else
            {
                throw new FileNotFoundException();
            }
        }

        public static void SaveConfig(Config config, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
            xmlSerializer.Serialize(File.Create(fileName), config);
        }

        public static void Create(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Config));
            Config config = new Config();
            xmlSerializer.Serialize(File.Create(fileName), config);
        }
    }
}
