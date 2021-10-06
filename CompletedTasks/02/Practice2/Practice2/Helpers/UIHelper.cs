using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Practice2.Models;

namespace Practice2.Helpers
{
    public class UIHelper
    {
        private string _host = string.Empty;
        public string GetuserAccountPage(User user)
        {
            return $"<h1>Hello, {user.Login}<br>" +
                $"Welcome to user account<br></h1>" +
                $"<h3>Your password: {user.Password}<br>" +
                $"Your id: {user.Id}<br></h3>";
        }
        
        internal string GetUsersListHtml(List<User> users, string host)
        {
            _host = host;
            StringBuilder list = new StringBuilder();
            list.Append("<ul>");
            for (int i = 0; i < users.Count; i++)
            {
                string detailsButton = $"<a href=\"http://{host}" +
                    $"/userAccount?login={users[i].Login}\">" +
                    $"{CreateButton("Go to user account")}" +
                    $"</a>";

                list.Append($"<li style=\"margin: 20px; font-size: 40px\">" +
                    $"<pre>{users[i].Login}  " +
                    $"{detailsButton}  " +
                    $"<span style=\"color: green; font-size: 16px\">{users[i].Label}</span>" +
                    $"</pre></li>");
            }
            list.Append("</ul><hr><br>");
            return list.ToString();
        }

        public string GetUserRegistrationButton()
        {
            return $"<a href=\"http://{_host}/registration\">{CreateButton("New user registration")}</a>";
        }

        private string CreateButton(string text)
        {
            return $"<button style=\"font-size: 20px; padding: 10px\">{text}</button>";
        }

        public string GetRegistrationPage()
        {
            return $"<form method='post' style='padding: 10px;'>" +
                $"<input name='login' placeholder='Enter login' style='{GetInputStyles()}'>" +
                $"<input name='password' placeholder='Enter password' style='{GetInputStyles()}'>" +
                $"{CreateButton("Send")}" +
                $"</form>";
        }

        private string GetInputStyles()
        {
            return "display: block; margin: 10px 0; padding: 10px";
        }
    }
}
