﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public interface IPrintPDF
    {
        void Print(string pdfFileName,string printer, bool IsDupex = true);
    }
}
