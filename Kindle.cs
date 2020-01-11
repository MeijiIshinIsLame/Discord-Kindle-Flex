using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace kindleflex
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

        public void UpdateCurrentPage()
        {
            int[] offsets = {0x03888A98, 0x1C, 0x14, 0xC4, 0x148, 0x18};
            currentPage = memoryReader.ReadMultiLevelPointer(offsets);
        }

        public void UpdateMaxPage()
        {
            int[] offsets = {0x038FE68C, 0x34, 0x14, 0x4, 0x4, 0x30};
            maxPage =  memoryReader.ReadMultiLevelPointer(offsets);
        }

        public void UpdateAllValues()
        {
            UpdateCurrentPage();
            UpdateMaxPage();
        }

        public bool InMenu()
        {
            if (currentPage == 0)
                return true;
            else
                return false;
        }

        public string GetPercentComplete()
        {
            UpdateAllValues();

            //You would assume this 0.2 does nothing, and you would be correct.
            //However, for whatever reason kindle percent calculation is weird and this is the only way it lines up.
            //So here it will stay.
            double decimalPercentage = (((double)currentPage / maxPage) * 100) + 0.2;
            int intPercentage = ((int)decimalPercentage) + 1;
            return $"{intPercentage}%";
        }
    }
}