using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace czytnik_danych
{
	public class kursDnia//klasa przechowująca kurs z danego dnia
	{
		public String spółka { get; set; }
		public DateTime data { get; set; }
		public double kursOpen { get; set; }
		public double kursLow { get; set; }
		public double kursHigh { get; set; }
		public double kursClose { get; set; }
		public Int32 Wolumen { get; set; }
	}



	public class dane //klasa, w której przychowywana jest lista kursów z pliku txt
	{
		public List<kursDnia> ListaKursów {get; set;}//publiczna właściwość gdzie przechowywana jest lista kursów zaczytana z pliku
        private List<kursDnia> Lista; //kolekcja do której zaczytywane są kursy dnia
		private String ścieżka; //ścieżka do pliku z danymi
		private StreamReader sr; //klasa, która zczytuje dane z pliku tekstowego

		dane(String ścieżka)
		{

			this.ścieżka = ścieżka;//ścieżka do pliku z danymi
			Lista = new List<kursDnia>();//stwórz listę, w której będą przechowywane dane 
			String line;
			String[] splitLine;

			try//przechwyć wyjątek jeżeli nie uda się otworzyć pliku z danymi
			{
				sr = new StreamReader(this.ścieżka);
			}
			catch (Exception)
			{
				throw new Exception("Otwarcie pliku: " + this.ścieżka + " nie powiodło się.");
			}

			try
			{
				while ((line = sr.ReadLine()) != null)
				{
					splitLine = line.Split(new char[] { ',' }, 7);

					if (line.StartsWith("<TICKER>") != true)
					{
						Lista.Add(new kursDnia
						{
							spółka = splitLine[0],
							data = new DateTime(Int32.Parse(splitLine[1].Substring(0, 4)), Int32.Parse(splitLine[1].Substring(4, 2)), Int32.Parse(splitLine[1].Substring(6, 2))),                   
							kursOpen = double.Parse(splitLine[2],CultureInfo.InvariantCulture),
							kursLow = double.Parse(splitLine[3],CultureInfo.InvariantCulture),
							kursHigh = double.Parse(splitLine[4],CultureInfo.InvariantCulture),
							kursClose = double.Parse(splitLine[5],CultureInfo.InvariantCulture),
							Wolumen = Int32.Parse(splitLine[6])

						});
					}
				}
				sr.Close(); //zamknij strumień danych
                ListaKursów = Lista; //przypisz publicznej właściwości dane zaczytane z pliku
			}
			catch (Exception)
			{
				throw new Exception("Nie udało się odczytać danych z pliku");
			}

		}

		public static void Main(String[] args)
		{
			dane d = new dane(args[0]);
		}
	}
}