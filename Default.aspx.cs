using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace FileGuard
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void TextBoxInput_TextChanged(object sender, EventArgs e)
        {
        }

        protected void TextBoxOutput_TextChanged(object sender, EventArgs e)
        {
        }

        // Start the program after pressing the button.
        protected void ButtonForAnalysis_Click(object sender, EventArgs e)
        {
            string pathToAnalyzeFolder = TextBoxInput.Text;
            bool error = false;
            LabelPathOutput.Text = "";

            // Creating a database from the path to the analyze folder.
            Database database = new Database(pathToAnalyzeFolder);

            // Loads data from the analyzed folder to the database.
            try
            {
                database.LoadAnalyzedFileData();
            }
            catch
            {
                database.message += "Nepodařilo se načíst soubory ze složky." + "\n";
                error = true;
            }

            // Loads data from the file with data to the database and compares these files.
            if (!error)
            {
                if (System.IO.File.Exists(database.pathToFileWithData))
                {
                    try
                    {
                        database.LoadDataFromFileWithData();

                    }
                    catch
                    {
                        database.message += "Nepodařilo se načíst data." + "\n";
                        error = true;
                    }
                    database.CompareFiles();

                }
                else
                {
                    database.message += "Nový adresář, žádné změny." + "\n";
                }
            }

            // Saves the data of the analyzed file to the file with data.
            if (!error)
            {
                try
                {
                    database.SaveDatabase();

                    // Report about the path to the file where the files data is stored.
                    LabelPathOutput.Text = "Data o adresáři uložena: " + database.pathToFileWithData;

                }
                catch
                {
                    database.message += "Nepodařilo se uložit databázi." + "\n";
                }
            }

            //Report on new, deleted and changed files. Or about mistakes.
            TextBoxOutput.Text = database.message;
        }

    }
}