using System;
using System.Collections.Generic;
using System.Linq;

namespace BenhartLog
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
		/// Removes a parameterless command
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		internal static bool UnregisterCommand(string command)
		{
			if (!Commands.ContainsKey(command)) return false;

			Commands.Remove(command);
			return true;
		}

		/// <summary>
		/// Removes a command with parameters
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		internal static bool UnregisterCommandWithParamters(string command)
		{
			if (!ParameteredCommands.ContainsKey(command)) return false;

			ParameteredCommands.Remove(command);
			return true;
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

		internal static string[] ReportCommands()
		{
			var list = new List<string>();

			list.AddRange(Commands.Keys.ToList());
			list.AddRange(ParameteredCommands.Keys.ToList());

			return list.ToArray();
		}
	}
}