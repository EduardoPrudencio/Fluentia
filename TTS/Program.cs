using System;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using Microsoft.Extensions.Configuration;

namespace SpeechSynthesisExample
{
    public class ProgramConfig
    {
        public string Path { get; set; }
        public string File { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                ProgramConfig config = LoadConfig();

                if (config == null)
                {
                    Console.WriteLine("Configuração não encontrada ou inválida.");
                    return;
                }

                string[] lines = LoadAndProcessLines(config);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Nenhuma linha válida foi encontrada no arquivo.");
                    return;
                }

                SynthesizeAudio(lines, config);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocorreu um erro: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("Pressione ENTER para sair...");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Lê o arquivo config.json na pasta atual e mapeia para ProgramConfig.
        /// </summary>
        private static ProgramConfig LoadConfig()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string configFile = Path.Combine(currentDirectory, "config.json");

            if (!File.Exists(configFile))
                return null;

            var builder = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddJsonFile("config.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = builder.Build();

            ProgramConfig config = configuration.Get<ProgramConfig>();
            return config;
        }

        /// <summary>
        /// Carrega o arquivo indicado no config, seleciona apenas as linhas ímpares,
        /// remove trechos indesejados e retorna o array de strings "limpas".
        /// </summary>
        private static string[] LoadAndProcessLines(ProgramConfig config)
        {
            string fullPath = Path.Combine(config.Path, config.File);
            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"Arquivo não encontrado em: {fullPath}");

            string[] allLines = File.ReadAllLines(fullPath)
                                    .Where(line => !string.IsNullOrWhiteSpace(line))
                                    .ToArray();

            var oddLines = allLines
                .Where((line, index) => (index + 1) % 2 != 0)
                .ToArray();

            for (int i = 0; i < oddLines.Length; i++)
            {
                oddLines[i] = CleanLine(oddLines[i]);
            }

            return oddLines;
        }

        /// <summary>
        /// Limpa uma linha removendo prefixos e caracteres específicos.
        /// Ajuste conforme sua necessidade.
        /// </summary>
        private static string CleanLine(string line)
        {
            return line
                .Replace("Gerente: ", "")
                .Replace("Programador: ", "")
                .Replace("?", "")
                .Replace(":", "")
                .TrimStart();
        }

        /// <summary>
        /// Gera o arquivo de áudio com o texto completo (FullText.wav) e depois
        /// gera arquivos de áudio individuais para cada linha, usando System.Speech.
        /// </summary>
        private static void SynthesizeAudio(string[] lines, ProgramConfig config)
        {
            string combinedText = string.Join(" ", lines);
            
            string basePath = Path.Combine(config.Path, Path.GetDirectoryName(config.File) ?? string.Empty);
            if (!Directory.Exists(basePath))
            {
                basePath = config.Path;
            }

            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    Console.WriteLine("Voice Name: " + info.Name);
                }

                synthesizer.SelectVoice("Microsoft Zira Desktop");

                string fullTextAudioPath = Path.Combine(basePath, "FullText.wav");
                synthesizer.SetOutputToWaveFile(fullTextAudioPath);
                synthesizer.Speak(combinedText);

                for (int i = 0; i < lines.Length; i++)
                {
                    string safeLine = lines[i].TrimEnd('.', ' ', '?', ':');
                    if (string.IsNullOrWhiteSpace(safeLine))
                    {
                        safeLine = $"Linha{i + 1}";
                    }

                    string nameAudio = $"{safeLine}.wav";

                    synthesizer.SetOutputToWaveFile(Path.Combine(basePath, nameAudio));
                    synthesizer.Speak(lines[i]);

                    Console.WriteLine($"Sintetizando e salvando {i + 1} de {lines.Length} áudio(s) - {nameAudio}");
                }
            }

            Console.WriteLine("Processo finalizado");
        }
    }
}
