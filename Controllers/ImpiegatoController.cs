using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System.Net;

namespace MvcMovie.Controllers
{
    public class ImpiegatoController : Controller
    {
        public IActionResult CreaForm()
        {
            Impiegato TempImpiegato = new Impiegato()
            {
                Id = 1,
                FullName = "",
                Gender = "",
                City = " ",
                EmailAddress = "",
                PersonalWebSite = "",
                Photo = "",
                AlternateText = ""

            };
            return View(TempImpiegato);
        }


        [HttpGet]
        public IActionResult CreaFormConFoto()
        {
            ImpiegatoConFile TempImpiegato = new ImpiegatoConFile()
            {
                Id = 1,
                FullName = "",
                Gender = "",
                City = " ",
                EmailAddress = "",
                PersonalWebSite = "",
                Photo = "",
                AlternateText = ""

            };
            return View(TempImpiegato);
        }

        public IActionResult CreaScheda(Impiegato DatiImpiegato)
        {
            string sNomeImpiegato = DatiImpiegato.FullName.Split(" ")[0];
            string sCognomeImpiegato = DatiImpiegato.FullName.Split(" ")[1];

            Impiegato VeroImpiegato = new Impiegato()
            {
                Id = DatiImpiegato.Id,
                FullName = DatiImpiegato.FullName,
                Gender = DatiImpiegato.Gender,
                City = DatiImpiegato.City,
                EmailAddress = sNomeImpiegato + "." + sCognomeImpiegato + "@azienda.it",
                PersonalWebSite = "www." + sNomeImpiegato + "." + sCognomeImpiegato + ".it",
                Photo = DatiImpiegato.Photo,
                AlternateText = "Foto non disponibile "

            };
            return View(VeroImpiegato);
        }


        [HttpPost] // impone che i dati siano passati da un form POST
        [ValidateAntiForgeryToken] // crea un token che crea un collegamento tra form e scheda
        public IActionResult CreaSchedaConFoto(ImpiegatoConFile DatiImpiegato)
        {

            //se il modello non è valido mi ritorna indietro alla view
            if (!ModelState.IsValid)
            {
                return View("CreaFormConFoto", DatiImpiegato);
            }
            
            
            //Da qui estraggo il file e me lo salvo su file system.
            //Agendo su Request
            
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/File");

            // qui creo la cartella se non esiste
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            //faccio un istanza a FileInfo per prendere le informazioni
            //del file e gli passo il file dal form

            FileInfo fileInfo = new FileInfo(DatiImpiegato.File.FileName);

            //qui gli do un nome diverso sovrascrivendo il nome.

            string fileName =  DatiImpiegato.FullName + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                DatiImpiegato.File.CopyTo(stream);
            }

            string sNomeImpiegato = DatiImpiegato.FullName.Split(" ")[0];
            string sCognomeImpiegato = DatiImpiegato.FullName.Split(" ")[1];

            ImpiegatoConFile VeroImpiegato = new ImpiegatoConFile()
            {
                Id = DatiImpiegato.Id,
                FullName = DatiImpiegato.FullName,
                Gender = DatiImpiegato.Gender,
                City = DatiImpiegato.City,
                EmailAddress = sNomeImpiegato + "." + sCognomeImpiegato + "@azienda.it",
                PersonalWebSite = "www." + sNomeImpiegato + "." + sCognomeImpiegato + ".it",
                Photo = "/File/" + fileName,
                AlternateText = "Foto non disponibile "

            };
            return View(VeroImpiegato);
        }


        public IActionResult CreaFormPut() 
        {

            //Recuperiamo i dati sul dirigente
            //Facciamo una preparazione della richiesta put al server

            var request = (HttpWebRequest)WebRequest.Create("https://localhost:7095/Impiegato/InserisciDirigente");
            var postData = "NomeDir=" + Uri.EscapeDataString("Mario");
            postData += "&CognomeDir=" + Uri.EscapeDataString("Rossi");
            var data = System.Text.Encoding.ASCII.GetBytes(postData);

            request.Method = "PUT";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            //Invio dei dati al server
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            //Prendiamo la risposta
            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //Processiamo la risposta del server
            //e creiamo la view conseguentemente.

            EsitoPerazionePut Esito = new EsitoPerazionePut();

            Esito.Esito = responseString;

            return View(Esito);
        }

        [HttpPut]
        public string InserisciDirigente()
        {
            string metod = Request.Method;        

            if (metod == "PUT")
                return "Dirigente inserito correttamente ";
            else
                return "Errore inserimentodirigente";
        }


    }
}
