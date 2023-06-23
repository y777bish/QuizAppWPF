using QuizOstateczny.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace QuizOstateczny
{
    public class SzyfrowanieConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Sprawdź, czy wartość jest typu string
            if (value is string tresc)
            {
                // Wywołaj funkcję Szyfrowanie
                return Szyfrowanko.Odszyfrowanie(tresc, "modelmvvm");
            }

            return value; // Jeśli wartość nie jest stringiem, zwróć ją bez zmiany
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException(); // Metoda ConvertBack nie jest obsługiwana
        }
    }
}