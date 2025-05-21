//esercizio 1
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;

List <string> letturaFile(string filepath)
{
    string[] lines = File.ReadAllLines(filepath);
    List<string> fileIntero = new List<string>();
    foreach (string line in lines)
    {
        fileIntero.Add(line);
    }
    fileIntero.RemoveAt(0);
    return fileIntero;
}
//esercizi 2-3
List <string> FiltraPerConsumo(List<string> fileIntero, int soglia)
{
    List<string> comuni = new List<string>();
    List<string> cognomi = new List<string>();
    List <string> consumiFiltrati = new List<string>();
    List <int> consumo = new List<int>(fileIntero.Count);
    string[] colonne= new string [fileIntero.Count];
    foreach (string line in fileIntero)
    {
        colonne = line.Split(';');
        int k = int.Parse(colonne[4]);
        consumo.Add(k);
        cognomi.Add(colonne[1]);
        comuni.Add(colonne[2]);
    }
    for (int i = 0; i < consumo.Count; i++)
    {
        if (consumo[i] < soglia)
        {
            consumiFiltrati.Add(cognomi[i]);
            Console.WriteLine(cognomi[i] + " - " + comuni[i] + "(" + consumo[i] + ")" );
        }
    }
    return consumiFiltrati;

}
//esercizio 4
void CalcolaStatistiche(List<string> fileIntero)
{
    List<string> cognomi = new List<string>();
    List<int> consumo = new List<int>(fileIntero.Count);
    
    string[] colonne = new string[fileIntero.Count];
    foreach (string line in fileIntero)
    {
        colonne = line.Split(';');
        int k = int.Parse(colonne[4]);
        consumo.Add(k);
        cognomi.Add(colonne[1]);
    }
    int somma = 0;
    foreach(int  i in consumo)
    {
        somma+= i;
    }
    float media= somma/ consumo.Count;
    Console.WriteLine("la media è " +  media);
    string famigliaMassima = "";
    foreach (int componenti in colonne[3])
    {
        int max = 0, indice=-1;
        indice++;
        if (componenti >= max)
        {
            max = componenti;
            famigliaMassima=cognomi[indice];
        }
    }
    Console.WriteLine("la famiglia con piu componenti è: " + famigliaMassima);
    Console.WriteLine("il totale di energia consumata è: " + somma);
    int famiglieMinori250 = 0;
    for (int i = 0; i < consumo.Count; i++)
    {
        if (consumo[i] < 250)
        {
            famiglieMinori250++;
        }
    }
    Console.WriteLine("le famiglie con un consumo minore a 250 kwh sono " + famiglieMinori250);
}
//esercizio 5
List <string> CercaPerComune(List<string> fileIntero, string comune)
{
    List<string> comuni = new List<string>();
    List <string> famigliePerComune = new List<string>();
    List<string> cognomi = new List<string>();
    string[] colonne = new string[fileIntero.Count];
    foreach (string line in fileIntero)
    {
        colonne = line.Split(';');
        int k = int.Parse(colonne[4]);
        cognomi.Add(colonne[1]);
        comuni.Add(colonne[2]);
    }
    foreach (string line in comuni)
    {
        if (line.Contains(comune))
        {
            famigliePerComune.Add(colonne[1]);
        }
    }
    return famigliePerComune;
}
//esercizio 6
void TrovaMinimiConsumo(List<string> fileIntero)
{
    int oro=10000000, argento=10000000, bronzo=100000000;
    string famiglia_oro = "" ,famiglia_argento="", famiglia_bronzo="";
    List<string> cognomi = new List<string>();
    List<int> consumo = new List<int>(fileIntero.Count);
    string[] colonne = new string[fileIntero.Count];
    foreach (string line in fileIntero)
    {
        colonne = line.Split(';');
        int k = int.Parse(colonne[4]);
        consumo.Add(k);
        cognomi.Add(colonne[1]);
    }
    for(int i = 0; i < consumo.Count; i++)
    {
        int appoggio = 0;
        if (consumo[i] < bronzo && consumo[i] < argento && consumo[i] < oro)
        {
            oro= consumo[i];
            famiglia_oro= cognomi[i];
            
        }
        else if(consumo[i] < bronzo && consumo[i] < argento)
        {
            argento = consumo[i];
            famiglia_argento = cognomi[i]; 
            if (argento > oro)
            {
                appoggio = argento;
                oro=argento;
                argento =oro;
            }

        }
        else if(consumo[i] < bronzo)
        {
            bronzo=consumo[i];
            famiglia_bronzo= cognomi[i];
        }

    }
    Console.WriteLine("1) " + famiglia_oro + " - " + "( " + oro + " )");
    Console.WriteLine("2) " + famiglia_argento + " - " + "( " + argento + " )");
    Console.WriteLine("3) " + famiglia_bronzo + " - " + "( " + bronzo + " )");


}
string filepath = "consumi.csv";
List<string> fileIntero = letturaFile(filepath);
foreach( string line in fileIntero)
{
    Console.WriteLine(line);
}
List<string> cognomiFiltrati = FiltraPerConsumo(fileIntero, 300);
CalcolaStatistiche(fileIntero);
string comune = "Milano";
List<string> famigliePerComune = CercaPerComune(fileIntero, comune);
StreamWriter streamWriter = new StreamWriter("Famiglie"+comune+".txt");

foreach (string line in famigliePerComune)
{
    streamWriter.WriteLine(line);
}
TrovaMinimiConsumo(fileIntero);