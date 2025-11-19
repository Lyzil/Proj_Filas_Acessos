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
        private Ambiente ambiente;
        private bool tipoAcesso = false; //true=Autorizado, false=Negado
        public DateTime DtAcesso { get => dtAcesso; set => dtAcesso = value; }
        public bool TipoAcesso { get => tipoAcesso; set => tipoAcesso = value; }
        internal Usuario Usuario { get => usuario; set => usuario = value; }
        internal Ambiente Ambiente { get => ambiente; set => ambiente = value; }

        public Log(DateTime dtAcesso, Usuario usuario, Ambiente ambiente, bool tipoAcesso)
        {
            DtAcesso = dtAcesso;
            Usuario = usuario;
            Ambiente = ambiente;
            TipoAcesso = tipoAcesso;
        }
    }
}
