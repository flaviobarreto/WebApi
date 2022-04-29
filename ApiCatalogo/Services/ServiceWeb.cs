using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Services
{
    public class ServiceWeb : IServiceWeb
    {
        public string Saudacao (string nome)
        {
            return $"Bem-vindo, {nome} \n\n {DateTime.Now}";
        }
    }
}
