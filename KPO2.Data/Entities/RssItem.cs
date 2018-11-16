namespace KPO2.Data.Entities 
{
	using System;
	using Types;

	public class RssItem : BaseEntity
	{
		public RssSource Source { get; set; }
		public string Title {get;set;}
		public string Link {get;set;}
		public DateTime Date {get;set;}
	}
}
