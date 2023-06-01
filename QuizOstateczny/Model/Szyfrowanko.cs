using static System.Net.Mime.MediaTypeNames;
using System.Windows.Input;

namespace QuizOstateczny.Model
{
    class Szyfrowanko
    {
        public string Szyfrowanie(string text, string key)
        {
            string encryptedText = "";
            int textLength = text.Length;
            int keyLength = key.Length;

            for (int i = 0; i < textLength; i++)
            {
                char currentChar = text[i];
                char currentKeyChar = key[i % keyLength];

                if (char.IsLetter(currentChar))
                {
                    // Obliczanie przesunięcia dla aktualnej litery klucza
                    int keyShift = char.ToUpper(currentKeyChar) - 'A';

                    // Szyfrowanie litery
                    if (char.IsUpper(currentChar))
                    {
                        char encryptedChar = (char)(((currentChar - 'A' + keyShift) % 26) + 'A');
                        encryptedText += encryptedChar;
                    }
                    else if (char.IsLower(currentChar))
                    {
                        char encryptedChar = (char)(((currentChar - 'a' + keyShift) % 26) + 'a');
                        encryptedText += encryptedChar;
                    }
                }
                else
                {
                    // Zachowanie oryginalnego znaku, jeśli nie jest to litera
                    encryptedText += currentChar;
                }
            }

            return encryptedText;
        }

        public string Odszyfrowanie(string encryptedText, string key)
        {
            string decryptedText = "";
            int encryptedTextLength = encryptedText.Length;
            int keyLength = key.Length;

            for (int i = 0; i < encryptedTextLength; i++)
            {
                char currentChar = encryptedText[i];
                char currentKeyChar = key[i % keyLength];

                if (char.IsLetter(currentChar))
                {
                    // Obliczanie przesunięcia dla aktualnej litery klucza
                    int keyShift = char.ToUpper(currentKeyChar) - 'A';

                    // Deszyfrowanie litery
                    if (char.IsUpper(currentChar))
                    {
                        char decryptedChar = (char)(((currentChar - 'A' - keyShift + 26) % 26) + 'A');
                        decryptedText += decryptedChar;
                    }
                    else if (char.IsLower(currentChar))
                    {
                        char decryptedChar = (char)(((currentChar - 'a' - keyShift + 26) % 26) + 'a');
                        decryptedText += decryptedChar;
                    }
                }
                else
                {
                    // Zachowanie oryginalnego znaku, jeśli nie jest to litera
                    decryptedText += currentChar;
                }
            }

            return decryptedText;
        }
    }
}
