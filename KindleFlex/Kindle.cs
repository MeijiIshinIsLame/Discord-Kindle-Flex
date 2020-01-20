using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace KindleFlex
{
    public class Kindle
    {
        private MemoryReader memoryReader;
        private int currentPage;
        private int maxPage;

        public Kindle()
        {
            this.memoryReader = new MemoryReader("Kindle");
        }

        public MemoryReader GetMemoryReader()
        {
            return memoryReader;
        }

        public int GetCurrentPage()
        {
            return currentPage;
        }

        public int GetMaxPage()
        {
            return maxPage;
        }

        public void UpdateCurrentPage()
        {
            int[] offsets = { 0x03888A98, 0x1C, 0x14, 0xC4, 0x148, 0x18 };
            currentPage = memoryReader.ReadMultiLevelPointer(offsets);
        }

        public void UpdateMaxPage()
        {
            int[] offsets = { 0x038FE68C, 0x34, 0x14, 0x4, 0x4, 0x30 };
            maxPage = memoryReader.ReadMultiLevelPointer(offsets);
        }

        public void UpdateAllValues()
        {
            UpdateCurrentPage();
            UpdateMaxPage();
        }

        public bool InMenu()
        {
            //Position always starts at 1, so it either errored at -1 or is being read as 0.
            if (currentPage <= 0)
                return true;
            else
                return false;
        }

        public string GetPercentComplete()
        {
            //You would assume this 0.22 does nothing, and you would be correct.
            //However, for whatever reason kindle percent calculation is weird and this is the only way it lines up.
            //So here it will stay.
            double decimalPercentage = (((double)currentPage / maxPage) * 100) + 0.22;
            int intPercentage = ((int)decimalPercentage) + 1;

            if (intPercentage > 100) //at the very end it goes to 101
                intPercentage = 100;

            //Yep, I don't know why but percentage in kindle incriments to 1% at position 2...
            if (currentPage == 1)
                intPercentage = 1;

            Console.WriteLine(intPercentage);
            return $"{intPercentage}%";
        }

        public string GetBookTitle()
        {
            if (InMenu())
                return null;
            else
            {
                string windowTitle = memoryReader.GetProcessWindowTitle("Kindle");
               
                string bookTitle = Regex.Split(windowTitle, "Kindle for PC 3 - ")[1]; //get only book title
                return bookTitle;
            }
        }

        public string GetStatusUpdate()
        {
            if (InMenu())
            {
                return "Kindle - in main menu.";
            }
            else
            {
                string percentComplete = GetPercentComplete();
                string bookTitle = GetBookTitle();
                return $"Reading: {percentComplete} - {bookTitle}";
            }
        }
    }
}
