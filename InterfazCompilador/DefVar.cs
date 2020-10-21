using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfazCompilador
{
	public class DefVar : Nodo
	{
		Nodo tipo;
		Nodo id;
		Nodo ListaVar;

		public DefVar(Stack pila)//DefVar -> tipo id ListaVar ; 
		{
			pila.Pop();//quita estado
			pila.Pop(); //quita  ;
			pila.Pop(); //quita estado estado
			ListaVar = ((NoTerminal)pila.Pop()).nodo; //quita ListaVar
			pila.Pop(); //quita estado
			id = new Id(((Terminal)pila.Pop()).simbolo); //quita Id
			pila.Pop(); //quita estado
			tipo = new Tipo(((Nodo)pila.Pop()).simbolo); //quita tipo
		}

		public override void validatipos(List<object> tabsim, List<string> errores)
		{
			
		}
	}

	public class ListaVar : Nodo
	{
		Nodo id;
		Nodo lVar;
		public ListaVar(Stack pila)//ListaVar -> , id ListaVar
		{
			pila.Pop();//Quita Estado
			lVar = ((NoTerminal)pila.Pop()).nodo;//Quita ListaVar
			pila.Pop();//Quita Estado
			id = new Id(((Terminal)pila.Pop()).simbolo);//quita id
			pila.Pop();//Quita Estado
			pila.Pop();//Quita ,
		}
	}
	
	public class DefFunc : Nodo
	{
		Nodo tipo;
		Nodo id;
		Nodo Parametros;
		Nodo BloqFunc;
		public DefFunc(Stack pila)//DefFunc -> tipo id(Parametros ) BloqFunc
		{
			pila.Pop();//Quita Estado
			BloqFunc = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita )
			pila.Pop();//Quita Estado
			Parametros = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita (
			pila.Pop();//Quita Estado
			id = new Id(((Terminal)pila.Pop()).simbolo);
			pila.Pop();//Quita Estado
			tipo = new Tipo(((Terminal)pila.Pop()).simbolo);
		}

		public override void validatipos(List<object> tabsim, List<string> errores)
		{
			string cadenapa="";
			tipodato = dimetipo(tipo);
			ambito = id.simbolo;
			if (Parametros != null) Parametros.validatipos(tabsim, errores);
			if (!existe(tabsim, id.simbolo, tipodato, ambito))
			{
				tabsim.Add(new ElementoTabla(id.simbolo, tipodato, ambito,cadenapa));
			}
			else
				errores.Add("La funcion" +id.simbolo + " ya existe");
			cadenapa = "";
			if (BloqFunc != null) BloqFunc.validatipos(tabsim, errores);
			ambito = "" ;
			if (siguiente != null) siguiente.validatipos(tabsim, errores);
		}
	}

	public class Parametros : Nodo
	{
		Nodo tipo;
		Nodo id;
		Nodo listaParam;
		public Parametros(Stack pila)//Parametros -> tipo id ListaParam
		{
			pila.Pop();//Quita Estado
			listaParam = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			id = new Id(((Terminal)pila.Pop()).simbolo);
			pila.Pop();//Quita Estado
			tipo = new Tipo(((Terminal)pila.Pop()).simbolo);
		}

		public override void validatipos(List<object> tabsim,List<string> errores){
			string cadenapa = "";
			tipodato =dimetipo(tipo);
			if(!existe(tabsim, id.simbolo, tipodato, ambito)){
			tabsim.Add(new ElementoTabla(id.simbolo, tipodato, ambito) );
			}
			else
				errores.Add("La variable "+id.simbolo+" ya fue declarada");
			cadenapa+=tipo.simbolo[0];
			if(listaParam != null) listaParam.validatipos(tabsim, errores);
			if(siguiente!=null) siguiente.validatipos(tabsim, errores);
		}
	}
	
	public class ListaParam : Nodo
	{
		Nodo tipo;
		Nodo id; 
		Nodo listaParam;
		public ListaParam(Stack pila)//ListaParam -> , tipo id ListaParam
		{
			pila.Pop();//Quita Estado
			listaParam = ((NoTerminal) pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			id = new Id(((Terminal)pila.Pop()).simbolo);
			pila.Pop();//Quita Estado
			tipo = new Tipo(((Terminal)pila.Pop()).simbolo);
			pila.Pop();//Quita Estado
			pila.Pop();//quita ,
		}
	}

	public class Asignacion : Nodo
	{
		Nodo id;
		Nodo expresion;
		public Asignacion(Stack pila)//Sentencia -> id = Expresion ;
		{
			pila.Pop();//Quita Estado
			pila.Pop();//Quita ;
			pila.Pop();//Quita Estado
			expresion = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita =
			pila.Pop();//Quita Estado
			id = new Id(((Terminal)pila.Pop()).simbolo);
		}
		public override void validatipos(List<object> tabsim, List<string> errores)
		{
			id.validatipos(tabsim, errores);
			expresion.validatipos(tabsim, errores);
			if (id.tipodato == 'c' && expresion.tipodato == 'c')
			{
				tipodato = 'c';
			}
			else
			{
				if (id.tipodato =='i' && expresion.tipodato == 'i')
				{
					tipodato = 'i';
				}
				else
				{
					if (id.tipodato == 'f' && expresion.tipodato == 'f')
					{
						tipodato = 'f';
					}
					else
					{
						tipodato = 'e';
						errores.Add("el tipo de dato de " +id.simbolo + " es diferente al de la expresion");
					}
				}
			}
			if (expresion.siguiente != null) expresion.siguiente.validatipos(tabsim,errores);
			if (siguiente != null) siguiente.validatipos(tabsim, errores);
		}
	}

	public class If: Nodo
	{
		Nodo Expresion;
		Nodo SentenciaBloque;
		Nodo Otro;
		public If(Stack pila)//Sentencia -> if ( Expresion ) SentenciaBloque Otro
		{
			pila.Pop();//Quita Estado
			Otro = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			SentenciaBloque = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita )
			pila.Pop();//Quita Estado
			Expresion = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita (
			pila.Pop();//Quita Estado
			pila.Pop();//Quita if
		}
	}

	public class While: Nodo
	{
		Nodo Expresion;
		Nodo Bloque;
		public While(Stack pila)//Sentencia ->while (Expresion ) Bloque
		{
			pila.Pop();//Quita Estado
			Bloque = ((NoTerminal) pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita )
			pila.Pop();//Quita Estado
			Expresion = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita (
			pila.Pop();//Quita Estado
			pila.Pop();//Quita while
		}
	}

	public class Return: Nodo
	{
	Nodo Expresion;
		public Return(Stack pila)//Sentencia -> return Expresion ;
		{
			pila.Pop();//Quita Estado
			pila.Pop();//Quita ;
			pila.Pop();//Quita Estado
			Expresion = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita return
		}
	}

	public class ListaArgumentos: Nodo
	{
		Nodo listaArgumentos;
		Nodo Expresion;
		public ListaArgumentos(Stack pila)//ListaArgumentos ->, Expresion ListaArgumentos
		{
			pila.Pop();//Quita Estado
			listaArgumentos = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			Expresion = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita ,
		}
	}

	public class LlamadaFunc : Nodo
	{
		Nodo id;
		Nodo Argumentos;
		public LlamadaFunc(Stack pila)//LlamadaFunc -> id ( Argumentos )
		{
			pila.Pop();//Quita Estado
			pila.Pop();//Quita )
			pila.Pop();//Quita Estado
			Argumentos = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			pila.Pop();//Quita (
			pila.Pop();//Quita Estado
			id = new Id(((Terminal)pila.Pop()).simbolo);
		}

		public override void validatipos(List<object> tabsim,List<string> errores) 
		{
			Nodo aux = new Nodo(ambito);
			tipodato=buscartipo(tabsim, id.simbolo);
			aux=Argumentos;
			string cadena = "";
			while (aux!=null)
			{
				char tipo2;
				tipo2=buscartipo2(tabsim, aux.simbolo, ambito);
				cadena+=tipo2;
				aux=aux.siguiente;
			}

			if(Argumentos!=null) Argumentos.validatipos(tabsim, errores);
			if (id.simbolo == "print")
				id.validatipos(tabsim, errores);
			else
			{
				Console.WriteLine("entra a ver si existe la funcion");
				if (existefunc(tabsim, id.simbolo, ambito, cadena, errores))
				{
					//id.validatipos(tabsim, cadena, errores);
				}
			}
			if(siguiente!=null) siguiente.validatipos(tabsim, errores);
		}

		public bool existefunc(List<object> tabsim,string id,string ambito,string cadenapa,List <string> errores){
			bool existe = false;
			foreach (ElementoTabla s in tabsim)
			{
				if (s.id==id){
					existe=true;

					Console.WriteLine("en la funcion " +id);
					Console.WriteLine("stpara=" + s.stpara + " === cadenapa=" + cadenapa);
					if(s.stpara == cadenapa)
					return true;
					else{
					errores.Add("los parametros de la funcion " + id + " son incorrectos");
					}
				}
			}
			if(!existe)errores.Add("la funcion"+id+" no existe");
			return false;
		}

	}

	public class Expresion: Nodo
	{
		Nodo izquierda;
		Nodo derecha;
		public Expresion(Stack pila)//Expresion -> Expresion opSuma Expresion
		{
			pila.Pop();//Quita Estado
			derecha = ((NoTerminal)pila.Pop()).nodo;
			pila.Pop();//Quita Estado
			simbolo = ((Terminal)pila.Pop()).simbolo;
			pila.Pop();//Quita Estado
			izquierda = ((NoTerminal)pila.Pop()).nodo;
		}
		public override void validatipos(List<object> tabsim, List<string> errores)
		{
			if (siguiente != null) siguiente.validatipos(tabsim, errores);
		}
	}
}
