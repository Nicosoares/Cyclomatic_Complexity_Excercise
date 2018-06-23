using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeperKataLibrary;
using System.Collections.Generic;
using System.IO;

namespace MineSweeperKataTestUnit
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SaveFieldLines()
        {
            Field result = new Field(5, 5);
            int expected = 5;
            Assert.AreEqual(expected, result.Lines);
        }

        [TestMethod]
        public void SaveFieldColumns()
        {
            Field result = new Field(5, 5);
            int expected = 5;
            Assert.AreEqual(expected, result.Columns);
        }

        [TestMethod]
        public void GenerateInputField()
        {
            Field result = new Field(4, 4);
            String[] expected = new string[4];
            Assert.AreEqual(expected.Length, result.InputGrid.Length);
        }

        [TestMethod]
        public void SaveFieldvalue()
        {
            Field result = new Field(4, 4);
            result.InputGrid = new String[4] { "....", "....", "....", "...." };
            String[] expected = new String[4] { "....", "....", "....", "...." };
            CollectionAssert.AreEqual(expected, result.InputGrid);
        }

        [TestMethod]
        public void Transform1DotIntoZero()
        {
            Field result = new Field(1, 1);
            result.InputGrid = new String[1] { "." };
            String[] expected = new String[1] { "0" };
            result.TransformAllDotsIntoZeroes();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void Transform2InlineDotsIntoZeroes()
        {
            Field result = new Field(1, 2);
            result.InputGrid = new String[1] { ".." };
            String[] expected = new String[1] { "00" };
            result.TransformAllDotsIntoZeroes();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void Transform2SeparateDotsIntoZeroes()
        {
            Field result = new Field(2, 1);
            result.InputGrid = new String[2] { ".", "." };
            String[] expected = new String[2] { "0", "0" };
            result.TransformAllDotsIntoZeroes();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void Transform4DotsInTotalIntoZeroes()
        {
            Field result = new Field(2, 2);
            result.InputGrid = new String[2] { "..", ".." };
            String[] expected = new String[2] { "00", "00" };
            result.TransformAllDotsIntoZeroes();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void DoNotTransform1AsteriskIntoZeroe()
        {
            Field result = new Field(1, 1);
            result.InputGrid = new String[1] { "*" };
            String[] expected = new String[1] { "*" };
            result.TransformAllDotsIntoZeroes();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void OnlyTransformDotsIntoZeroes()
        {
            Field result = new Field(2, 2);
            result.InputGrid = new String[2] { ".*", "*." };
            String[] expected = new String[2] { "0*", "*0" };
            result.TransformAllDotsIntoZeroes();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void GenerateSingleMineOutput()
        {
            Field result = new Field(1, 2);
            result.InputGrid = new String[1] { ".*" };
            String[] expected = new String[1] { "1*" };
            result.GenerateOutputField();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void UpdateSurroundingTiles()
        {
            Field result = new Field(3, 3);
            result.InputGrid = new String[3] { "...", ".*.", "..." };
            String[] expected = new String[3] { "111", "1*1", "111" };
            result.GenerateOutputField();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void Update1TileTwice()
        {
            Field result = new Field(1, 3);
            result.InputGrid = new String[1] { "*.*"};
            String[] expected = new String[1] { "*2*"};
            result.GenerateOutputField();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void AcceptanceTestPart1()
        {
            Field result = new Field(4, 4);
            result.InputGrid = new String[4] { "*...", "....", ".*..", "...." };
            String[] expected = new String[4] { "*100", "2210", "1*10", "1110" };
            result.GenerateOutputField();
            CollectionAssert.AreEqual(expected, result.OutputGrid);
        }

        [TestMethod]
        public void ReadEntireTextBlock()
        {
            FieldCollector collector = new FieldCollector();
            String BLANK = System.Environment.NewLine;
            String path = "C:\\Users\\nic_l\\Desktop\\Test_Cases_Text_Files\\ReadEntireTextBlock.txt";
            String expected = "4 4" + BLANK + "...." + BLANK + "...." + BLANK + "...." + BLANK + "....";
            collector.GetFields(path);
            Assert.AreEqual(expected, collector.infoBlock);
        }

        [TestMethod]
        public void readEntireTextBLockUntil0_0()
        {
            FieldCollector collector = new FieldCollector();
            String BLANK = System.Environment.NewLine;
            String path = "C:\\Users\\nic_l\\Desktop\\Test_Cases_Text_Files\\ReadEntireTextBlockUntil0_0.txt";
            String expected = "4 4" + BLANK + "...." + BLANK + "...." + BLANK + "...." + BLANK + "....";
            collector.GetFields(path);
            Assert.AreEqual(expected, collector.infoBlock);
        }

        [TestMethod]
        public void Generate1FieldFromText()
        {
            FieldCollector collector = new FieldCollector();
            String path = "C:\\Users\\nic_l\\Desktop\\Test_Cases_Text_Files\\Generate1FieldFromText.txt";
            Field expected = new Field(4, 4);
            expected.InputGrid = new String[4] { "*...", "....", ".*..", "...." };
            collector.GetFields(path);
            CollectionAssert.AreEqual(expected.InputGrid, collector.fields[0].InputGrid);
        }

        [TestMethod]
        public void Generate2FieldsFromText()
        {
            FieldCollector collector = new FieldCollector();
            String path = "C:\\Users\\nic_l\\Desktop\\Test_Cases_Text_Files\\Generate2FieldFromText.txt";

            Field field1 = new Field(4, 4);
            field1.InputGrid = new String[4] { "*...", "....", ".*..", "...." };

            Field field2 = new Field(3, 5);
            field2.InputGrid = new String[3] { "**...", ".....", ".*..."};
            
            List<Field> expected = new List<Field>();
            expected.Add(field1);
            expected.Add(field2);
            collector.GetFields(path);

            CollectionAssert.AreEqual(expected[1].InputGrid, collector.fields[1].InputGrid);
        }

        [TestMethod]
        public void Generate1OutputFieldFromText()
        {
            FieldCollector collector = new FieldCollector();
            String path = "C:\\Users\\nic_l\\Desktop\\Test_Cases_Text_Files\\Generate2FieldFromText.txt";

            Field expected = new Field(4, 4);
            expected.OutputGrid = new String[4] { "*100", "2210", "1*10", "1110" };
            
            collector.GenerateOutput(path);

            CollectionAssert.AreEqual(expected.OutputGrid, collector.fields[0].OutputGrid);
        }

        [TestMethod]
        public void ConfirmGeneratedOutputFromFile()
        {
            FieldCollector collector = new FieldCollector();
            String BLANK = System.Environment.NewLine;
            String InputPath = "C:\\Users\\nic_l\\Desktop\\Test_Cases_Text_Files\\Generate2FieldFromText.txt";
            String OutputPath = "C:\\Users\\nic_l\\Desktop\\Test_Cases_Text_Files\\Output.txt";
            
            String expected = "Field #1:" + BLANK + "*100" + BLANK + "2210" + BLANK + "1*10" + BLANK + "1110" + BLANK + BLANK +
                                "Field #2:" + BLANK + "**100" + BLANK + "33200" + BLANK + "1*100" + BLANK;

            collector.GenerateOutput(InputPath);

            String result;
            using (StreamReader reader = new StreamReader(OutputPath))
            {
                result = reader.ReadToEnd();
            }
            Assert.AreEqual(expected, result);
        }
    }
}
