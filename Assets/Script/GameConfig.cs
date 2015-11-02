using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class GameConfig
{
	private static Dictionary<int, FishConfig> _fishConfigDic = new Dictionary<int, FishConfig>();
	public static FishConfig GetFishConfig(int fishType)
	{
		if(_fishConfigDic.ContainsKey(fishType))
		{
			return _fishConfigDic[fishType];
		}

		string path = DataAccessor.GetFishPathByType(fishType);
		FishConfig config = DataAccessor.LoadObjectFromJsonFile<FishConfig>(path);
		if(config != null)
		{
			_fishConfigDic[fishType] = config;
		}

		return config;
	}

	private static Dictionary<int, TraceConfig> _traceConfig = new Dictionary<int, TraceConfig>();
	public static TraceConfig GetTraceConfig(int id)
	{
		if(_traceConfig.ContainsKey(id))
		{
			return _traceConfig[id];
		}

		string path = DataAccessor.GetTracePathByID(id);

		TraceConfigSerializable traceData = DataAccessor.LoadObjectFromJsonFile<TraceConfigSerializable>(path);
		List<MovePoint> configData = new List<MovePoint>();
		foreach (var item in traceData.MovePoints) 
		{
			configData.Add(new MovePoint(item.x, item.y, item.Angle));
		}
		TraceConfig config = new TraceConfig{MovePoints = configData};
		if(traceData != null)
		{
			_traceConfig[id] = config;
		}
		return config;
	}
}


