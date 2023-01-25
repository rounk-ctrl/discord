using fastJSON;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Websocket.Client;

namespace discord
{
	public partial class Form1 : Form
	{
		WebsocketClient client;
		int heartbeat = 50000;
		bool heartbeatRecieved = false;
		private static readonly ManualResetEvent ExitEvent = new ManualResetEvent(false);
		[DllImport("user32.dll")]
		public static extern int SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
		[DllImport("uxtheme.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Unicode)]
		public static extern int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
		public Form1()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			ListViewItem_SetSpacing(listView1, 48 + 10, 48 + 5);
			ListViewItem_SetSpacing(listView2, 158, 2);
			SetWindowTheme(treeView1.Handle, "Explorer", null);
			SetWindowTheme(listView2.Handle, "Explorer", null);
			this.Shown += Form1_Shown;
			this.FormClosing += Form1_FormClosing;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			client.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "1000");
		}

		void Form1_Shown(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.id))
			{
				textBox1.Visible = false;
				button1.Visible = false;
				//SetUp();
			}
			ListViewItem directmess = new ListViewItem();
			directmess.Text = "";
			directmess.ImageIndex = 0;
			directmess.ToolTipText = "Direct Messages";
			listView1.Items.Add(directmess);
			listView1.LargeImageList = srvicons;
			SetUp();
		}
		public int MakeLong(short lowPart, short highPart)
		{
			return (int)(((ushort)lowPart) | (uint)(highPart << 16));
		}

		public void ListViewItem_SetSpacing(ListView listview, short leftPadding, short topPadding)
		{
			const int LVM_FIRST = 0x1000;
			const int LVM_SETICONSPACING = LVM_FIRST + 53;
			SendMessage(listview.Handle, LVM_SETICONSPACING, IntPtr.Zero, (IntPtr)MakeLong(leftPadding, topPadding));
		}
		Color ContrastColor(Color color)
		{
			int d = 0;

			// Counting the perceptive luminance - human eye favors green color...      
			double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

			if (luminance > 0.5)
				d = 0; // bright colors - black font
			else
				d = 255; // dark colors - white font

			return Color.FromArgb(d, d, d);
		}
		private async void SetUp()
		{
			button1.Visible = false;
			textBox1.Visible = false;
			progressBar1.BringToFront();
			label3.BringToFront();
			progressBar1.Visible = true;
			label3.Visible = true;
			listView2.Visible = true;
			button2.Visible = true;
			label3.Text = "Getting user info...";
			User user = await Task.Run(() => DiscordHelper.GetUserInfo().Result);
			label1.Text = user.username;
			label4.Text = "#" + user.discriminator;
			var avatarUrl = !string.IsNullOrWhiteSpace(user.avatar) ? DiscordHelper.GetAvatarUrl(user.id.ToString(), user.avatar.ToString(), 64) : DiscordHelper.GetDefaultAvatarUrl(user.discriminator);
			pictureBox1.ImageLocation = avatarUrl;
			//label2.Text = user.bio.ToString();
			Color color = Color.Empty;
			if (!string.IsNullOrWhiteSpace(user.banner_color))
			{
				color = ColorTranslator.FromHtml(user.banner_color);
			}
			else
			{
				Int32.TryParse(user.discriminator, out int num);
				switch (num % 5)
				{
					case 0:
						{
							color = Color.FromArgb(88, 101, 242);
							break;
						}
					case 1:
						{
							color = Color.FromArgb(117, 126, 138);
							break;
						}
					case 2:
						{
							color = Color.FromArgb(59, 165, 92);
							break;
						}
					case 3:
						{
							color = Color.FromArgb(250, 166, 26);
							break;
						}
					case 4:
						{
							color = Color.FromArgb(237, 66, 69);
							break;
						}
					case 5:
						{
							color = Color.FromArgb(235, 69, 159);
							break;
						}
					default:
						break;
				}
			}
			label1.BackColor = label4.BackColor = pictureBox2.BackColor = color;
			label1.ForeColor = label4.ForeColor = ContrastColor(color);
			label4.Location = new Point((label1.Location.X + label1.Size.Width) - 5, label4.Location.Y);

			label3.Text = "Getting DMs...";
			DM[] dynObj = await Task.Run(() => DiscordHelper.GetDMs());
			dmid = await Task.Run(() => DiscordHelper.GetDMIds(dynObj));
			List<string> dms = await Task.Run(() => DiscordHelper.GetDMNames(dynObj));
			listView2.BeginUpdate();
			//dms.Reverse();
			for (int i = 0; i < dms.Count; i++)
			{
				ListViewItem dm = new ListViewItem();
				dm.Text = dms[i];
				dm.ImageIndex = i;
				dm.Tag = dmid[i];
				listView2.Items.Add(dm);
			}
			listView2.EndUpdate();

			label3.Text = "Getting Servers...";
			Server[] dynservObj = await Task.Run(() => DiscordHelper.GetServers());
			List<string> srvnames = await Task.Run(() => DiscordHelper.GetServerNames(dynservObj));
			srvid = await Task.Run(() => DiscordHelper.GetServerIDs(dynservObj));
			listView1.BeginUpdate();
			for (int j = 0; j < srvnames.Count; j++)
			{
				ListViewItem server = new ListViewItem();
				server.ImageIndex = j + 1;
				server.Tag = srvid[j];
				server.ToolTipText = (string)srvnames[j];
				listView1.Items.Add(server);
			}
			listView1.EndUpdate();
			label3.Text = "Connecting to the gateway...";
			OpCode hb = new OpCode();
			client = new WebsocketClient(new Uri("wss://gateway.discord.gg/?v=9&encoding=json"));
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			Task.Run(() => 
			client.MessageReceived.Subscribe(msg =>
			{
				hb = JSON.ToObject<OpCode>(msg.Text);
				if (hb.op == 10)
				{
					heartbeat = hb.d.heartbeat_interval;
					Random rand = new Random();
					double jitter = 0.1;
					Thread.Sleep((int)(heartbeat * jitter));
					client.Send("{\"op\":1,\"d\":null}");
				}
				else if (hb.op == 1)
				{
					client.Send("{\"op\":1,\"d\":null}");
				}
				else if (hb.op == 11)
				{
					if (!heartbeatRecieved)
					{
						heartbeatRecieved = true;
						int ok = 125;
						client.Send("{\"op\":2,\"d\":" + "{\"token\":\"" + DiscordHelper.token + "\",\"intents\":" + ok.ToString() + ",\"properties\":{\"os\":\"Linux\",\"browser\":\"Chrome\",\"device\":\"\"}}}");
					}
				}
				else if (hb.t == "READY")
				{
					MessageBox.Show("user online!");
				}
				else if (hb.t == "MESSAGE_CREATE")
				{
					MessageBox.Show(msg.Text);
				}
			}));
			//client.Stop(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "");
			await client.Start();
			Task.Run(() => StartSendingPing(client));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
			progressBar1.Visible = false;
			label3.Visible = false;
			button2.Visible = false;
			srvicons = await Task.Run(() => DiscordHelper.GetServerIcons(dynservObj));
			listView1.LargeImageList = srvicons;
			ImageList dmicons = await Task.Run(() => DiscordHelper.GetDMIcons(dynObj));
			listView2.SmallImageList = dmicons;
			await Task.Run(() => ExitEvent.WaitOne());
		}
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
		public async Task StartSendingPing(WebsocketClient client)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
		{
			while (true)
			{
				Thread.Sleep(heartbeat);
				client.Send("{\"op\":1,\"d\":null}");
			}
		}
		List<string> dmid;
		List<string> srvid;
		ImageList srvicons;
		private async void listView2_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView2.FocusedItem == null) return;
			int p = listView2.FocusedItem.Index;
			string id = listView2.FocusedItem.Tag.ToString();
			var Response = await DiscordHelper.GetResponseAsync("channels/" + id + "/messages", CancellationToken.None);
			Message[] messages = JSON.ToObject<Message[]>(Response.Content.ReadAsStringAsync().Result);
			var dataSet = new List<Tuple<string, string>>();

			foreach (var mes in messages)
			{
				dataSet.Add(new Tuple<string, string>(mes.author.username, mes.content));
			}
			dataSet.Reverse();
			listBox1.DataSource = dataSet;
			if (!textBox1.Visible)
			{
				textBox1.Visible = true;
				button1.Visible = true;
			}
			if (!listBox1.Visible)
				listBox1.Visible = true;
			listBox1.Focus();
			SendKeys.Send("{END}");
		}

		private void listView2_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
		{
			e.Cancel = true;
			e.NewWidth = listView2.Columns[e.ColumnIndex].Width;
		}
		private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
		{
			Brush roomsBrush = SystemBrushes.ControlText;
			if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
			{
				return;
			}

			var linePen = new Pen(SystemBrushes.Control);
			var lineStartPoint = new Point(e.Bounds.Left, e.Bounds.Height + e.Bounds.Top);
			var lineEndPoint = new Point(e.Bounds.Width, e.Bounds.Height + e.Bounds.Top);

			e.Graphics.DrawLine(linePen, lineStartPoint, lineEndPoint);
			e.DrawBackground();
			var dataItem = listBox1.Items[e.Index] as Tuple<string, string>;
			var timeFont = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
			RectangleF rectF = new RectangleF();
			rectF.X = e.Bounds.Left + 3;
			rectF.Y = e.Bounds.Top + 2;
			rectF.Width = listBox1.Width - 25;
			rectF.Height = ((int)e.Graphics.MeasureString(dataItem.Item1, timeFont, listBox1.Width - 25, StringFormat.GenericDefault).Height);
			e.Graphics.DrawString(dataItem.Item1, timeFont, Brushes.Black, rectF, StringFormat.GenericDefault);
			var roomsFont = new Font("Segoe UI", 9.5f, FontStyle.Regular);
			rectF.Y = e.Bounds.Top + 18;
			int hei = ((int)e.Graphics.MeasureString(dataItem.Item2, roomsFont, listBox1.Width - 25, StringFormat.GenericDefault).Height);
			if (hei == 0)
				hei = 40;
			else
				rectF.Height = hei;
			e.Graphics.DrawString(dataItem.Item2, roomsFont, roomsBrush, rectF, StringFormat.GenericDefault);
		}

		private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
		{
			var dataItem = listBox1.Items[e.Index] as Tuple<string, string>;
			var roomsFont = new Font("Segoe UI", 9.5f, FontStyle.Regular);
			var height = ((int)e.Graphics.MeasureString(dataItem.Item2, roomsFont, listBox1.Width - 25, StringFormat.GenericDefault).Height) + ((int)e.Graphics.MeasureString(dataItem.Item1, roomsFont, listBox1.Width - 25, StringFormat.GenericDefault).Height);
			if (height == 18)
				e.ItemHeight = 40;
			else
				e.ItemHeight = height;
		}
		private async void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.FocusedItem != null) listView1.FocusedItem = null;
			if (listView1.FocusedItem == null) return;
			if (listView2.Visible)
				listView2.Visible = false;
			int p = listView1.FocusedItem.Index - 1;
			string id;
			if (p == -1)
			{
				treeView1.Visible = false;
				listView2.Visible = true;
				return;
			}
			else
			{
				id = listView1.FocusedItem.Tag.ToString();
			}
			if (!treeView1.Visible)
				treeView1.Visible = true;
			var Response = await DiscordHelper.GetResponseAsync("guilds/" + id + "/channels", CancellationToken.None);
			Channel[] channel = JSON.ToObject<Channel[]>(Response.Content.ReadAsStringAsync().Result);
			if (treeView1.Nodes.Count != 0)
				treeView1.Nodes.Clear();
			id = "";
			for (int i = 0; i < channel.Length; i++)
			{
				TreeNode lvi = new TreeNode();
				if (channel[i].type == 4)
				{
					lvi.Text = channel[i].name;
					id = channel[i].id;
				}
				for (int j = 0; j < channel.Length; j++)
				{
					if ((!string.IsNullOrWhiteSpace(channel[j].parent_id)) && (channel[j].parent_id == id))
					{
						TreeNode ok = new TreeNode();
						ok.Text = channel[j].name;
						ok.Tag = channel[j].id;
						lvi.Nodes.Add(ok);
					}
				}
				if (!string.IsNullOrWhiteSpace(lvi.Text))
					treeView1.Nodes.Add(lvi);
			}
		}

		private async void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			if (treeView1.SelectedNode == null) return;
			int p = treeView1.SelectedNode.Index;
			string id;
			try
			{
				id = treeView1.SelectedNode.Tag.ToString();
			}
			catch
			{
				return;
			}
			var Response = await DiscordHelper.GetResponseAsync("channels/" + id + "/messages", CancellationToken.None);
			Message[] messages = JSON.ToObject<Message[]>(Response.Content.ReadAsStringAsync().Result);
			var dataSet = new List<Tuple<string, string>>();

			foreach (var mes in messages)
			{
				dataSet.Add(new Tuple<string, string>(mes.author.username, mes.content));
			}
			dataSet.Reverse();
			listBox1.DataSource = dataSet;
			if (!textBox1.Visible)
			{
				textBox1.Visible = true;
				button1.Visible = true;
			}
			listBox1.TopIndex = listBox1.Items.Count - 1;
			if (!listBox1.Visible)
				listBox1.Visible = true;

			listBox1.Focus();
			SendKeys.Send("{END}");
		}

		private void menuItem2_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Properties.Settings.Default.id = null;
			Properties.Settings.Default.Save();
		}
	}
}
