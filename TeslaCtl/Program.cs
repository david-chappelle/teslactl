using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Mono.Options;
using Newtonsoft.Json;
using TeslaLib;

namespace TeslaCtl
{
	class Program
	{
		static async Task Main(string[] args)
		{
			var showHelp = false;
			string email = null;
			string password = null;
			bool saveAccessToken = false;
			List<string> extraArgs = null;
			var client = new TeslaClient();

			var os = new OptionSet()
				{
					{ "e|email=", "Tesla account email", v => email = v },
					{ "p|password=", "Tesla account password", v => password = v },
					{ "a|accesstoken=", "Access token", v => client.AccessToken = v },
					{ "st|savetoken", "Save auth token to file", v => saveAccessToken = v != null },
					{ "h|help", "Show this message and exit", v => showHelp = v != null }
				};

			try
			{
				extraArgs = os.Parse(args);
			}
			catch (OptionException ex)
			{
				Console.WriteLine($"teslactl: {ex.Message}");
				Console.WriteLine("Try 'teslactl --help' for more information.");
			}

			if (showHelp)
			{
				Console.WriteLine("Usage: tesla [OPTIONS]");
				Console.WriteLine("Control a Tesla car.");
				Console.WriteLine();
				Console.WriteLine("Options:");
				os.WriteOptionDescriptions(Console.Out);
				return;
			}

			readConfig();

			var serializer = new JsonSerializer();
			serializer.Formatting = Formatting.Indented;

			// check for user requested auth
			if (extraArgs.Contains("auth"))
			{
				var authResponse = await client.Authenticate(_clientId, _clientSecret, email, password);
				serializer.Serialize(Console.Out, authResponse);

				client.AccessToken = authResponse.AccessToken;
			}

			var tokenFile = getTokenFile();

			// save the access token to a file if user requested it
			if (saveAccessToken && client.AccessToken != null)
				File.WriteAllText(tokenFile, client.AccessToken);

			// if we still have no access token, try to read one out of the token file
			if (client.AccessToken == null && File.Exists(tokenFile))
				client.AccessToken = File.ReadAllText(tokenFile);

			if (client.AccessToken == null)
			{
				// if still no access token, nothing we can do
				return;
			}

			if (extraArgs.Contains("list-vehicles"))
			{
				var vehicles = await client.QueryVehicles();
				serializer.Serialize(Console.Out, vehicles);
			}
		}

		static void readConfig()
		{
			var cfg = new ConfigurationBuilder()
				.AddJsonFile("config.json")
				.Build();

			_clientId = cfg["Encryption:ClientId"];
			_clientSecret = cfg["Encryption:ClientSecret"];
		}

		static string getTokenFile()
		{
			return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "token.txt");
		}

		static string _clientId = null;
		static string _clientSecret = null;
	}
}
