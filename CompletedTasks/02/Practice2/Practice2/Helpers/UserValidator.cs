using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Practice2.Helpers
{
    public class UserValidator
    {
        internal string CheckUserData(string login, string password)
        {
            try
            {
                CheckLogin(login);
                CheckPassword(password);
                return "isValid";
            }
            catch (InvalidDataException ex)
            {
                return ex.Message;
            }
        }

        private void CheckLogin(string login)
        {
            if (!Regex.IsMatch(login, "^[a-zA-Z][a-zA-Z0-9_]{3,15}$"))
            {
                throw new InvalidDataException("Incorrect entered login." +
                    "\nmust be 4-16 symbols, English language, first a letter, only letters, gigits, symbol _");
            }
        }

        private void CheckPassword(string password)
        {
            if (!Regex.IsMatch(password, "^[a-zA-Z0-9_]{4,16}$"))
            {
                throw new InvalidDataException("Incorrect entered password." +
                    "\nmust be 4-16 symbols, English language, only letters, gigits, symbol _");
            }
        }
    }
}
