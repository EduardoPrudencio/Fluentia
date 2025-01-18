using System.Speech.Synthesis;

internal class Program
{
    private static void Main(string[] args)
    {

        string path = "";
        string[] lines = File.ReadAllLines(path).Where(line => !string.IsNullOrWhiteSpace(line))
         .ToArray();

        string[] oddLines = lines
          .Where((line, index) => (index + 1) % 2 != 0)
          .ToArray();

        string[] listOfFrases = new string[oddLines.Length];

        for (int i = 0; i < oddLines.Length; i++)
        {
            listOfFrases[i] = oddLines[i]
                .Replace("Gerente: ","")
                .Replace("Programador: ", "")
                .TrimStart();
        }


        string combinedText = string.Join(" ", listOfFrases);
        path = path.Replace("FullText.txt", "");

        try
        {
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    Console.WriteLine("Voice Name: " + info.Name);
                }

                synthesizer.SelectVoice("Microsoft Zira Desktop");

                synthesizer.SetOutputToWaveFile($"{path}FullText.wav");
                synthesizer.Speak(combinedText);

                for (int i = 0; i < listOfFrases.Length; i++)
                {
                    string nameAudio = listOfFrases[i].Substring(0, listOfFrases[i].Length - 1);
                    nameAudio = $"{nameAudio}.wav";

                    synthesizer.SetOutputToWaveFile($"{path}{nameAudio}");
                    synthesizer.Speak(listOfFrases[i]);
                }

                Console.WriteLine("Áudio sintetizado e salvo em 'meuAudio.wav'.");
                // Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}