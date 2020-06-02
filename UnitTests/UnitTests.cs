using System;
using System.IO;
using practical_task5;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TestCheckInput1()
        {
            string[] input = { "3 5 7", "-5 9 0", "35 1000 -1 19" };
            int[,] matrix;

            bool result = false;

            Assert.AreEqual(result, Program.CheckInput(input, out matrix));
        }

        [TestMethod]
        public void TestCheckInput2()
        {
            string[] input = { "3 5 7", "-5 9 0", "35 1abc -1" };
            int[,] matrix;

            bool result = false;

            Assert.AreEqual(result, Program.CheckInput(input, out matrix));
        }

        [TestMethod]
        public void TestCheckInput3()
        {
            string[] input = { "3 5 7", "-5 9 0", "35 100 -1" };
            int[,] matrix;

            bool result = true;

            Assert.AreEqual(result, Program.CheckInput(input, out matrix));
        }

        [TestMethod]
        public void TestFindDecresing()
        {
            string[] input = { "3 5 7", "-5 9 0", "35 10 -1" };
            int[,] matrix;
            Program.CheckInput(input, out matrix);

            int result = 3;

            Assert.AreEqual(result, Program.FindDecreacaningLines(matrix)[0]);
        }

        [TestMethod]
        public void TestFindIncresing()
        {
            string[] input = { "3 5 7", "-5 9 0", "35 10 -1" };
            int[,] matrix;
            Program.CheckInput(input, out matrix);

            int result = 1;

            Assert.AreEqual(result, Program.FindIncreacaningLines(matrix)[0]);
        }

        [TestMethod]
        public void TestIntInput()
        {
            Console.SetIn(new StreamReader("input.txt"));
            double result = 2;

            double input = Program.IntInput(lBound: 0, info: "some info");

            Assert.AreEqual(result, input);
        }
    }
}
