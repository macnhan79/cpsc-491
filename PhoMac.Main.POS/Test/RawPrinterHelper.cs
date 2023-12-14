using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PhoMac.Main.POS.Test
{
    public class RawPrinterHelper
    {
        // Structure and API declarions:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct DOCINFOW
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pDataType;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool OpenPrinter(string src, ref IntPtr hPrinter, Int32 pd);
        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool ClosePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, Int32 level, ref DOCINFOW pDI);
        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, ref Int32 dwWritten);

        // SendBytesToPrinter()
        // When the function is given a printer name and an unmanaged array of  
        // bytes, the function sends those bytes to the print queue.
        // Returns True on success or False on failure.
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, Int32 dwCount)
        {
            IntPtr hPrinter = default(IntPtr);
            // The printer handle.
            Int32 dwError = default(Int32);
            // Last error - in case there was trouble.
            DOCINFOW di = default(DOCINFOW);
            // Describes your document (name, port, data type).
            Int32 dwWritten = default(Int32);
            // The number of bytes written by WritePrinter().
            bool bSuccess = false;
            // Your success code.

            // Set up the DOCINFO structure.
            var _with1 = di;
            _with1.pDocName = "My Visual Basic .NET RAW Document";
            _with1.pDataType = "RAW";
            // Assume failure unless you specifically succeed.
            bSuccess = false;
            if (OpenPrinter(szPrinterName, ref hPrinter, 0))
            {
                if (StartDocPrinter(hPrinter, 1, ref _with1))
                {
                    if (StartPagePrinter(hPrinter))
                    {
                        // Write your printer-specific bytes to the printer.
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, ref dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            // If you did not succeed, GetLastError may give more information
            // about why not.
            if (bSuccess == false)
            {
                dwError = Marshal.GetLastWin32Error();
            }
            return bSuccess;
        }
        // SendBytesToPrinter()

        // SendFileToPrinter()
        // When the function is given a file name and a printer name, 
        // the function reads the contents of the file and sends the
        // contents to the printer.
        // Presumes that the file contains printer-ready data.
        // Shows how to use the SendBytesToPrinter function.
        // Returns True on success or False on failure.
        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            // Open the file.
            using (FileStream fs = new FileStream(szFileName, FileMode.Open))
            {
                // Create a BinaryReader on the file.
                BinaryReader br = new BinaryReader(fs);
                // Dim an array of bytes large enough to hold the file's contents.
                byte[] bytes = new byte[fs.Length + 1];
                bool bSuccess = false;
                // Your unmanaged pointer.
                IntPtr pUnmanagedBytes = new IntPtr(0);
                int nLength = Convert.ToInt32(fs.Length);
                // Read the contents of the file into the array.
                bytes = br.ReadBytes(nLength);
                // Allocate some unmanaged memory for those bytes.
                pUnmanagedBytes = Marshal.AllocCoTaskMem(nLength);
                // Copy the managed byte array into the unmanaged array.
                Marshal.Copy(bytes, 0, pUnmanagedBytes, nLength);
                // Send the unmanaged bytes to the printer.
                bSuccess = SendBytesToPrinter(szPrinterName, pUnmanagedBytes, nLength);
                // Free the unmanaged memory that you allocated earlier.
                Marshal.FreeCoTaskMem(pUnmanagedBytes);
                return bSuccess;
            }
        }
        // SendFileToPrinter()

        // When the function is given a string and a printer name,
        // the function sends the string to the printer as raw bytes.
        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            IntPtr pBytes = default(IntPtr);
            Int32 dwCount = default(Int32);
            // How many characters are in the string?
            dwCount = szString.Length;
            // Assume that the printer is expecting ANSI text, and then convert
            // the string to ANSI text.
            pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            // Send the converted ANSI string to the printer.
            SendBytesToPrinter(szPrinterName, pBytes, dwCount);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

    }

    public static class CashDrawer
    {
        public static void OpenCashDrawer(string printerName)
        {
            RawPrinterHelper.SendStringToPrinter(printerName, Globals.drawerCode);
        }
    }
    public static class Globals
    {
        public static string fullPaperCutCode = Convert.ToChar(27).ToString() + Convert.ToChar(109).ToString();
        public static string drawerCode = Convert.ToChar(27).ToString() + Convert.ToChar(112).ToString() + Convert.ToChar(48).ToString() + Convert.ToChar(55).ToString() + Convert.ToChar(121).ToString();
        public static string partialPaperCutCode = Convert.ToChar(27).ToString() + Convert.ToChar(105).ToString();
    }
}
