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
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Globalization;
using System.Threading;
using System.Runtime.InteropServices;

namespace TirageTombola
{
    public partial class Tombola : Form
    {
        string _filename = "";
        private Microsoft.Office.Interop.Excel.Application _excel;
        private Workbooks _workbooks;
        private Workbook _xlWorkBookLots;
        Dictionary<int, Lot> _ListeLots = new Dictionary<int, Lot>();  // numero de row du lot et info lot (à partir de 1)
        private int _maxProgram;
        List<Programme> _ListeProgrammesVendusEleves = new List<Programme>();  // liste des programmes et des eleves
        private dynamic _xlWorksheet1;

        private static readonly Random getrandom = new Random();

        public Tombola()
        {
            InitializeComponent();
        }


        public static int GetRandomNumber(int min, int max)
        {
            lock (getrandom) // synchronize
            {
                return getrandom.Next(min, max);
            }
        }



        private void buttonTirage_Click(object sender, EventArgs e)
        {
            // TIRAGE AU SORT

            buttonTirage.Enabled = false;
            progressBar1.Value = 0;
            progressBar1.Visible = true;

            var copieListeProgrammesVendus = _ListeProgrammesVendusEleves; // on fait une copie

            int compte = 0;
            foreach (var lot in _ListeLots)
            {
                progressBar1.Value = (int)((double)compte / (double)_ListeLots.Count * 20d);
                var indexTire = GetRandomNumber(0, copieListeProgrammesVendus.Count - 1); // index du tableau des programmes vendu (pas le numero du programme directement)
                lot.Value.EleveGagnant = copieListeProgrammesVendus[indexTire];
                copieListeProgrammesVendus.RemoveAt(indexTire);
                compte++;
            }

            // Ecrivons les programmes gagnants pour chaque lot 
            var xlRange = _xlWorksheet1.UsedRange;

            for (int i = 2; i <= xlRange.Rows.Count; i++)
            {
                progressBar1.Value = 20 + (int)((double)i / (double)xlRange.Rows.Count * 40d);
                if (_ListeLots.ContainsKey(i))
                {
                    var xlRangeedit = (Microsoft.Office.Interop.Excel.Range)_xlWorksheet1.Cells[i, 4];
                    xlRangeedit.Value = _ListeLots[i].EleveGagnant.NumeroProgramme;
                }
            }

            // Ecrivons les programmes gagnants et le lot 
            Microsoft.Office.Interop.Excel.Worksheet xlWorksheet2 = _xlWorkBookLots.Sheets[2];

            var ListeDesLotsTriesParProgramme = _ListeLots.OrderBy(l => l.Value.EleveGagnant.NumeroProgramme).ToList();

            int index = 2;
            foreach (var LeLot in ListeDesLotsTriesParProgramme)
            {
                progressBar1.Value = 60 + (int)((double)index / (double)ListeDesLotsTriesParProgramme.Count * 40d);

                var xlRangeedit = (Microsoft.Office.Interop.Excel.Range)xlWorksheet2.Cells[index, 1];
                xlRangeedit.Value = LeLot.Value.EleveGagnant.NumeroProgramme;

                xlRangeedit = (Microsoft.Office.Interop.Excel.Range)xlWorksheet2.Cells[index, 2];
                xlRangeedit.Value = LeLot.Value.Description;

                xlRangeedit = (Microsoft.Office.Interop.Excel.Range)xlWorksheet2.Cells[index, 3];
                xlRangeedit.Value = LeLot.Value.Carton;


                xlRangeedit = (Microsoft.Office.Interop.Excel.Range)xlWorksheet2.Cells[index, 4];
                xlRangeedit.Value = LeLot.Value.EleveGagnant.Nom;

                xlRangeedit = (Microsoft.Office.Interop.Excel.Range)xlWorksheet2.Cells[index, 5];
                xlRangeedit.Value = LeLot.Value.EleveGagnant.Classe;

                index++;
            }
            progressBar1.Visible = false;
            this.Close();
        }

        private void buttonLots_Click(object sender, EventArgs e)
        {
            // LECTURE DES LOTS A PARTIR DU FICHIER

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files|*.xlsx";
                saveFileDialog.FileName = System.IO.Path.GetFileNameWithoutExtension(openFileDialog.FileName) + "-" + DateTime.Now.ToString("hhmmssFFF", CultureInfo.InvariantCulture) + ".xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    File.Copy(openFileDialog.FileName, saveFileDialog.FileName);

                    _filename = saveFileDialog.FileName;
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;

                    xlspath.Text = _filename;

                    object _missingValue = System.Reflection.Missing.Value;
                    _excel = new Microsoft.Office.Interop.Excel.Application();
                    _workbooks = _excel.Workbooks;

                    _xlWorkBookLots = _workbooks.Open(_filename,
                                                                           _missingValue,
                                                                           false,
                                                                           _missingValue,
                                                                           _missingValue,
                                                                           _missingValue,
                                                                           true,
                                                                           _missingValue,
                                                                           _missingValue,
                                                                           true,
                                                                           _missingValue,
                                                                           _missingValue,
                                                                           _missingValue);

                    try
                    {
                        // Lisons les lots 
                        _xlWorksheet1 = _xlWorkBookLots.Sheets[1];
                        var xlRange = _xlWorksheet1.UsedRange;
                        int numlot = 1;

                        for (int i = 2; i <= xlRange.Rows.Count; i++)
                        {
                            progressBar1.Value = (int)((double)i / (double)xlRange.Rows.Count * 80d);
                            if (_xlWorksheet1.Cells[i, 2].Value2 != null)
                            {
                                string qte = Convert.ToString(_xlWorksheet1.Cells[i, 2].Value2);
                                if (qte != null && qte.Trim() == "1") // c'est bien une ligne de lot
                                {
                                    Lot lelot = new Lot { NumeroLot = numlot, Description = Convert.ToString(_xlWorksheet1.Cells[i, 3].Value2), Carton = null };

                                    if (_xlWorksheet1.Cells[i, 1].Value2 != null) // il y a un carton
                                    {
                                        lelot.Carton = Convert.ToString(_xlWorksheet1.Cells[i, 1].Value2);
                                    }

                                    _ListeLots.Add(i, lelot);
                                    numlot++;
                                }
                            }
                        }

                        infolabellots.Text = string.Format("{0} lots", numlot - 1);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        buttonElevesProgrammes.Enabled = true;
                        buttonTirage.Enabled = true;
                        progressBar1.Visible = false;
                    }
                }
            }
        }

        private void buttonElevesProgrammes_Click(object sender, EventArgs e)
        {
            // LECTURE DES PROGRAMMES & ELEVES

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files|*.xlsx";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                buttonTirage.Enabled = false;
                progressBar1.Value = 0;
                progressBar1.Visible = true;
                xlspath_Eleves.Text = openFileDialog.FileName;

                object _missingValue = System.Reflection.Missing.Value;
                var xlWorkBookProgrammes = _excel.Workbooks.Open(openFileDialog.FileName, _missingValue, true);
                _ListeProgrammesVendusEleves.Clear(); // Fichier optionnel qui écrase les hypothèses du fichier des lots

                try
                {
                    // Lisons les eleves 
                    Microsoft.Office.Interop.Excel.Worksheet xlWorksheet = xlWorkBookProgrammes.Sheets[1];
                    var xlRange = xlWorksheet.UsedRange;


                    for (int i = 2; i <= xlRange.Rows.Count; i++)
                    {
                        progressBar1.Value = (int)((double)i / (double)xlRange.Rows.Count * 100d);

                        if (xlWorksheet.Cells[i, 1].Value2 != null)  // il y a numero de programme
                        {
                            int numProgram = Convert.ToInt32(xlWorksheet.Cells[i, 1].Value2);

                            if (xlWorksheet.Cells[i, 4].Value2 != null && Convert.ToDouble(xlWorksheet.Cells[i, 4].Value2) == 1) // Case colonne D payé avec un 1 (cas normal), sinon pas acheté
                            {
                                _ListeProgrammesVendusEleves.Add(new Programme()
                                {
                                    Nom = Convert.ToString(xlWorksheet.Cells[i, 2].Value2),
                                    Classe = Convert.ToString(xlWorksheet.Cells[i, 3].Value2),
                                    NumeroProgramme = numProgram
                                });
                            }
                        }
                    }

                    infolabel_Programmes.Text = string.Format("{0} programmes", _ListeProgrammesVendusEleves.Count);
                    buttonTirage.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    xlWorkBookProgrammes.Close();
                    Marshal.ReleaseComObject(xlWorkBookProgrammes);
                    progressBar1.Visible = false;
                }
            }
        }

        private void Tombola_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _xlWorkBookLots.Save();
                _xlWorkBookLots.Close(true);
                _workbooks.Close();
                _excel.Quit();

                Marshal.ReleaseComObject(_xlWorkBookLots);
                Marshal.ReleaseComObject(_workbooks);
                Marshal.ReleaseComObject(_excel);

                Process.Start(_filename);
            }
            catch
            {

            }
        }

        private void Tombola_Load(object sender, EventArgs e)
        {
            infolabel_Programmes.Text = infolabellots.Text = xlspath.Text = xlspath_Eleves.Text = "";
        }
    }
}
