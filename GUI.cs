using System;
using dane;
using System.Windows.Forms;


class GUI : Form //Formularz do obsługi okna użytkownika
{
    private void OnWczytajDaneClick(object sender, EventArgs ea)
    {
        OpenFileDialog openFileDialog1 = new OpenFileDialog();

        openFileDialog1.InitialDirectory = "c:\\";
        openFileDialog1.Filter = "txt files (*.txt)|*.txt";
        openFileDialog1.RestoreDirectory = true;

        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            czytnik_danych cd = new czytnik_danych(openFileDialog1.FileName);
        }
    }

    GUI () 
    {      
        Text = "Analiza danych giełdowych";//Tytuł okna głównego
        Width = 320;//szerokość okna głównego
        Height = 200;//wysokość okna głównego

        //Struktura menu

        MainMenu MenuGłówne = new MainMenu();// Tworzenie menu głównego
        MenuItem MenuGłówneMiDane = new MenuItem();// Tworzy submenu Dane w menu głównym
        MenuGłówneMiDane.Text = "Dane";// Nadaje nazwe submenu Dane
        MenuItem MenuGłówneMiDaneMiWczytajDane = new MenuItem();// Tworzy pozycje wczytaj dane w submenu Dane
        MenuGłówneMiDaneMiWczytajDane.Text = "Wczytaj dane";//Nadaje nazwę pozycji wczytaj dane w submenu Dane
        MenuGłówneMiDaneMiWczytajDane.Click += new EventHandler(OnWczytajDaneClick);//Przypisuje metodę wywoływaną po kliknięciu wczytaj dane
        MenuGłówneMiDane.MenuItems.Add(MenuGłówneMiDaneMiWczytajDane);//Dodaje pozycję wczytaj dane do sub menu dane 
        MenuGłówne.MenuItems.Add(MenuGłówneMiDane);//Dodaje submenu dane do Głównego Menu
        Menu = MenuGłówne;//Przypisz strukturę menu do menu okna
    }

    [STAThread]
    public static void Main()
    {
        Application.Run(new GUI());
    }

}

