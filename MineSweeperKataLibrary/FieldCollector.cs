using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MineSweeperKataLibrary
{
    public class FieldCollector
    {
        public String infoBlock;
        public List<Field> fields;

        public FieldCollector()
        {
            fields = new List<Field>();
        }
        public void GetFields(string path)
        {
            bool endOfStream = false;
            using (StreamReader reader = new StreamReader(path))
            {
                GetLinesUntilEnd(reader, endOfStream);
            }
            GetFields();
        }

        private void GetLinesUntilEnd(StreamReader reader, bool endOfStream)
        {
            String curLine;
            for (int i = 0; endOfStream == false; i++)
            {
                curLine = reader.ReadLine();
                endOfStream = CheckEndOfFile(curLine);
                if (endOfStream)
                    break;
                else
                    UpdateLine(i, curLine);
            }
        }

        private void UpdateLine(int i, string curLine)
        {
            if(i > 0)
                infoBlock = infoBlock + System.Environment.NewLine + curLine;
            else
                infoBlock = curLine;
        }

        private bool CheckEndOfFile(string curLine)
        {
            if (curLine.Equals("0 0"))
                return true;
            return false;
        }

        private void GetFields()
        {
            String BLANK = System.Environment.NewLine;
            String[] values = infoBlock.Split(BLANK.ToCharArray());

            for (int i = 0; i < values.Length; i += 2)
            {
                if (GetNthChar(values[i], 0) != '.' && GetNthChar(values[i], 0) != '*')
                {
                    int N = TransformCharToNum(GetNthChar(values[i], 0));
                    int M = TransformCharToNum(GetNthChar(values[i], 2));

                    fields.Add(GenerateField(N, M, values, i));
                    i += (N * 2);
                }
            }
        }

        private Field GenerateField(int N, int M, string[] values, int i)
        {
            Field field = new Field(N, M);
            for (int j = i + 2, k = 0; j <= i + (N * 2) && k < field.Lines; j += 2, k++)
            {
                field.InputGrid[k] = values[j];
            }
            return field;
        }

        private char GetNthChar(string v, int i)
        {
            return v.ToCharArray()[i];
        }

        private int TransformCharToNum(char v)
        {
            return Convert.ToInt32(v) - 48;
        }

        public void GenerateOutput(string path)
        {
            GetFields(path);
            foreach(Field field in fields)
            {
                field.GenerateOutputField();
            }

            using(StreamWriter writer = new StreamWriter("C:\\Users\\nic_l\\Desktop\\Test_Cases_Text_Files\\Output.txt"))
            {
                WriteOutputs(writer);
            }
        }

        private void WriteOutputs(StreamWriter writer)
        {
            for (int i = 0; i < fields.Count; i++)
            {
                writer.WriteLine("Field #" + (i + 1) + ":");
                foreach (String line in fields[i].OutputGrid)
                    writer.WriteLine(line);
                if (!(i + 1 >= fields.Count))
                    writer.WriteLine();
            }
        }
    }
}
