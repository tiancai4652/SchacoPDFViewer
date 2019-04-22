using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SchacoPDFViewer
{
    public class TreeTypeToImageSource : IValueConverter
    {
        ImageSource _excelImage;
        private ImageSource excelImage
        {
            get
            {
                if (_excelImage == null)
                {
                    _excelImage = new BitmapImage(new Uri("/Resource/excel.png", UriKind.Relative));
                }
                return _excelImage;
            }
        }

        ImageSource _pdfImage;
        private ImageSource pdfImage
        {
            get
            {
                if (_pdfImage == null)
                {
                    _pdfImage = new BitmapImage(new Uri("/Resource/pdf.png", UriKind.Relative));
                }
                return _pdfImage;
            }
        }

        ImageSource _folderImage;
        private ImageSource folderImage
        {
            get
            {
                if (_folderImage == null)
                {
                    _folderImage = new BitmapImage(new Uri("/Resource/folder.png", UriKind.Relative));
                }
                return _folderImage;
            }
        }

        ImageSource _otherImage;
        private ImageSource otherImage
        {
            get
            {
                if (_otherImage == null)
                {
                    _otherImage = new BitmapImage(new Uri("/Resource/other.png", UriKind.Relative));
                }
                return _otherImage;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TreeType)
            {
                if ((TreeType)value == TreeType.ExcelFlie)
                {
                    return excelImage;
                }
                else if ((TreeType)value == TreeType.Pdf)
                {
                    return pdfImage;
                }
                else if ((TreeType)value == TreeType.Folder)
                {
                    return folderImage;
                }
                else
                {
                    return otherImage;
                }
            }
            else
            {
                return otherImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
