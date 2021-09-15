using System;
using System.IO;


namespace MyClasses
{
    //Classe pública porque ela será referenciada nos testes
    public class FileProcess
    {
        //Método simples que vai retornar um boolean e vai verificar se um arquivo existe
        public bool FileExists(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentNullException("fileName");

            }
            return File.Exists(fileName);
        }

    }

  

}



