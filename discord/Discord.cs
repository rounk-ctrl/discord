using System;
using System.Collections.Generic;
namespace discord
{
    #region User
    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string discriminator { get; set; }
        public string avatar { get; set; }
        public object avatar_decoration { get; set; }
        public bool bot { get; set; }
        public bool system { get; set; }
        public bool mfa_enabled { get; set; }
        public object banner { get; set; }
        public int accent_color { get; set; }
        public string locale { get; set; }
        public bool verified { get; set; }
        public string email { get; set; }
        public int flags { get; set; }
        public int premium_type { get; set; }
        public int public_flags { get; set; }
        public string banner_color { get; set; }
        public string bio { get; set; }
        public bool? nsfw_allowed { get; set; }
        public string phone { get; set; }
    }
    #endregion
    #region Server
    public class Server
    {
        public string id { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public bool owner { get; set; }
        public string permissions { get; set; }
        public string[] features { get; set; }
    }
    #endregion
    #region DM
    public class Recipient
    {
        public string id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public object avatar_decoration { get; set; }
        public string discriminator { get; set; }
        public int public_flags { get; set; }
        public bool? bot { get; set; }
    }

    public class DM
    {
        public string id { get; set; }
        public int type { get; set; }
        public string last_message_id { get; set; }
        public int flags { get; set; }
        public DateTime last_pin_timestamp { get; set; }
        public Recipient[] recipients { get; set; }
        public string name { get; set; }
        public string icon { get; set; }
        public string owner_id { get; set; }
    }
    #endregion
    #region Message
    public class Attachment
    {
        public string id { get; set; }
        public string filename { get; set; }
        public int size { get; set; }
        public string url { get; set; }
        public string proxy_url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string content_type { get; set; }
    }

    public class Author
    {
        public string id { get; set; }
        public string username { get; set; }
        public string avatar { get; set; }
        public object avatar_decoration { get; set; }
        public string discriminator { get; set; }
        public int public_flags { get; set; }
        public bool? bot { get; set; }
        public string name { get; set; }
        public string icon_url { get; set; }
        public string proxy_icon_url { get; set; }
    }

    public class Component
    {
        public int type { get; set; }
        public List<Component> components { get; set; }
        public int style { get; set; }
        public string label { get; set; }
        public string custom_id { get; set; }
        public bool disabled { get; set; }
    }

    public class Embed
    {
        public string type { get; set; }
        public string url { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int color { get; set; }
        public Author author { get; set; }
        public Image image { get; set; }
        public Thumbnail thumbnail { get; set; }
        public Footer footer { get; set; }
    }

    public class Footer
    {
        public string text { get; set; }
        public string icon_url { get; set; }
        public string proxy_icon_url { get; set; }
    }

    public class Image
    {
        public string url { get; set; }
        public string proxy_url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Message
    {
        public string id { get; set; }
        public int type { get; set; }
        public string content { get; set; }
        public string channel_id { get; set; }
        public Author author { get; set; }
        public List<Attachment> attachments { get; set; }
        public List<Embed> embeds { get; set; }
        public List<object> mentions { get; set; }
        public List<object> mention_roles { get; set; }
        public bool pinned { get; set; }
        public bool mention_everyone { get; set; }
        public bool tts { get; set; }
        public DateTime timestamp { get; set; }
        public object edited_timestamp { get; set; }
        public int flags { get; set; }
        public List<Component> components { get; set; }
    }

    public class Thumbnail
    {
        public string url { get; set; }
        public string proxy_url { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }


#endregion
    #region Channel
    public class AvailableTag
    {
        public string id { get; set; }
        public string name { get; set; }
        public string emoji_id { get; set; }
        public object emoji_name { get; set; }
        public bool moderated { get; set; }
    }

    public class DefaultReactionEmoji
    {
        public string emoji_id { get; set; }
        public object emoji_name { get; set; }
    }

    public class PermissionOverwrite
    {
        public string id { get; set; }
        public int type { get; set; }
        public string allow { get; set; }
        public string deny { get; set; }
    }

    public class Channel
    {
        public string id { get; set; }
        public int type { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public int flags { get; set; }
        public string parent_id { get; set; }
        public string guild_id { get; set; }
        public List<PermissionOverwrite> permission_overwrites { get; set; }
        public string last_message_id { get; set; }
        public string topic { get; set; }
        public DateTime? last_pin_timestamp { get; set; }
        public int? rate_limit_per_user { get; set; }
        public bool? nsfw { get; set; }
        public int? default_thread_rate_limit_per_user { get; set; }
        public int? bitrate { get; set; }
        public int? user_limit { get; set; }
        public object rtc_region { get; set; }
        public int? default_auto_archive_duration { get; set; }
        public List<AvailableTag> available_tags { get; set; }
        public string template { get; set; }
        public DefaultReactionEmoji default_reaction_emoji { get; set; }
        public object default_sort_order { get; set; }
    }
	#endregion
	#region OpCode
    public class OpCode
	{
        public string t { get; set; }
        public string s { get; set; }
        public int op { get; set; }
        public HeartBeat d { get; set; }
	}
    public class MsgCreate
    {
        public string t { get; set; }
        public string s { get; set; }
        public int op { get; set; }
        public HeartBeat d { get; set; }
    }
    /*
    public class Message
	{
        public string guild_id { get; set; }
        public GuildMember member { get; set; }
	}*/
    public class HeartBeat : Object
	{
        public int heartbeat_interval { get; set; }
        public string[] _trace { get; set; }

    }
	#endregion
}
