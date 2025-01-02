using System;
using System.Speech.Synthesis;

class Program
{
    static void Main(string[] args)
    {

        string[] listOfFrases = new string[11];

        listOfFrases[0] = "Alex! I need your help with the finance system.";
        listOfFrases[1] = "Good morning, Mr. Smith. What do you need?";
        listOfFrases[2] = "We need a new API to calculate taxes.";
        listOfFrases[3] = "Sure. What kind of taxes?";
        listOfFrases[4] = "Simple ones for now. Income and sales taxes.";
        listOfFrases[5] = "Got it. Do you need it by this week?";
        listOfFrases[6] = "Yes, please. By Friday.";
        listOfFrases[7] = "Okay, I will start working on it today.";
        listOfFrases[8] = "Thank you. By the way, did you watch Naruto yesterday?";
        listOfFrases[9] = "Yes! It was amazing. The fight scene was so cool.";
        listOfFrases[10] = "I agree. Naruto is the best!";

        try
		{
            // Cria o sintetizador de voz
            using (SpeechSynthesizer synthesizer = new SpeechSynthesizer())
            {
                // Lista as vozes instaladas
                foreach (InstalledVoice voice in synthesizer.GetInstalledVoices())
                {
                    VoiceInfo info = voice.VoiceInfo;
                    Console.WriteLine("Voice Name: " + info.Name);
                }

                synthesizer.SelectVoice("Microsoft Zira Desktop");

                for (int i = 0; i < listOfFrases.Length; i++)
                {
                    string nameAudio = listOfFrases[i].Substring(0, (listOfFrases[i].Length -1));
                    nameAudio = $"{nameAudio}.wav";

                    synthesizer.SetOutputToWaveFile(nameAudio);

                    synthesizer.Speak(listOfFrases[i]);
                }

                Console.WriteLine("Áudio sintetizado e salvo em 'meuAudio.wav'.");
                Console.ReadLine();
            }
        }
		catch (Exception ex)
		{
            Console.WriteLine(ex.Message);
		}
    }
}

