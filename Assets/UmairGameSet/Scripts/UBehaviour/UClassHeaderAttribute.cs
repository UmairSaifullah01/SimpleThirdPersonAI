using System;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
public sealed class UClassHeaderAttribute : Attribute
{

	public string header;
	public bool   openClose;
	public string iconName;
	public bool   useHelpBox;
	public string helpBoxText;

	public UClassHeaderAttribute(string header, bool openClose = true, string iconName = "icon_v2", bool useHelpBox = false, string helpBoxText = "")
	{
		this.header      = header;
		this.openClose   = openClose;
		this.iconName    = iconName;
		this.useHelpBox  = useHelpBox;
		this.helpBoxText = helpBoxText;
	}

	public UClassHeaderAttribute(string header, string helpBoxText)
	{
		this.header      = header;
		this.openClose   = true;
		this.iconName    = "icon_v2";
		this.useHelpBox  = true;
		this.helpBoxText = helpBoxText;
	}

}