﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchacoPDFViewer
{
    public interface IExcelToPDF
    {
        void TurnToPDF(string sourceExcelFile,string targetPdfFile);
    }
}
