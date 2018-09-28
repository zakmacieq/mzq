using System;
using czytnik_danych;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

namespace Modele
{
    public class Model
    {
        private List<kursDnia> dane;
        public double średnia { get; set; }
        public double wariancja { get; set; }
        public double odchylenie_standardowe { get; set; }
        public double kurtoza { get; set; }

        Model(List<kursDnia> dane)
        {
            this.dane = dane.OrderByDescending(kd => kd.data).ToList();
        }

        private List<kursDnia> weźKursyMiesięczne()
		{
			List<kursDnia> kursyMiesięczne = new List<kursDnia>();
			//maksymalna data w zbiorze danych
            DateTime walidatorDaty = this.dane.Max(kd => kd.data);
			
            foreach(kursDnia kd in this.dane)
			{
				if(kd.data == walidatorDaty)
				{
					//wpisuje kurs miesięczny do listy z kursami miesięcznymi
					kursyMiesięczne.Add(kd);
                    //ustala kolejny wpis miesięczny do listy z kursami miesięcznymi
                    walidatorDaty = walidatorDaty.AddMonths(-1);
				}
			}
            return kursyMiesięczne;

		}


        public double wyliczŚrednią(List<kursDnia> dane)
        {
            return dane.Average(kd => kd.kursClose);
        }

        //testuj model
        public static void Main()
        {
            double średnia;
            double średniaMiesięczna;

            Model m = new Model(new List<kursDnia> { 
                new kursDnia { spółka = "testowa", data = new DateTime(2018,12,31), kursClose = 1},
                new kursDnia { spółka = "testowa", data = new DateTime(2018,12,30), kursClose = 10},
                new kursDnia { spółka = "testowa", data = new DateTime(2018,11,30), kursClose = 1},
                new kursDnia { spółka = "testowa", data = new DateTime(2018,11,20), kursClose = 15},
            });

            Console.WriteLine("Lista kursów:");
            foreach (kursDnia kd in m.dane)
            {
                Console.WriteLine("spółka:{0}, data:{1}, kurs Close: {2}", kd.spółka, kd.data, kd.kursClose);
            }

            średnia = m.wyliczŚrednią(m.dane);
            Console.WriteLine("średnia z całości danych wynosi: {0}", średnia);

            List<kursDnia> daneMiesięczne = m.weźKursyMiesięczne();

            Console.WriteLine("Lista kursów miesięcznych:");
            foreach (kursDnia kd in daneMiesięczne)
            {
                Console.WriteLine("spółka:{0}, data:{1}, kurs Close: {2}", kd.spółka, kd.data, kd.kursClose);
            }


            średniaMiesięczna = m.wyliczŚrednią(daneMiesięczne);
            Console.WriteLine("średnia miesięczna z danych wynosi: {0}", średniaMiesięczna);
        }


    }
}