using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfazCompilador
{
	public class Nodo
    {
		public string simbolo;
		public Nodo siguiente;
		public static string ambito;
		public char tipodato;

		public Nodo()
		{
			simbolo = "";
			ambito = "";
			siguiente = null;
		}

		public Nodo(string _ambito)
		{
			simbolo = "";
			ambito = _ambito;
			siguiente = null;
		}

		public char dimetipo(Nodo tipo)
		{
			if (tipo.simbolo == "int")
			{
				return 'i';
			}
			else if (tipo.simbolo == "float")
			{
				return 'f';
			}
			else if (tipo.simbolo == "char")
			{
				return 'c';
			}
			else if (tipo.simbolo == "double")
			{
				return 'd';
			}
			else if (tipo.simbolo == "void")
			{
				return 'v';
			}
			else
			{
				return 'v';
			}
		}
		public bool existe(List<object> tabsim, string _simbolo, char _tipodato, string _ambito)
		{
			bool bFound=false;
			foreach ( ElementoTabla element in tabsim)
			{
				if (_simbolo == element.id && _tipodato == element.tipo && _ambito == element.ambito)
				{
					bFound = true;
					break;
				}
			}
			return bFound;
		}
		public char buscartipo(List<object> tabsim, string _simbolo)
		{
			string _tipodato = "";

			foreach (ElementoTabla element in tabsim)
			{
				if (_simbolo == element.id)
				{
					_tipodato += element.tipo;
					break;
				}
			}
			return _tipodato[0];
		}

		public char buscartipo2(List<object> tabsim, string _simbolo, string _ambito)
		{
			string _tipodato = "";

			foreach (ElementoTabla element in tabsim)
			{
				if (_simbolo == element.id && _ambito == element.ambito)
				{
					_tipodato += element.tipo;
					break;
				}
			}
			return _tipodato[0];
		}

		public virtual void validatipos(List<object> tabsim, List<string> errores)
		{
			if (siguiente != null) siguiente.validatipos(tabsim, errores);
		}
	}

	public class NoTerminal
	{
		public int valor;
		public Nodo nodo;
		public NoTerminal(int x)
		{
			valor = x;
		}
		public NoTerminal() { }
	}
	public class Estado
	{
		public int valor;
		public Nodo nodo;
		public Estado(int e)
		{
			valor = e;
		}
	}
	public class Terminal : Nodo
	{
		public int valor;
		public Terminal(int x)
		{
			valor = x;
		}
		public Terminal()
		{
		}
	}
	public class Id : Nodo
	{
		public string clase;
		public Id(string _simbolo)
		{
			simbolo = _simbolo;
			clase = "id";
		}

		//public override void validatipos(List<object> tabsim, string cadena, List<string> errores)
		//{
			
		//}
	}

	public class Tipo : Nodo
	{
		public Tipo(string _simbolo)
		{

			simbolo = _simbolo;
		}

		public override void validatipos(List<object> tabsim, List<string> errores)
		{
			if (siguiente != null) siguiente.validatipos(tabsim, errores);
		}
	}

	public class ElementoTabla
	{
		public string id;
		public char tipo;
		public string ambito;
		public string stpara;

		public ElementoTabla(string _id, char _tipo, string _ambito, string _stpara)
		{
			id = _id;
			tipo = _tipo;
			ambito = _ambito;
			stpara = _stpara;
		}
		public ElementoTabla(string _id, char _tipo, string _ambito)
		{
			id = _id;
			tipo = _tipo;
			ambito = _ambito;
			stpara = "";
		}
	}
}
