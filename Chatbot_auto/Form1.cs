using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


//strona tytulowa, cel projektu, zalozenia, opis architektury, urzyte narzedzia screenshoot literatura

namespace Chatbot_auto
{
    public partial class Form1 : Form
    {
        string wiadomosc_klienta = "";
        string wiadomosc_boota = "";

        int czy_oferta = 0;

        int aktywny_enter = 0;

        int licznik_powitanie = 0;
        int licznik_oferta = 0;
        int licznik_wiadomosci = 0;
        int licznik_pozegnanie = 0;

        string powitanie_niezalogowany = "Witam drogi kliencie. Jestem chatbottem auto-wypożyczalni. ";
        string powitanie_zalogowany = "Witam Panie Adamie. Jestem chatbottem auto-wypożyczalni. ";

        string pytanie_podstawowe = "W czym mogę pomóc? ";
        string pytanie_podstawowe2 = "Czy mogę w czymś jeszcze doradzić? ";

        string niezrozumiale = "To zdanie jest dla mnie niejasne. Odpowiadam tylko na pytania powiązane z usługami jakie świadczy nasza firma. Jeżeli wypowiedź była z tym związana, proszę o ujęcie innymi słowami. ";



        string[] tagi = new string[] 
        {
            "witam","dzien dobry","dobry wieczor","siema","elo","siemanko","dziendobry","dobrywieczor","hello","czesc",

            "strona internetowa","adres www","adres strony","stronawww","strona www","strony internetowej","web page","strona online","strona wwww","strona ww",
            "poczta elektroniczna","adres e-mail","adres email","email","e-mail","skrzynka pocztowa","poczty e-mail","poczty email","wiadomosc email","wiadomosc e-mail",
            "jak leci","sie masz","co slychac","u ciebie","jak tam","jak dzien","jak noc","sie czujesz","jak dzionek","co ciekawego",
            "jakie dokumenty","jaki dokument","co potrzebne","co potrzebuje","co potrzeba","jaki dokumenty","wystarczy dowod","sam dowod","tylko dowod","dokumenty wymagane",
            "potrzebuje pomocy","potrzebuje informacji","czy pomozesz","czy wspomozesz","moge zapytac","mam pytanie","mam pytania","inne pytanie","pare pytan","kilka pytan",
            "cena auta", "cena samochodu", "koszt wypozyczenia", "cennik uslugi", "koszt uslugi", "ceny aut", "kosztuje wypozyczenie", "kosztowac wypozyczenie", "koszt", "cennik", 
            "telefon", "komorka", "komurka", "infolinia", "numer telefonu", "numer komurkowy", "nomer komorkowy","telefon kontaktowy","numer kontaktowy","aktualny numer",
            "czynne", "otwarte", "godziny pracy", "godzina otwarcia", "otwarta", "otwarcie", "zamkniecie", "koniec pracy", "godziny przyjec", "czyne",
            "adres firmy", "ulica", "przy ulicy", "znajduje", "siedziba", "lokalizacja firmy", "jak dojechac", "gdzie znajde", "adres biura", "adresem",
            "jakie samochody", "jakie auta", "dostepne samochody", "dostepne auta", "dostepne bryki", "katalog samochodow", "jaki samochod", "jakie auto", "pojazdy", "jaki pojazd",
            "okres wynajmu", "czas wynajmu", "minimalny czas", "maksymalny okres", "jak dlugo", "maksymalny czas", "minimalny okres", "jak krotko", "jak dlugo", "jaki okres",
            "awaria samochodu", "wypadek", "kolizja", "zdarzenie drogowe", "usterka", "usterki", "wypadku", "awarii", "kolizji", "zdarzenia drogowego",
            "dziekuje", "uszanowanie", "dzieki", "super dziekuje", "thx", "podziekowal", "podziekowac", "dzieks", "za pomoc", "za podpowiedz", "za rade",
            "chcialbym wypozyczyc", "formularz wypozyczenia", "wynajac samochod", "wynajac auto", "chcialbym wynajac", "formularz wynajecia", "wypozyczyc auto", "wypozyczyc samochod", "wypozyczyc pojazd", "wynajac pojazd", "jak wypozyczyc",

            "dobranoc", "zegnam", "do widzenia", "dowidzenia", "czesc", "nara", "dobrej nocy", "milego dnia", "zegnaj", "milej pracy", "trzymaj sie",
        };

        string[] odpowiedzi = new string[]
            {
            "Witam ponownie w naszej wypożyczalni sprzętu sportowego. ",
            "Adres strony internetowej to: http://wypozyczalnia-sprzetu.pl/ . ",
            "Email: wypozyczalniasprzetu@gmail.com . ",
            "Wszystko dobrze :) dziękuje za troskę. ",
            "Wymagane dokumenty do wypożyczenia sprzętu to: ważny dowód osobisty. ",
            "Słucham. Służę pomocą. ",
            "Nasz cennik znajduje sie na stronie http://wypozyczalnia-sprzetu/cennik, szczegółowych informacji udzieli państwu pracownik pod numerem telefonu 666 777 888. ",
            "Telefon do biura to: 604 670 340. ",
            "Firma jest otwarta od godziny 10 do godziny 18. ",
            "Adres firmy: Piłsudzkiego 23d/5 Koszali . ",
            "W naszej ofercie znajduja sie narty, bluzy sportowe do biegania, korki marki adidas i nike, dresy sportowe, torby sportowe, opaski na reke, zegarki sportowe, stroj pilkarski ",
            "Czas wynajmu sprzętu wynosi maksymalnie 2 tygodnie.W razie przedluzenia wynajmuje, toprosze nas o tym poinformowac  ",
            "W przypadku uszkodzenie sprzętu sportowego, prosze o poinformowanie naszej wypozyczalni pod numerem: 666 345 765",
            "Zawsze do usług :) ",
            "Proszę o kontakt telefoniczny z pracownikiem biura: 543 234 233 lub wypełnienie formularza na stronie internetowej pod adresem http://wypozyczalnia-sprzetu/formularz",
            "Zapraszam ponownie do skorzystanie z naszych ofert. Milego dnia"
            };

    string[] oferty = new string[] 
        {
            "Chciałbym zaproponować naszą ofertę promocyjną: Tylko w tym tygodniu można wypożyczyć samochód VW Golf 1.6TDI na okres 2 tygodni za jedyne 200zł. Czy drogi kliencie jesteś zainteresowany?",
            "Chciałbym zaproponować naszą ofertę promocyjną: Tylko w tym miesiącu można wypożyczyć samochód Skoda Fabia 1.6TDI na okres 3 tygodni za jedyne 250zł. Czy drogi kliencie jesteś zainteresowany?",
            "Chciałbym zaproponować naszą ofertę promocyjną: Tylko w tym tygodniu można wypożyczyć samochód Toyota Yaris 1.3 na okres 4 tygodni za jedyne 300zł. Czy drogi kliencie jesteś zainteresowany?"
        };
        string[] oferty_tagi = new string[] 
        {
            "tak", "jasne", "oczywiscie", "pewnie", "zainteresowany", "interesuje",
            "nie", "niee", "odmawiam", "nieinteresuje", "odrzucam", "spadaj"
        };
        string[] oferty_odpowiedzi = new string[] 
        {
            "Szczegóły oferty znajdują się pod wskazanym adresem:http://auto-wypozyczalnia.pl/promocje . Zapraszamy również do kontaktu z naszym przedstawicielem: 777 888 999",
            "Ok. W takim razie jak mogę jeszcze pomóc?"
        };

        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            pictureBox1.Image = Image.FromFile(@"original1.gif");
        }

        public void wyslij_wiadomosc()
        {
            if (textBox1.Text != "" && textBox1.Text != " " && textBox1.Text != String.Empty)
            {
                wiadomosc_klienta = textBox1.Text;
                richTextBox1.Text = richTextBox1.Text + "Ty:    " + wiadomosc_klienta + "\n";

                //------- konwersja wiadomosci -------------------------
                wiadomosc_klienta = wiadomosc_klienta.Replace("ą","a");
                wiadomosc_klienta = wiadomosc_klienta.Replace("ć", "c");
                wiadomosc_klienta = wiadomosc_klienta.Replace("ę", "e");
                wiadomosc_klienta = wiadomosc_klienta.Replace("ó", "o");
                wiadomosc_klienta = wiadomosc_klienta.Replace("ś", "s");
                wiadomosc_klienta = wiadomosc_klienta.Replace("ł", "l");
                wiadomosc_klienta = wiadomosc_klienta.Replace("ż", "z");
                wiadomosc_klienta = wiadomosc_klienta.Replace("ź", "z");
                wiadomosc_klienta = wiadomosc_klienta.Replace("ń", "n");
                wiadomosc_klienta = wiadomosc_klienta.Replace("!", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace("?", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace("@", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace("#", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace("$", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace("*", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace("&", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace(".", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace(",", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace(";", " ");
                wiadomosc_klienta = wiadomosc_klienta.Replace("€", "u");
                wiadomosc_klienta = wiadomosc_klienta.Replace(Environment.NewLine, " ");
                //-------- koniec konwersji -----------------------------

                odpowiedz_wiadomosc(wiadomosc_klienta);
                textBox1.Text = "";
            }
        }

        public async Task oferta_wiadomosc(int licz_wiad)
        {
            if (licz_wiad == licznik_wiadomosci && licznik_oferta < 1 && licznik_wiadomosci > 5)
            {
                pictureBox1.InitialImage = null;
                pictureBox1.Image = Image.FromFile(@"original.gif");
                await Task.Delay(6000);

                Random rnd = new Random();
                int ofertx = rnd.Next(1, 3);
                ofertx = ofertx - 1;

                richTextBox1.Text = richTextBox1.Text + "Chatbott:  " + oferty[ofertx] + "\n";
                licznik_oferta++;
                czy_oferta = 1;

                pictureBox1.InitialImage = null;
                pictureBox1.Image = Image.FromFile(@"original1.gif");
            }
            else if (licz_wiad == licznik_wiadomosci && licznik_wiadomosci > 1)
            {
                pictureBox1.InitialImage = null;
                pictureBox1.Image = Image.FromFile(@"original.gif");
                await Task.Delay(6000);

                licznik_wiadomosci++;
                richTextBox1.Text = richTextBox1.Text + "Chatbott:  " + pytanie_podstawowe2 + "\n";

                pictureBox1.InitialImage = null;
                pictureBox1.Image = Image.FromFile(@"original1.gif");
            } 
        }

        public async Task odpowiedz_wiadomosc(string wiadomosc)
        {
            //richTextBox1.Text = richTextBox1.Text + wiadomosc;
            await Task.Delay(2500);
            pictureBox1.InitialImage = null;
            pictureBox1.Image = Image.FromFile(@"original1.gif");

            string zdanie_oferta = "";
            int znalezione3 = -1;
            int znalezione4 = -1;

            string[] zdanie_odpowiadajace = new string[] 
            {
                "", "", "", "", "","", "", "", "", "", "","","","", "","",
            };
            int znalezione1 = -1;
            int znalezione2 = -1;
            int czy_odpowiadac = -1;
            int licznik_odpowiedzi = 0;

            wiadomosc_boota = "";

            //-------------------- Powitanie --------------------
            if (licznik_powitanie == 0)
            {
                if (comboBox1.Text == "Zalogowany Adam")
                {
                    wiadomosc_boota = powitanie_zalogowany;
                }
                else
                {
                    wiadomosc_boota = powitanie_niezalogowany;
                }
                licznik_powitanie = 1;
            }

            //-------------------- Analiza ------------------------
            string[] slowa = wiadomosc.Split(' ');

            var e = from s in slowa select s;
            int ile_slow = e.Count();

            if (czy_oferta == 1)
            {
                for (int i = 0; i <= ile_slow - 1; i++)
                {
                    if (i > 0)
                    {
                        znalezione3 = Array.IndexOf(oferty_tagi, slowa[i - 1] + " " + slowa[i]);
                        // richTextBox1.Text = richTextBox1.Text + "\nz3 - " + znalezione3;
                    }
                    znalezione4 = Array.IndexOf(oferty_tagi, slowa[i]);

                    //richTextBox1.Text = richTextBox1.Text + "\nz4 - "  + znalezione4;

                    if (znalezione3 > -1)
                    {
                        zdanie_oferta = oferty_odpowiedzi[znalezione3 / 6];
                        czy_odpowiadac = 1;
                        licznik_wiadomosci++;
                    }
                    if (znalezione4 > -1)
                    {
                        zdanie_oferta = oferty_odpowiedzi[znalezione4 / 6];
                        czy_odpowiadac = 1;
                        licznik_wiadomosci++;
                    }
                    czy_oferta = 0;
                }
            }

            for (int i = 0; i <= ile_slow - 1; i++)
            {
                znalezione1 = -1;
                znalezione2 = -1;
                if (i > 0)
                {
                    znalezione1 = Array.IndexOf(tagi, slowa[i-1] + " " + slowa[i]);
                }
                    znalezione2 = Array.IndexOf(tagi, slowa[i]);
                //richTextBox1.Text = richTextBox1.Text + znalezione1+ " - "+znalezione2+"\n";
                
                if (znalezione2 > -1)
                {
                    if (znalezione2 < 10)
                    {

                    }
                    else
                    {
                        if (licznik_wiadomosci < 1 && (znalezione2 / 10) == 0)
                        {
                        }
                        else
                        {
                            zdanie_odpowiadajace[znalezione2 / 10] = odpowiedzi[znalezione2 / 10];
                            licznik_odpowiedzi++;
                            czy_odpowiadac = 1;
                            if ((znalezione2 / 10) == 15)
                            {
                                licznik_pozegnanie++;
                            }
                        }
                        
                    }
     
                }
                if (znalezione1 > -1)
                {
                    if (znalezione1 < 10)
                    {

                    }
                    else
                    {
                        if (licznik_wiadomosci < 1 && (znalezione1 / 10) == 0)
                        {
                        }
                        else
                        {
                            zdanie_odpowiadajace[znalezione1 / 10] = odpowiedzi[znalezione1 / 10];
                            licznik_odpowiedzi++;
                            czy_odpowiadac = 1;
                            if ((znalezione1 / 10) == 15)
                            {
                                licznik_pozegnanie++;
                            }
                        }
                        
                    }
                }
            }
            
            for (int i = 0; i <= 15; i++)
            {
                wiadomosc_boota = wiadomosc_boota + zdanie_odpowiadajace[i];
                //richTextBox1.Text = richTextBox1.Text + czy_odpowiadac + " - "+ i + " - " + zdanie_odpowiadajace[i] + "\n";
            }
            

            licznik_wiadomosci = licznik_wiadomosci + 1;

            //-------------------- Odpowiedz -----------------------
            pictureBox1.InitialImage = null;
            pictureBox1.Image = Image.FromFile(@"original.gif");
            await Task.Delay(6000);
            if (czy_odpowiadac >= 0)
            {
                richTextBox1.Text = richTextBox1.Text + "Chatbott:  " + zdanie_oferta + wiadomosc_boota + "\n";
            }
            else if (czy_odpowiadac < 0 && licznik_wiadomosci <= 1)
            {
                richTextBox1.Text = richTextBox1.Text + "Chatbott:  " + wiadomosc_boota + pytanie_podstawowe +"\n";
            }
            else if (czy_odpowiadac < 0 && licznik_wiadomosci > 1)
            {
                richTextBox1.Text = richTextBox1.Text + "Chatbott:  " + niezrozumiale + "\n";
            }
            
            //-------- koniec odpowiedzi ----------------
            await Task.Delay(3000);
            pictureBox1.InitialImage = null;
            pictureBox1.Image = Image.FromFile(@"original1.gif");
            int lw = licznik_wiadomosci;

            if (licznik_pozegnanie < 1)
            {
                await Task.Delay(30000);
                oferta_wiadomosc(lw);
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            // set the current caret position to the end
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            // scroll it automatically
            richTextBox1.ScrollToCaret();
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Niezalogowany" || comboBox1.Text == "Zalogowany Adam")
            {
                button2.Enabled = true;
                aktywny_enter = 1;
            }
            else
            {
                button2.Enabled = false;
                aktywny_enter = 0;
            }
        }

        private void textBoxTest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && aktywny_enter ==1)
            {
                if (licznik_wiadomosci > 0)
                {
                    comboBox1.Enabled = false;
                }
                else
                {
                    comboBox1.Enabled = true;
                }

                comboBox1.Enabled = false;
                wyslij_wiadomosc();
                textBox1.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (licznik_wiadomosci > 0)
            {
                comboBox1.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
            }

            comboBox1.Enabled = false;
            wyslij_wiadomosc();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
            comboBox1.Enabled = true;
            button2.Enabled = true;

            wiadomosc_klienta = "";
            wiadomosc_boota = "";

            licznik_powitanie = 0;
            licznik_oferta = 0;
            licznik_wiadomosci = 0;
            licznik_pozegnanie = 0;
            aktywny_enter = 0;

            textBox1.Text = "";
        }
    }
}
