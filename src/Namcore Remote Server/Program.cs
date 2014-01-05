//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//* Copyright (C) 2013-2014 NamCore Studio <https://github.com/megasus/Namcore-Studio>
//*
//* This program is free software; you can redistribute it and/or modify it
//* under the terms of the GNU General Public License as published by the
//* Free Software Foundation; either version 3 of the License, or (at your
//* option) any later version.
//*
//* This program is distributed in the hope that it will be useful, but WITHOUT
//* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
//* FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for
//* more details.
//*
//* You should have received a copy of the GNU General Public License along
//* with this program. If not, see <http://www.gnu.org/licenses/>.
//*
//* Developed by Alcanmage/megasus
//*
//* //FileInfo//
//*      /Filename:      todo
//*      /Description:   todo
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

using System;
using System.Text.RegularExpressions;

namespace Namcore_Remote_Server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Log.ServerType = "World";

            Log.Message(LogType.Init, "__________________________________________________________________");
            Log.Message();
            Log.Message(LogType.Init, "##    ##    ###    ##     ##  ######   #######  ########  ######## ");
            Log.Message(LogType.Init, "###   ##   ## ##   ###   ### ##    ## ##     ## ##     ## ##       ");
            Log.Message(LogType.Init, "####  ##  ##   ##  #### #### ##       ##     ## ##     ## ##       ");
            Log.Message(LogType.Init, "## ## ## ##     ## ## ### ## ##       ##     ## ########  ######   ");
            Log.Message(LogType.Init, "##  #### ######### ##     ## ##       ##     ## ##   ##   ##       ");
            Log.Message(LogType.Init, "##   ### ##     ## ##     ## ##    ## ##     ## ##    ##  ##       ");
            Log.Message(LogType.Init, "##    ## ##     ## ##     ##  ######   #######  ##     ## ######## ");
            Log.Message(LogType.Init, "__________________________________________________________________");
            Log.Message();

            Log.Message(LogType.Normal, "Starting NamCore Remote Server...");
            XML.RetrieveAccounts();
            var srv = new Server();
            while (true)
            {
                string str = Console.ReadLine() ?? "";

                if (str.StartsWith("/"))
                {
                    //command
                    if (str.StartsWith("/account create"))
                    {
                        int argsCount = Regex.Matches(str, Regex.Escape(" ")).Count;
                        switch (argsCount)
                        {
                            case 4:
                                //correct
                                string[] parts = (str + " ").Split(' ');
                                var newAccount = new Account { Name = parts[2], Password = parts[3], Rights = (AccountRights)Convert.ToInt32(parts[4])};
                                Globals.Accounts.Add(newAccount);
                                break;
                            default:
                                //not correct
                                Log.Message(LogType.Error, "Invalid arguments");
                                Log.Message(LogType.Error, "Usage: /account create [accountname] [password] [rank]");
                                break;
                        }
                    }

                }
                else { }
            }
        }
    }
}