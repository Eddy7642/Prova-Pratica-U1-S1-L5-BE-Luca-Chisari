using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calcolo_Tasse
{
    using System;
    using System.Globalization;

    // Definizione della classe Contribuente
    public class Contribuente
    {
        // Proprietà della classe Contribuente
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public DateTime DataNascita { get; set; }
        public string CodiceFiscale { get; set; }
        public char Sesso { get; set; }
        public string ComuneResidenza { get; set; }
        public double RedditoAnnuale { get; set; }

        // Costruttore della classe Contribuente
        public Contribuente(string nome, string cognome, DateTime dataNascita, string codiceFiscale, char sesso, string comuneResidenza, double redditoAnnuale)
        {
            Nome = nome;
            Cognome = cognome;
            DataNascita = dataNascita;
            CodiceFiscale = codiceFiscale;
            Sesso = sesso;
            ComuneResidenza = comuneResidenza;
            RedditoAnnuale = redditoAnnuale;
        }

        // Metodo per calcolare l'imposta basata sul reddito annuale
        public double CalcolaImposta()
        {
            double imposta = 0.0;

            // Calcolo dell'imposta in base agli scaglioni di reddito
            if (RedditoAnnuale <= 15000)
            {
                imposta = RedditoAnnuale * 0.23; // 23% per redditi fino a 15.000
            }
            else if (RedditoAnnuale <= 28000)
            {
                imposta = 3450 + (RedditoAnnuale - 15000) * 0.27; // 27% sulla parte eccedente i 15.000 fino a 28.000
            }
            else if (RedditoAnnuale <= 55000)
            {
                imposta = 6960 + (RedditoAnnuale - 28000) * 0.38; // 38% sulla parte eccedente i 28.000 fino a 55.000
            }
            else if (RedditoAnnuale <= 75000)
            {
                imposta = 17220 + (RedditoAnnuale - 55000) * 0.41; // 41% sulla parte eccedente i 55.000 fino a 75.000
            }
            else
            {
                imposta = 25420 + (RedditoAnnuale - 75000) * 0.43; // 43% sulla parte eccedente i 75.000
            }

            return imposta; // Ritorna l'imposta calcolata
        }

        // Metodo ToString per visualizzare le informazioni del contribuente e l'imposta calcolata
        public override string ToString()
        {
            return $"Contribuente: {Nome} {Cognome},\n" +
                   $"nato il {DataNascita:dd/MM/yyyy} ({Sesso}),\n" +
                   $"residente in {ComuneResidenza},\n" +
                   $"codice fiscale: {CodiceFiscale}\n" +
                   $"Reddito dichiarato: {RedditoAnnuale.ToString("C2", CultureInfo.CreateSpecificCulture("it-IT"))}\n" +
                   $"IMPOSTA DA VERSARE: {CalcolaImposta().ToString("C2", CultureInfo.CreateSpecificCulture("it-IT"))}";
        }
    }

    // Classe principale del programma
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write("Inserisci il nome: ");
                string nome = Console.ReadLine() ?? throw new ArgumentNullException("nome", "Il nome non può essere null");
                Console.Write("Inserisci il cognome: ");
                string cognome = Console.ReadLine() ?? throw new ArgumentNullException("cognome", "Il cognome non può essere null");
                Console.Write("Inserisci la data di nascita (gg/mm/aaaa): ");
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, DateTimeStyles.None, out DateTime dataNascita))
                {
                    throw new ArgumentException("Formato data non valido");
                }
                Console.Write("Inserisci il codice fiscale: ");
                string codiceFiscale = Console.ReadLine() ?? throw new ArgumentNullException("codiceFiscale", "Il codice fiscale non può essere null");
                Console.Write("Inserisci il sesso (M/F): ");
                char sesso = char.Parse(Console.ReadLine()?.ToUpper() ?? throw new ArgumentNullException("sesso", "Il sesso non può essere null"));
                Console.Write("Inserisci il comune di residenza: ");
                string comuneResidenza = Console.ReadLine() ?? throw new ArgumentNullException("comuneResidenza", "Il comune di residenza non può essere null");
                Console.Write("Inserisci il reddito annuale: ");
                if (!double.TryParse(Console.ReadLine(), out double redditoAnnuale))
                {
                    throw new ArgumentException("Formato reddito non valido");
                }

                // Creazione di un oggetto Contribuente con i dati forniti
                Contribuente contribuente = new Contribuente(nome, cognome, dataNascita, codiceFiscale, sesso, comuneResidenza, redditoAnnuale);

                // Visualizzazione delle informazioni del contribuente e dell'imposta da versare
                Console.WriteLine("==================================================");
                Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE:");
                Console.WriteLine(contribuente.ToString());

                // Attendi l'input dell'utente prima di chiudere la console
                Console.WriteLine("Premi un tasto per chiudere...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                // Gestione degli errori e visualizzazione del messaggio di errore
                Console.WriteLine("Si è verificato un errore: " + ex.Message);
            }
        }
    }


}
