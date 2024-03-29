﻿namespace SpaceTraders.Features.StatusFeature;

public class GameStatus
{
	public string Status { get; set; }

	public bool IsOnline
	{
		get => Status == "spacetraders is currently online and available to play";
	}
}