using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Kontomanager;

namespace Kontomanager_0._3
{
    //private filehelper FileHelper { get; set; }
    public partial class Form1 : Form
    {
        filehelper file = new filehelper("Aktivitaeten.txt");

        //Gutschriftvariablen
        string gBetrag;
        string gAbsender;
        string gDatum;

        //Abbuchungvariablen
        string aBetrag;
        string aEmpfaenger;
        string aDatum;

        // Kontostand
        string AnzeigeKontostand; // Anzeigestand
        double RechenKontostand; // Rechenstand
        
        // Abspeichern
        string pathlog = "Aktivitaeten.txt";
        string pathkonto = "Kontostand.txt";
        string pathcounter = "Counter.txt";
        string patherstesmal = "True.txt";

       // Transaktionenzähler variablen
        int counter;
        string lbcounter;

        // ProgrammCode

        public Form1()
        {
            InitializeComponent();

            this.lbDatum.Text = "Willkommen User, heute ist der " + DateTime.Now.ToLocalTime();

            this.lblStatus.Text = "Alles wurde korrekt geladen";

        }

        private void btnGutschrift_Click(object sender, EventArgs e)
        {
            Gutschrift();
        }

        private void btnAbbuchung_Click(object sender, EventArgs e)
        {
            Abbuchung();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Speichern();
        }

        private void btnBeenden_Click(object sender, EventArgs e)
        {
            Close();
        }


        void Gutschrift()
        {
            Zähler();
            gBetrag = this.tbgBetrag.Text;
            gAbsender = this.tbgAbsender.Text;
            gDatum = this.tbgDatum.Text;

            this.tbLog.Text += "(" + lbcounter + ")" + "[" + gDatum + "] " + gBetrag + "€ Erhalten von" + gAbsender + "\n";

            RechenKontostand += Convert.ToDouble(gBetrag);

            this.lbKontostand.Text = "Kontostand: " + RechenKontostand + "€";
        }

        void Abbuchung()
        {
            Zähler();
            aBetrag = this.tbaBetrag.Text;
            aEmpfaenger = this.tbaEmpfaenger.Text;
            aDatum = this.tbaDatum.Text;

            this.tbLog.Text += "(" + lbcounter + ")" + "[" + aDatum + "] " + aBetrag + "€ Gesendet an: " + aEmpfaenger + "\n";

            RechenKontostand -= Convert.ToDouble(aBetrag);

            this.lbKontostand.Text = "Kontostand: " + RechenKontostand + "€";
        }

        void Zähler()
        {
            counter += 1;

            lbcounter = counter.ToString();

            lbTransaktionen.Text = "Transaktionen: " + lbcounter;
        }

        void Speichern()
        {
            string textlog = this.tbLog.Text;

                DialogResult Result = MessageBox.Show("Datei hier abspeichern", "Information", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if(Result == DialogResult.Yes)
                {

                // File überschreiben und speichern LOG
                byte[] array = Encoding.UTF8.GetBytes(textlog.Replace("\n", Environment.NewLine‌​));
                File.WriteAllBytes(pathlog, array);

                // File überschreiben und speichern Kontostand

                byte[] Kontostandarray = BitConverter.GetBytes(RechenKontostand);
                File.WriteAllBytes(pathkonto, Kontostandarray);
                }

                byte[] CounterArray = BitConverter.GetBytes(counter);
                File.WriteAllBytes(pathcounter, CounterArray);

        }




        private void bugReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://k-development.de/projects/viw/issues");
        }

        private void kontostandSetztenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://k-development.de/projects/wiki/kontostand");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void beimErstenStartenDesProgrammsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string leer = "";

            File.WriteAllText(pathcounter, leer);
            File.WriteAllText(pathkonto, leer);
            File.WriteAllText(pathlog, leer);

            File.WriteAllText(patherstesmal, leer);
        }

        private void normalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Kontostand aus Datei auslesen und in variable speichern anschließend in tb schreiben


            byte[] array = File.ReadAllBytes(pathkonto);
            RechenKontostand = BitConverter.ToDouble(array, 0);

            byte[] array2 = File.ReadAllBytes(pathkonto);
            AnzeigeKontostand = BitConverter.ToString(array2);

            this.lbKontostand.Text = "Kontostand: " + File.ReadAllText(pathkonto) + "€";


            string[] Log = File.ReadAllLines(pathlog);


            this.tbLog.Lines = Log;

            byte[] causlesen = File.ReadAllBytes(pathcounter);
            counter = BitConverter.ToInt16(causlesen, 0);

        }
    }
}
