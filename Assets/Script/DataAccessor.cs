using UnityEngine;
using System.Collections;
using System.IO;
using Newtonsoft.Json;

public class DataAccessor
{
	public static T LoadObjectFromJsonFile<T>(string path)
	{
		TextReader reader = new StreamReader(path);
		if(reader == null)
		{
			Debug.LogError("Cannot find " + path);
			reader.Close();
			return default(T);
		}
		
		T data = JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
		if(data == null)
		{
			Debug.LogError("Cannot read data from " + path);
		}
		
		reader.Close();
		return data;
	}
	
	public static void SaveObjectToJsonFile<T>(T data, string path)
	{
		TextWriter tw = new StreamWriter(path);
		if(tw == null)
		{
			Debug.LogError("Cannot write to " + path);
			return;
		}
		
		string jsonStr = JsonConvert.SerializeObject(data);
		tw.Write(jsonStr);
		tw.Flush();
		tw.Close();
	}


	public static string GetFishPathByType(int fishType)
	{
		return FishConfigJsonPath + fishType.ToString()+".json";
	}

	public static string GetTracePathByID(int traceID)
	{
		return TraceDirectory + traceID.ToString()+".json";
	}

	public static string GetGroupFishPathByID(int id)
	{
		return GroupFishDirectory + id.ToString()+".json";
	}



	public static readonly string GroupFishDirectory = "Assets/Resources/EditorData/鱼阵配置/Json文件/";
	public static readonly string TraceDirectory = "Assets/Resources/EditorData/曲线轨迹/Json文件/";
	public static readonly string FishConfigJsonPath = "Assets/Resources/EditorData/鱼配置/Json文件/";
}
