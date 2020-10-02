using System;
using System.Globalization;
using System.IO;
using UnityEngine;


namespace UMGS.Log
{


	public static class Logger
	{

		public static bool IsActivated = true;
		public static bool IsFileSave  = false;

		public static void Log<T>(this T type, string message)
		{
			if (!IsActivated) return;
			message = "[" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "]" + typeof(T).Name + " - " + message;
			Debug.Log(message);
			if (!IsFileSave) return;
			using (StreamWriter writer = File.AppendText($"{Application.persistentDataPath}/EventLog.txt"))
			{
				writer.WriteLine(message);
			}
		}

		public static void Log(string message)
		{
			Debug.Log(message);
		}

	}

	public class UMLogger : ILogHandler
	{

		public static bool         logEnabled => false;
		private       FileStream   m_FileStream;
		private       StreamWriter m_StreamWriter;
		public        ILogHandler  logHandler = Debug.unityLogger.logHandler;
		static        UMLogger     instance;

		[RuntimeInitializeOnLoadMethod]
		static void Initialize()
		{
			if (instance == null)
			{
				instance = new UMLogger();
			}
		}

		UMLogger()
		{
			if (!logEnabled) return;
			string filePath = Application.persistentDataPath + "/MyLogs.txt";
			m_FileStream   = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
			m_StreamWriter = new StreamWriter(m_FileStream);

			// Replace the default debug log handler
			Debug.unityLogger.logHandler = this;
			Debug.Log("Initialize");
		}

		public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
		{
			m_StreamWriter.WriteLine(String.Format(format, args));
			m_StreamWriter.Flush();
			logHandler.LogFormat(logType, context, format, args);
		}

		public void LogException(Exception exception, UnityEngine.Object context)
		{
			logHandler.LogException(exception, context);
		}

	}


}