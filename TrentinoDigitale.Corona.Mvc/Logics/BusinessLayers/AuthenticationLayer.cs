using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using TrentinoDigitale.Corona.Mvc.Logics.Entities;

namespace TrentinoDigitale.Corona.Mvc.Logics.BusinessLayers
{
    public class AuthenticationLayer
    {
        public User SignIn(string userName, string password)
        {
            //Validazione argomenti
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));

            //Cerco sul database un utente {userName}
            string sql = $"SELECT * FROM tblUsers WHERE Username LIKE '{userName}'";
            DataTable table = new DataTable(); //TODO Apro connessione, eseguo query, ecc

            User user = new User
            {
                UserName = "mario",
                PasswordHash = "12345",
                FirstName = "Mario",
                Surname = "Rossi",
                Email = "mario.rossi@icubed.it",
                IsEnabled = true, 
                IsAdministrator = true
            };

            ////Se non lo trovo, esco
            //if (table.Rows.Count == 0)
            //    return null;

            //User user = new User
            //{
            //    UserName = (string)table.Rows[0]["UserName"],
            //    PasswordHash = (string)table.Rows[0]["PasswordHash"],
            //    FirstName = (string)table.Rows[0]["FirstName"],
            //    Surname = (string)table.Rows[0]["Surname"],
            //    Email = (string)table.Rows[0]["Email"],
            //};

            //Se lo trovo, faccio encoding della pwd passata
            string encodedPwd = EncodingUtils.Encode(password);

            //Se non è uguale a quella sullo User, esco
            if (encodedPwd != user.PasswordHash)
                return null;

            //Verifico se lo user è IsEnabled
            //Se è disabilitato, esco
            if (!user.IsEnabled)
                return null;

            //Se è abilitato, ritorno l'utente
            return user;
        }

        public User GetUserByUserName(string authUserName)
        {
            var user = new User
            {
                UserName = "mario",
                PasswordHash = "12345",
                FirstName = "Mario",
                Surname = "Rossi",
                Email = "mario.rossi@icubed.it",
                IsEnabled = true,
                IsAdministrator = true
            };

            if (authUserName != user.UserName)
                return null;
            else
                return user;

            
        }
    }
}