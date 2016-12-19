
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at

//   http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.

// Made by Vasile2k. See LICENSE

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
