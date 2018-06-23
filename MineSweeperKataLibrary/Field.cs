using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MineSweeperKataLibrary
{
    public class Field
    {
        public int Lines;
        public int Columns;
        public String[] InputGrid;
        public String[] OutputGrid;

        public Field(int N, int M)
        {
            Lines = N;
            Columns = M;
            InputGrid = new String[Lines];
            OutputGrid = new String[Lines];
        }

        public void TransformAllDotsIntoZeroes()
        {
            for(int i = 0; i < Lines; i++)
            {
                OutputGrid[i] = new string(ChangeInlineDotsIntoZero(GetInputCharArray(i)));
            }
            
        }

        private char[] GetInputCharArray(int i)
        {
            return InputGrid[i].ToCharArray();
        }

        private char[] GetOutputCharArray(int i)
        {
            return OutputGrid[i].ToCharArray();
        }

        private char[] ChangeInlineDotsIntoZero(char[] chars)
        {
            for (int j = 0; j < Columns; j++)
                ChangeDotIntoZero(chars, j);

            return chars;
        }
        private void ChangeDotIntoZero(char[] chars, int j)
        {
            if (chars[j] != '*')
                chars[j] = '0';
        }

        public void GenerateOutputField()
        {
            TransformAllDotsIntoZeroes();
            for (int i = 0; i < Lines; i++)
            {
                UpdateInlineMines(i);
            }
        }

        private void UpdateInlineMines(int i)
        {
            UpdateInlineMines(i, GetOutputCharArray(i));
        }

        private void UpdateInlineMines(int i, char[] chars)
        {
            for (int j = 0; j < Columns; j++)
            {
                CheckIfAsterisk(chars, i, j);
            }
        }

        private void CheckIfAsterisk(char[] chars, int i, int j)
        {
            if (chars[j] == '*')
                UpdateSurroundingValues(i, j);
        }

        private void UpdateSurroundingValues(int i, int j)
        {
            UpdateAbove(i, j);
            UpdateBelow(i, j);
            UpdateLeft(i, j);
            UpdateRight(i, j);
        }

        private void UpdateAbove(int i, int j)
        {
            if (i + 1 < Lines)
            {
                DotValuePlus1(i + 1, j);
                UpdateLeft(i + 1, j);
                UpdateRight(i + 1, j);
            }
        }

        private void UpdateBelow(int i, int j)
        {
            if (i - 1 >= 0)
            {
                DotValuePlus1(i - 1, j);
                UpdateLeft(i - 1, j);
                UpdateRight(i - 1, j);
            }
        }

        private void UpdateRight(int i, int j)
        {
            if (j + 1 < Columns)
            {
                DotValuePlus1(i, j + 1);
            }
        }

        private void UpdateLeft(int i, int j)
        {
            if (j - 1 >= 0)
            {
                DotValuePlus1(i, j - 1);
            }
        }       

        private void DotValuePlus1(int i, int j)
        {
            char[] chars = OutputGrid[i].ToCharArray();
            if(chars[j] != '*')
            {
                int value = Convert.ToInt32(chars[j]) - 48;
                value++;
                chars[j] = Convert.ToChar(value + 48);
            }

            OutputGrid[i] = new String(chars);
        }
    }
}
