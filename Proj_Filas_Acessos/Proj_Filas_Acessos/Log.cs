using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_Filas_Acessos
{
    internal class Log
    {
        private DateTime dtAcesso;
        private Usuario usuario;
        private bool tipoAcesso = false; //true=Autorizado, false=Negado
        public DateTime DtAcesso { get => dtAcesso; set => dtAcesso = value; }
        public bool TipoAcesso { get => tipoAcesso; set => tipoAcesso = value; }
        internal Usuario Usuario { get => usuario; set => usuario = value; }

        public Log(DateTime dtAcesso, Usuario usuario, bool tipoAcesso)
        {
            DtAcesso = dtAcesso;
            Usuario = usuario;
            TipoAcesso = tipoAcesso;
        }
    }
}
