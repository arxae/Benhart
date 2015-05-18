using System;
using System.Collections.Generic;

namespace BenhartLog.Windows.Pages
{
	internal class CommandManager
	{
		// Dictionary for commands witouth parameters
		private static Dictionary<string, dynamic> _cmdsInstance;
		private static Dictionary<string, dynamic> Commands => _cmdsInstance ?? (_cmdsInstance = new Dictionary<string, dynamic>());

		// Dictionary for commands with parameters
		private static Dictionary<string, dynamic> _cmdsParamteredInstance;
		private static Dictionary<string, dynamic> ParameteredCommands => _cmdsParamteredInstance ?? (_cmdsParamteredInstance = new Dictionary<string, dynamic>());

		/// <summary>
		/// Registers a command witouth parameters
		/// </summary>
		/// <param name="command">The command that has to be inputted into the console for it to be called</param>
		/// <param name="commandAction">An action that gets executed when the command is ran</param>
		internal static void RegisterCommand(string command, Action commandAction)
		{
			Commands.Add(command, commandAction);
		}

		/// <summary>
		/// Registers a command with parameters
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="command">The command that has to be inputted into the console for it to be called</param>
		/// <param name="commandAction">An action that gets executed when the command is ran</param>
		internal static void RegisterCommandWithParameters<T>(string command, Action<T> commandAction)
		{
			ParameteredCommands.Add(command, commandAction);
		}

		/// <summary>
		/// Runs a command
		/// </summary>
		/// <param name="command"></param>
		/// <param name="parameters"></param>
		/// <returns>A return code. 0 is all fine, -1 is command not found</returns>
		internal static int RunCommand(string command, string parameters = "")
		{
			try
			{
				if (string.IsNullOrWhiteSpace(parameters))
				{
					Commands[command]();
				}
				else
				{
					ParameteredCommands[command](parameters);
				}
				return 0;
			}
			catch (KeyNotFoundException)
			{
				return -1;
			}
		}
	}
}