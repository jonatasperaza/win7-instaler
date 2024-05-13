using System;
using ConsoleTables;
using System.Net;
using System.Diagnostics;

namespace Win7Installer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao instalador estilo Ninite!");
            Console.WriteLine("Selecione os programas que deseja instalar:");

            var programas = new string[] { "Google Chrome", "Mozilla Firefox", "Visual Studio Code", "Notepad++", "WinRAR", "VLC Media Player" };

            var table = new ConsoleTable("Número", "Programa");
            for (int i = 0; i < programas.Length; i++)
            {
                table.AddRow(i + 1, programas[i]);
            }

            table.Write(Format.Minimal);

            Console.WriteLine("\nSelecione os programas que deseja instalar (separados por vírgula) ou digite 'todos' para instalar todos:");
            string input = Console.ReadLine();

            if (input.ToLower() == "todos")
            {
                InstalarTodos(programas);
            }
            else
            {
                string[] selecao = input.Split(',');
                InstalarSelecao(programas, selecao);
            }

            Console.WriteLine("Instalação concluída! Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }

        static void InstalarTodos(string[] programas)
        {
            foreach (var programa in programas)
            {
                Instalar(programa);
            }
        }

        static void InstalarSelecao(string[] programas, string[] selecao)
        {
            foreach (var item in selecao)
            {
                int index;
                if (int.TryParse(item.Trim(), out index) && index > 0 && index <= programas.Length)
                {
                    Instalar(programas[index - 1]);
                }
            }
        }

        static void Instalar(string programa)
        {
            string url;
            switch (programa.ToLower())
            {
                case "google chrome":
                    url = "https://dl.google.com//update2/installers/win_7/ChromeSetup.exe";
                    break;
                case "mozilla firefox":
                    url = "https://download.mozilla.org/?product=firefox-stub&os=win&lang=en-US";
                    break;
                case "visual studio code":
                    url = "https://aka.ms/win32-x64-user-stable";
                    break;
                case "notepad++":
                    url = "https://notepad-plus-plus.org/repository/8.x/8.3.1/npp.8.3.1.Installer.exe";
                    break;
                case "winrar":
                    url = "https://www.win-rar.com/fileadmin/winrar-versions/winrar/winrar-x64-602pt-br.exe";
                    break;
                case "vlc media player":
                    url = "https://www.videolan.org/vlc/download-win.html";
                    break;
                default:
                    Console.WriteLine($"O programa '{programa}' não é suportado.");
                    return;
            }

            string fileName = Path.GetFileName(url);
            string destinationPath = Path.Combine(Path.GetTempPath(), fileName);

            using (var webClient = new WebClient())
            {
                try
                {
                    Console.WriteLine($"Baixando {programa}...");
                    webClient.DownloadFile(url, destinationPath);
                    Console.WriteLine($"{programa} baixado com sucesso.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao baixar {programa}: {ex.Message}");
                    return;
                }
            }

            try
            {
                Console.WriteLine($"Instalando {programa}...");
                Process.Start(destinationPath);
                Console.WriteLine($"{programa} instalado com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao instalar {programa}: {ex.Message}");
            }
        }
    }
}
