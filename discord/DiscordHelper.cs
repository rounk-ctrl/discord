using fastJSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace discord
{
	internal class DiscordHelper
	{
		public static string token;
		public static List<Tuple<string, string>> dataSet;
		public static string id;
		private static readonly Uri _baseUri = new Uri("https://discord.com/api/v9/", UriKind.Absolute);
		public static async Task<HttpResponseMessage> GetResponseAsync(string url, CancellationToken cancellationToken)
		{
			var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_baseUri, url));
			request.Headers.TryAddWithoutValidation("Authorization", token);
			HttpClient client = new HttpClient();
			return await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
		}
		public static string GetAvatarUrl(string id, string avatarHash, int size)
		{
			var extension = avatarHash.StartsWith("a_", StringComparison.Ordinal) ? "gif" : "png";
			return "https://cdn.discordapp.com/avatars/" + id + "/" + avatarHash + "." + extension + "?size=" + size.ToString();
		}
		public static string GetServerUrl(string id, string iconHash)
		{
			return "https://cdn.discordapp.com/icons/" + id + "/" + iconHash + ".png?size=48";
		}
		public static string GetGroupUrl(string id, string iconHash)
		{
			return "https://cdn.discordapp.com/channel-icons/" + id + "/" + iconHash + ".png?size=24";
		}
		public static string GetDefaultAvatarUrl(string discriminator)
		{
			Int32.TryParse(discriminator, out int num);
			return "https://cdn.discordapp.com/embed/avatars/" + (num % 5).ToString() + ".png";
		}
		public static string GetDefaultIconUrl()
		{
			return "https://cdn.discordapp.com/embed/avatars/0.png";
		}

		public static async Task<User> GetUserInfo()
		{
			var userResponse = await GetResponseAsync("users/@me", CancellationToken.None);
			User dynuserObj = JSON.ToObject<User>(userResponse.Content.ReadAsStringAsync().Result);
			return dynuserObj;
		}
		public static async Task<Server[]> GetServers()
		{
			var serversResponse = await DiscordHelper.GetResponseAsync("users/@me/guilds", CancellationToken.None);
			Server[] dynservObj = JSON.ToObject<Server[]>(serversResponse.Content.ReadAsStringAsync().Result);
			return dynservObj;
		}
		public static async Task<DM[]> GetDMs()
		{
			var channelsResponse = await DiscordHelper.GetResponseAsync("users/@me/channels", CancellationToken.None);
			DM[] dynObj = JSON.ToObject<DM[]>(channelsResponse.Content.ReadAsStringAsync().Result);
			return dynObj;
		}
		public static List<string> GetServerNames(Server[] server)
		{
			List<string> names = new List<string>();
			foreach (var srv in server)
			{
				names.Add(srv.name);
			}

			return names;
		}

		public static List<string> GetServerIDs(Server[] server)
		{
			List<string> names = new List<string>();
			foreach (var srv in server)
			{
				names.Add(srv.id.ToString());
			}

			return names;
		}
		public static List<string> GetDMIds(DM[] dm)
		{
			List<string> names = new List<string>();
			foreach (var chan in dm)
			{
				names.Add(chan.id.ToString());
			}

			return names;
		}
		public static List<string> GetDMNames(DM[] dm)
		{
			List<string> dms = new List<string>();
			foreach (var data in dm)
			{
				foreach (var data1 in data.recipients)
				{
					if (data.type == 1)
					{
						dms.Add(data1.username);
					}
				}

				if ((data.type == 3) && !string.IsNullOrWhiteSpace(data.name))
				{
					dms.Add(data.name);
				}
				else if ((data.type == 3) && string.IsNullOrWhiteSpace(data.name))
				{
					string name = "";
					foreach (var data1 in data.recipients)
					{
						name += data1.username + ", ";
					}
					dms.Add(name);
				}
			}
			return dms;
		}
		public static ImageList GetDMIcons(DM[] dm)
		{
			ImageList dms = new ImageList
			{
				ImageSize = new Size(32, 32),
				ColorDepth = ColorDepth.Depth32Bit
			};
			string temp = Path.GetTempPath();
			string dsfolder = Path.Combine(temp, "DickSword");
			string dsfolderDm = Path.Combine(dsfolder, "dm");
			if (!Directory.Exists(dsfolder))
			{
				Directory.CreateDirectory(dsfolder);
			}
			if (!Directory.Exists(dsfolderDm))
			{
				Directory.CreateDirectory(dsfolderDm);
			}
			foreach (var data in dm)
			{
				string avatarUrl;
				foreach (var data1 in data.recipients)
				{
					if (data.type == 1)
					{
						try
						{
							if (!File.Exists(Path.Combine(dsfolderDm, data1.id.ToString() + ".png")))
							{
								avatarUrl = DiscordHelper.GetAvatarUrl(data1.id.ToString(), data1.avatar.ToString(), 32);
								WebClient wc = new WebClient();
								byte[] bytes = wc.DownloadData(avatarUrl);
								MemoryStream ms = new MemoryStream(bytes);
								System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
								img.Save(Path.Combine(dsfolderDm, data1.id.ToString() + ".png"));
								ms.Dispose();
								dms.Images.Add(img);
							}
							else
							{
								dms.Images.Add(System.Drawing.Image.FromFile(Path.Combine(dsfolderDm, data1.id.ToString() + ".png")));
							}
						}
						catch
						{
							dms.Images.Add(Properties.Resources._default);
						}
					}
				}
				if ((data.type == 3) && !string.IsNullOrWhiteSpace(data.name))
				{
					try
					{
						if (!File.Exists(Path.Combine(dsfolderDm, data.id.ToString() + ".png")))
						{
							avatarUrl = DiscordHelper.GetGroupUrl(data.id.ToString(), data.icon.ToString());
							WebClient wc = new WebClient();
							byte[] bytes = wc.DownloadData(avatarUrl);
							MemoryStream ms = new MemoryStream(bytes);
							System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
							img.Save(Path.Combine(dsfolderDm, data.id.ToString() + ".png"));
							ms.Dispose();
							dms.Images.Add(img);
						}
						else
						{
							dms.Images.Add(System.Drawing.Image.FromFile(Path.Combine(dsfolderDm, data.id.ToString() + ".png")));
						}
					}
					catch
					{
						dms.Images.Add(Properties.Resources._default);
					}
				}
				else if ((data.type == 3) && string.IsNullOrWhiteSpace(data.name))
				{
					try
					{
						if (!File.Exists(Path.Combine(dsfolderDm, data.id.ToString() + ".png")))
						{
							avatarUrl = DiscordHelper.GetGroupUrl(data.id.ToString(), data.icon.ToString());
							WebClient wc = new WebClient();
							byte[] bytes = wc.DownloadData(avatarUrl);
							MemoryStream ms = new MemoryStream(bytes);
							System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
							img.Save(Path.Combine(dsfolderDm, data.id.ToString() + ".png"));
							ms.Dispose();
							dms.Images.Add(img);
						}
						else
						{
							dms.Images.Add(System.Drawing.Image.FromFile(Path.Combine(dsfolderDm, data.id.ToString() + ".png")));
						}
					}
					catch
					{
						dms.Images.Add(Properties.Resources._default);
					}
				}
			}
			return dms;
		}
		public static ImageList GetServerIcons(Server[] server)
		{
			ImageList image = new ImageList
			{
				ImageSize = new Size(48, 48),
				ColorDepth = ColorDepth.Depth32Bit
			};
			string serverUrl = "";
			image.Images.Add(Properties.Resources._default);
			for (int i = 0; i < server.Length; i++)
			{
				try
				{
					serverUrl = DiscordHelper.GetServerUrl(server[i].id.ToString(), server[i].icon.ToString());
					WebClient wc = new WebClient();
					string temp = Path.GetTempPath();
					string dsfolder = Path.Combine(temp, "DickSword");
					string dsfolderServer = Path.Combine(dsfolder, "server");
					if (!Directory.Exists(dsfolder))
					{
						Directory.CreateDirectory(dsfolder);
					}
					if (!Directory.Exists(dsfolderServer))
					{
						Directory.CreateDirectory(dsfolderServer);
					}
					if (!File.Exists(Path.Combine(dsfolderServer, server[i].id.ToString() + ".png")))
					{
						byte[] bytes = wc.DownloadData(serverUrl);
						MemoryStream ms = new MemoryStream(bytes);
						System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
						img.Save(Path.Combine(dsfolderServer, server[i].id.ToString() + ".png"));
						image.Images.Add(img);
						ms.Dispose();
					}
					else
					{
						image.Images.Add(System.Drawing.Image.FromFile(Path.Combine(dsfolderServer, server[i].id.ToString() + ".png")));
					}
				}
				catch
				{
					image.Images.Add(Properties.Resources._default);
				}
			}
			return image;
		}

	}
}
