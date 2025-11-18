using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C4_ENTIDAD
{
    public class Domicilio
    {
        private int _idDomicilio;
        private int _numero;
        private string _barrio;
        private string _piso;
        private string _ciudad;
        private string _provincia;

        public int IdDomicilio { get => _idDomicilio; set => _idDomicilio = value; }
        public int Numero { get => _numero; set => _numero = value; }
        public string Barrio { get => _barrio; set => _barrio = value; }
        public string Piso { get => _piso; set => _piso = value; }
        public string Ciudad { get => _ciudad; set => _ciudad = value; }
        public string Provincia { get => _provincia; set => _provincia = value; }

        public Domicilio()
        {
        }

        public Domicilio(int idDomicilio, int numero, string barrio, string piso, string ciudad, string provincia)
        {
            _idDomicilio = idDomicilio;
            _numero = numero;
            _barrio = barrio;
            _piso = piso;
            _ciudad = ciudad;
            _provincia = provincia;
        }
    }
}
