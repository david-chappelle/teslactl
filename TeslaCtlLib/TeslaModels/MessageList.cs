using System;
using System.IO;

namespace TeslaLib.TeslaModels
{
	public class MessageList : ILoggable
	{
		public int UnreadCount { get; set; }
		public int TotalCount { get; set; }
		public Message[] Messages { get; set; }
		public MessagePagination Pagination { get; set; }

		public void Log(TextWriter writer)
		{
			Console.WriteLine("Unread {0} of {1}", this.UnreadCount, this.TotalCount);
			for (int i = 0; i < this.Messages.Length; i++)
			{
				if (i > 0)
					Console.WriteLine();

				var message = this.Messages[i];
				Console.WriteLine($"Id: {message.Id}");
				Console.WriteLine($"Date: {message.Date}");
				Console.WriteLine($"Status: {message.Status}");
				Console.WriteLine($"Priority: {message.Priority}");
				Console.WriteLine($"Category: {message.Category}");
				Console.WriteLine($"Title: {message.Summary.Title}");
				Console.WriteLine($"Description: {message.Summary.Description}");
				Console.WriteLine($"Content Available: {message.Summary.ContentAvailable}");
			}
		}
	}

	public class Message
	{
		public DateTime Date { get; set; }
		public string Id { get; set; }
		public string Status { get; set; }
		public string Priority { get; set; }
		public string Category { get; set; }
		public MessageSummary Summary { get; set; }
		public MessageAction[] Actions { get; set; }
	}

	public class MessageSummary
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public bool ContentAvailable { get; set; }
	}

	public class MessageAction
	{
		public string DisplayType { get; set; }
		public MessageActionType[] ActionTypes { get; set; }
	}

	public class MessageActionType
	{
		public string Action { get; set; }
		public string Title { get; set; }
		public string Type { get; set; }
	}

	public class MessagePagination
	{
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int TotalPages { get; set; }
	}
}
