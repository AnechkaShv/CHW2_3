using DataProcessing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessing
{
    public class FileSaving
    {
        readonly string fPath;
        readonly string nPath;
        private Notarius[] _resTable;
        public FileSaving() { }
        public FileSaving(PathProcessing fPath, Notarius[] resTable)
        {
            this.fPath = fPath.FPath;
            _resTable = resTable;
        }
        public FileSaving(PathProcessing fPath, PathProcessing nPath, Notarius[] resTable)
        {
            this.fPath = fPath.FPath;
            this.nPath = nPath.NPath;
            _resTable = resTable;
        }
        /// <summary>
        /// This method writes data to the initial file.
        /// </summary>
        /// <param name="columnNames"></param>
        public void WriteToThis(string[] columnNames)
        {
            //No checking because fPath has been already checked.
            for (int i = 0; i < _resTable.Length; i++)
            {
                // Rewriting the file with the first row of the table.
                if (i == 0)
                {
                    for (int j = 0; j < columnNames.Length; j++)
                    {
                        // Writing formatted column names.
                        if (j == 0)
                            File.WriteAllText(fPath, columnNames[j] + ',', System.Text.Encoding.Unicode);
                        else if (j != columnNames.Length - 1)
                            File.AppendAllText(fPath, columnNames[j] + ',', System.Text.Encoding.Unicode);
                        else
                            File.AppendAllText(fPath, columnNames[j] + '\n', System.Text.Encoding.Unicode);
                    }
                }
                // The rest of rows appending to the first one.
                else
                {
                    // Writing table and appending \n to every row except the last one.
                    if (i == _resTable.Length - 1)
                        File.AppendAllText(fPath, _resTable[i].ToString(), System.Text.Encoding.Unicode);
                    else
                        File.AppendAllText(fPath, _resTable[i].ToString() + '\n', System.Text.Encoding.Unicode);
                }
            }
        }
        /// <summary>
        /// This method writes data to a new file.
        /// </summary>
        /// <param name="columnNames"></param>
        public void WriteToNew(string[] columnNames)
        {
            for (int i = 0; i < _resTable.Length; i++)
            {
                // Rewriting the file with the first row of the table.
                if (i == 0)
                {
                    for (int j = 0; j < columnNames.Length; j++)
                    {
                        if (j == 0)
                            File.WriteAllText(nPath, columnNames[j] + ',', System.Text.Encoding.Unicode);
                        else if (j != columnNames.Length - 1)
                            File.AppendAllText(nPath, columnNames[j] + ',', System.Text.Encoding.Unicode);
                        else
                            File.AppendAllText(nPath, columnNames[j] + '\n', System.Text.Encoding.Unicode);
                    }
                }
                // The rest of rows appending to the firs one.
                else
                {
                    if (i == _resTable.Length - 1)
                        File.AppendAllText(nPath, _resTable[i].ToString(), System.Text.Encoding.Unicode);
                    else
                        File.AppendAllText(nPath, _resTable[i].ToString() + '\n', System.Text.Encoding.Unicode);
                }
            }
        }
        /// <summary>
        /// This method appends data to the initial file.
        /// </summary>
        /// <param name="columnNames"></param>
        public void Append(string[] columnNames)
        {
            //No checking because fPath has been already checked.
            for (int i = 0; i < _resTable.Length; i++)
            {
                // Appending values to the file's data.
                if (i == _resTable.Length - 1)
                    File.AppendAllText(fPath, _resTable[i].ToString(), System.Text.Encoding.Unicode);
                else
                    File.AppendAllText(fPath, _resTable[i] + '\n'.ToString(), System.Text.Encoding.Unicode);
            }
        }
    }
}
