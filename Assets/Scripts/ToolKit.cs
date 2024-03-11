using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



public class ToolKit
{
    private static ToolKit instance;

    public static ToolKit Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ToolKit();
            }
            return instance;
        }
    }

    private ToolKit() { }

    public bool IsValidEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        string emailPattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
        return Regex.IsMatch(email, emailPattern);
    }

    public bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
            return false;

        // Password regex pattern
        // ^                 # start-of-string
        // (?=.*[0-9])       # a digit must occur at least once
        // (?=.*[a-z])       # a lower case letter must occur at least once
        // (?=.*[A-Z])       # an upper case letter must occur at least once
        // .{8,}             # anything, at least eight places though
        // $                 # end-of-string
        string passwordPattern = @"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z]).{8,}$";

        return Regex.IsMatch(password, passwordPattern);
    }
}

