using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

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

        public IActionResult CreaSchedaConFoto(ImpiegatoConFile DatiImpiegato)
        {
            //Da qui estraggo il file e me lo salvo su file system.
            //Agendo su Request
            
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/File");

            // qui creo la cartella se non esiste
            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

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
    }
}
