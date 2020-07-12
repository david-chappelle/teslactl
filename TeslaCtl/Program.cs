using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Mono.Options;
using Newtonsoft.Json;
using TeslaLib;
using TeslaLib.TeslaModels;

namespace TeslaCtl
{
	public delegate Task<CommandResponse> StandardCommand();

	class Program
	{
		static async Task<int> Main(string[] args)
		{
			var showHelp = false;
			string email = null;
			string password = null;
			string accessToken = null;
			string vehicleId = null;
			string temps = null;
			string chargeLimitRequested = null;
			List<string> extraArgs = null;
			bool resetRequested = false;
			bool wakeRequested = false;
			bool vehicleDataRequested = false;
			bool driveStateRequested = false;
			bool? lockRequested = null;
			bool honkRequested = false;
			bool flashRequested = false;
			string hvacSetting = null;
			bool remoteStartRequested = false;
			string chargePortStateRequested = null;
			string chargeStateRequested = null;
			bool getChargeStateRequested = false;
			bool getClimateStateRequested = false;
			bool guiSettingsRequested = false;
			bool getServiceDataRequested = false;
			string sentryModeRequested = null;

			var os = new OptionSet()
				{
					// commands
					{ "w|wake", "Wake up the car", v => wakeRequested = v != null },
					{ "u|unlockdoors", "Unlock the doors", v => { if (v != null) lockRequested = false; } },
					{ "l|lockdoors", "Lock the doors", v => { if (v != null) lockRequested = true; } },
					{ "honk", "Honk the horn", v => honkRequested = v != null },
					{ "f|flash", "Flash the lights", v => flashRequested = v != null },
					{ "hvac=", "HVAC (on/off)", v => hvacSetting = v },
					{ "t|temp=", "Set HVAC temperature (driver,passenger)", v => temps = v },
					{ "chargelimit=", "Set charge limit (%/max/standard)", v => chargeLimitRequested = v },
					{ "remotestartdrive", "Start vehicle keyless drive mode", v => remoteStartRequested = v != null },
					{ "chargeport=", "Set charge port state (open/closed)", v => chargePortStateRequested = v },
					{ "charge=", "Set charging state (on/off)", v => chargeStateRequested = v },
					{ "sentrymode=", "Set sentry mode (on/off)", v => sentryModeRequested = v },

					// state and settings
					{ "d|vehicledata", "Retrieve vehicle data", v => vehicleDataRequested = v != null },
					{ "cs|chargestate", "Retrieve vehicle charging state", v => getChargeStateRequested = v != null },
					{ "cls|climatestate", "Retrieve climate state", v => getClimateStateRequested = v != null },
					{ "ds|drivestate", "Retrieve vehicle drive state", v => driveStateRequested = v != null },
					{ "gui|guisettings", "Retrieve GUI setttings", v => guiSettingsRequested = v != null },
					{ "servicedata", "Retrieve service data", v => getServiceDataRequested = v != null },

					// administrative
					{ "e|email=", "Tesla account email", v => email = v },
					{ "p|password=", "Tesla account password", v => password = v },
					{ "a|accesstoken=", "Access token", v => accessToken = v },
					{ "vid|vehicleid=", "Vehicle ID", v => vehicleId = v },
					{ "r|reset", "Remove default token and vehicle", v => resetRequested = v != null },
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
				Console.WriteLine("Usage: teslactl [OPTIONS]");
				Console.WriteLine("Control a Tesla car.");
				Console.WriteLine();
				Console.WriteLine("Options:");
				os.WriteOptionDescriptions(Console.Out);

				return 1;
			}

			if (resetRequested)
				DefaultSettings.RemoveFile();

			readConfig();

			_defaultSettings = DefaultSettings.ReadFromFile();

			bool needToUpdateDefaults = false;

			if (vehicleId != null)
			{
				_defaultSettings.VehicleId = vehicleId;
				needToUpdateDefaults = true;
			}

			if (accessToken != null)
			{
				_defaultSettings.AccessToken = accessToken;
				needToUpdateDefaults = true;
			}

			var client = new TeslaClient()
			{
				BaseUri = new Uri(_baseUri)
			};

			if (_defaultSettings.AccessToken != null)
				client.AccessToken = _defaultSettings.AccessToken;

			_serializer = new JsonSerializer();
			_serializer.Formatting = Formatting.Indented;

			// check for user requested auth
			// if auth requested, reauthorize even if we have an existing token
			if (extraArgs.Contains("auth"))
			{
				if (email == null || password == null)
				{
					Console.Error.WriteLine("auth command requires Tesla username and password");
					return 1;
				}

				var authResponse = await client.Authenticate(_clientId, _clientSecret, email, password);
				printResults("Authenticate", authResponse);

				_defaultSettings.AccessToken = authResponse.AccessToken;
				needToUpdateDefaults = true;
			}

			if (_defaultSettings.AccessToken == null)
			{
				// if still no access token, nothing we can do
				return 1;
			}

			client.AccessToken = _defaultSettings.AccessToken;

			var userRequestedListVehicles = extraArgs.Contains("list-vehicles");
			if (extraArgs.Contains("list-vehicles") || _defaultSettings.VehicleId == null)
			{
				var vehicles = await client.QueryVehicles();
				printResults("QueryVehicles", vehicles);

				if (_defaultSettings.VehicleId == null && vehicles.Length > 0)
				{
					_defaultSettings.VehicleId = vehicles[0].Id.ToString();
					needToUpdateDefaults = true;
				}
			}

			if (needToUpdateDefaults)
				_defaultSettings.UpdateFile();

			client.VehicleId = long.Parse(_defaultSettings.VehicleId);

			if (wakeRequested)
			{
				var response = await client.Wake();
				printResults("wake", response);
			}

			if (honkRequested)
				await execStandardCommand(client.Honk, "honk_horn");

			if (vehicleDataRequested)
			{
				var vehicleData = await client.QueryVehicleData();
				printResults("vehicle-data", vehicleData);
			}

			if (driveStateRequested)
			{
				var driveState = await client.QueryDriveState();
				printResults("drive-state", driveState);
			}

			if (temps != null)
			{
				int tempDriver;
				int tempPassenger;
				var tempSides = temps.Split(',');
				if (tempSides.Length > 0)
				{
					tempDriver = int.Parse(tempSides[0]);
					tempPassenger = (tempSides.Length > 1) ? int.Parse(tempSides[1]) : tempDriver;

					var response = await client.SetTemperatureF(tempDriver, tempPassenger);
					printResults("set-temps", response);
				}
			}

			if (chargeLimitRequested != null)
			{
				CommandResponse response = null;

				if (chargeLimitRequested.ToLower() == "standard")
					await execStandardCommand(client.SetStandardRangeChargeLimit, "charge_standard");
				else if (chargeLimitRequested.ToLower() == "max")
					await execStandardCommand(client.SetMaxRangeChargeLimit, "charge_max_range");
				else
				{
					if (int.TryParse(chargeLimitRequested, out var chargePercent))
					{
						response = await client.SetChargeLimit(chargePercent);
						printResults("set_charge_limit", response);
					}
				}
			}

			if (lockRequested.HasValue)
			{
				var response = await client.LockDoors(lockRequested.Value);
				printResults("door_lock", response);
			}

			if (flashRequested)
				await execStandardCommand(client.FlashLights, "flash_lights");

			if (hvacSetting != null)
			{
				bool? setting = null;
				if (hvacSetting.ToLower() == "on" || hvacSetting == "1")
					setting = true;
				else if (hvacSetting.ToLower() == "off" || hvacSetting == "0")
					setting = false;

				if (setting.HasValue)
				{
					var response = await client.Hvac(setting.Value);
					printResults("auto_conditioning", response);
				}
				else
					Console.Error.WriteLine("auto_conditioning: specify \"on\" or \"off\"");
			}

			if (remoteStartRequested)
			{
				if (password != null)
				{
					CommandResponse r = await client.RemoteStart(password);
					printResults("remote_start_drive", r);
				}
				else
					Console.Error.WriteLine("remote_start_drive: password must be supplied");
			}

			if (chargePortStateRequested != null)
			{
				CommandResponse response = null;

				if (chargePortStateRequested.ToLower() == "open" || chargePortStateRequested == "1")
					response = await client.ChargePort(true);
				else if (chargePortStateRequested.ToLower() == "closed" || chargePortStateRequested == "0")
					response = await client.ChargePort(false);
				else
					Console.Error.WriteLine("chargeportstate must be \"open\" or \"closed\"");

				if (response != null)
					printResults("chargeport", response);
			}

			if (chargeStateRequested != null)
			{
				if (chargeStateRequested.ToLower() == "start" || chargeStateRequested == "1")
					await execStandardCommand(client.StartCharging, "charge_start");
				else if (chargeStateRequested.ToLower() == "stop" || chargeStateRequested == "0")
					await execStandardCommand(client.StopCharging, "charge_stop");
				else
					Console.Error.WriteLine("charge setting must be \"start\" or \"stop\"");
			}

			if (getChargeStateRequested)
			{
				var response = await client.QueryChargeState();
				printResults("charge-state-data", response);
			}

			if (getClimateStateRequested)
			{
				var response = await client.QueryClimateState();
				printResults("climate-state", response);
			}

			if (guiSettingsRequested)
			{
				var response = await client.QueryGuiSettings();
				printResults("gui-settings", response);
			}

			if (getServiceDataRequested)
			{
				var response = await client.QueryVehicleServiceData();
				printResults("service_data", response);
			}

			if (sentryModeRequested != null)
			{
				var sentryOn = parseBoolean(sentryModeRequested);
				if (sentryOn.HasValue)
				{
					var response = await client.SentryMode(sentryOn.Value);
					printResults("set_sentry_mode", response);
				}
				else
					Console.Error.WriteLine("sentry mode must be \"on\" or \"off\"");
			}

			return 0;
		}

		static void readConfig()
		{
			var cfg = new ConfigurationBuilder()
				.AddJsonFile("config.json")
				.Build();

			_clientId = cfg["Encryption:ClientId"];
			_clientSecret = cfg["Encryption:ClientSecret"];
			_baseUri = cfg["BaseUri"];
		}

		static async Task execStandardCommand(StandardCommand cmd, string title)
		{
			var response = await cmd();
			printResults(title, response);
		}

		static void printResults(string title, object results)
		{
			Console.Out.WriteLine($"*** [{title}] ***");
			Console.Out.WriteLine();
			_serializer.Serialize(Console.Out, results);
			Console.Out.WriteLine();
		}

		static bool? parseBoolean(string opt)
		{
			switch (opt.ToLower())
			{
				case "on":
				case "1":
				case "true":
					return true;

				case "off":
				case "0":
				case "false":
					return false;
			}

			return null;
		}

		static string _clientId = null;
		static string _clientSecret = null;
		static string _baseUri = null;
		static JsonSerializer _serializer = null;
		static DefaultSettings _defaultSettings = null;
	}
}
