using System.Speech.Synthesis;

string[] listOfFrases = new string[11];

listOfFrases[0] = "Hi, Alex! How is the API for taxes going?";
listOfFrases[1] = "Hello, Mr. Smith! It’s almost done. I just need to test it.";
listOfFrases[2] = "That’s great! Can you explain how it works?";
listOfFrases[3] = "Sure! It takes user income as input and calculates the tax amount.";
listOfFrases[4] = "Does it handle errors, like wrong inputs?";
listOfFrases[5] = "Yes, I added validation for all inputs.";
listOfFrases[6] = "Excellent! When will it be ready for deployment?";
listOfFrases[7] = "I’ll finish testing by tomorrow and deploy it.";
listOfFrases[8] = "Good job! Oh, by the way, have you read that Naruto chapter?";
listOfFrases[9] = "Yes! It was amazing. The fight scene was so cool.";
listOfFrases[10] = "Yes, and it was so emotional. Naruto never gives up!";

string combinedText = string.Join(" ", listOfFrases);


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

        synthesizer.SetOutputToWaveFile("FullText.wav");
        synthesizer.Speak(combinedText);

        for (int i = 0; i < listOfFrases.Length; i++)
        {
            string nameAudio = listOfFrases[i].Substring(0, (listOfFrases[i].Length - 1));
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

