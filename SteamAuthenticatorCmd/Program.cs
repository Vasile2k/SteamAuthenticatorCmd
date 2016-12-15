using System;
using SteamAuth;
using Newtonsoft.Json;

namespace SteamAuthenticatorCmd
{
    class Program
    {
        // Future updates
        enum Action { NONE, GET_AUTH_CODE, SEE_TRADES, CONFIRM_TRADE };

        static void Main(string[] args)
        {
            int argCount = args.Length;
            string accountName = "";

            Action action = Action.NONE;

            for(int i = 0; i < argCount; i++)
            {
                if(args[i] == "-account" && i + 1 < argCount)
                {
                    accountName = args[i+1];
                }
                else if(args[i] == "-getAuthCode")
                {
                    action = Action.GET_AUTH_CODE;
                }
            }

            switch (action)
            {
                case Action.NONE:
                    Console.Write("nothing...");
                    break;
                case Action.GET_AUTH_CODE:
                    if(accountName.Length > 1)
                    {
                        Console.Write(getCodeForAccount(accountName));
                    }
                    break;
            }
        }

        static string getCodeForAccount(string accName)
        {
            string fileName = "./maFiles/" + accName + ".maFile";
            string text = "";
            string code = "NULL";
            try
            {
                text = System.IO.File.ReadAllText(fileName);
                SteamGuardAccount acc = JsonConvert.DeserializeObject<SteamGuardAccount>(text);
                code = acc.GenerateSteamGuardCode();
            }
            catch (System.Exception e)
            {
            }
            return code;
        }

    }
}
