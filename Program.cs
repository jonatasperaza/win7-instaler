using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace Win7Installer
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> programas = new Dictionary<string, string>()
            {
                { "Google Chrome", "https://dl.google.com//update2/installers/win_7/ChromeSetup.exe" },
                { "Mozilla Firefox", "https://download.mozilla.org/?product=firefox-stub&os=win&lang=en-US" }
                // Adicione mais programas e suas URLs de download conforme necessário
            };

            Console.WriteLine("Selecione os programas que deseja instalar:");

            foreach (var programa in programas.Keys)
            {
                Console.WriteLine($"[{programas.Keys.ToList().IndexOf(programa) + 1}] {programa}");
            }

            Console.WriteLine("[0] Instalar todos");
            Console.Write("Escolha uma opção: ");

            string input = Console.ReadLine();
            List<string> selectedPrograms = new List<string>();

            if (input == "0")
            {
                selectedPrograms.AddRange(programas.Keys);
            }
            else
            {
                int choice;
                if (int.TryParse(input, out choice) && choice >= 1 && choice <= programas.Count)
                {
                    selectedPrograms.Add(programas.Keys.ElementAt(choice - 1));
                }
                else
                {
                    Console.WriteLine("Opção inválida!");
                    return;
                }
            }

            foreach (string program in selectedPrograms)
            {
                if (programas.ContainsKey(program))
                {
                    string url = programas[program];
                    string fileName = url.Substring(url.LastIndexOf('/') + 1);
                    string destinationPath = AppDomain.CurrentDomain.BaseDirectory + fileName;

                    WebClient webClient = new WebClient();
                    Console.WriteLine($"Baixando {program}...");
                    webClient.DownloadFile(url, destinationPath);
                    Console.WriteLine($"{program} baixado com sucesso.");

                    Console.WriteLine($"Instalando {program}...");
                    Process.Start(destinationPath);
                }
            }
        }
    }
}
