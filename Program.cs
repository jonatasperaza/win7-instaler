using System;
using System.Net;
using System.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        string url = "https://code.visualstudio.com/sha/download?build=stable&os=win32-x64-user";
        string destinationPath = "arquivo.exe";

        // Baixar o arquivo da URL especificada
        WebClient webClient = new WebClient();
        Console.WriteLine("Baixando arquivo...");
        webClient.DownloadFile(url, destinationPath);
        Console.WriteLine("Arquivo baixado com sucesso.");

        // Executar o arquivo
        Console.WriteLine("Executando arquivo...");
        Process.Start(destinationPath);
    }
}
