using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace PdfiumViewer
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal delegate bool WriteBlockCallback([MarshalAs(UnmanagedType.LPStruct)] FPDF_FILEWRITE pThis, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer, int buflen);

    [StructLayout(LayoutKind.Sequential)]
    internal class FPDF_FILEWRITE
    {
        private int Version = 1;

        [MarshalAs(UnmanagedType.FunctionPtr)]
        public WriteBlockCallback WriteBlock;
    }
}
