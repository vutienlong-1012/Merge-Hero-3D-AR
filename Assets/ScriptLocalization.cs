using UnityEngine;

namespace I2.Loc
{
	public static class ScriptLocalization
	{

		public static string LEVEL 		{ get{ return LocalizationManager.GetTranslation ("LEVEL"); } }
		public static string SWIPE_TO_MOVE 		{ get{ return LocalizationManager.GetTranslation ("SWIPE TO MOVE"); } }
		public static string LOCKED 		{ get{ return LocalizationManager.GetTranslation ("LOCKED"); } }
		public static string SPEED 		{ get{ return LocalizationManager.GetTranslation ("SPEED"); } }
		public static string FREE 		{ get{ return LocalizationManager.GetTranslation ("FREE"); } }
		public static string LOADING 		{ get{ return LocalizationManager.GetTranslation ("LOADING"); } }
		public static string DAY 		{ get{ return LocalizationManager.GetTranslation ("DAY"); } }
	}

    public static class ScriptTerms
	{

		public const string LEVEL = "LEVEL";
		public const string SWIPE_TO_MOVE = "SWIPE TO MOVE";
		public const string LOCKED = "LOCKED";
		public const string SPEED = "SPEED";
		public const string FREE = "FREE";
		public const string LOADING = "LOADING";
		public const string DAY = "DAY";
	}
}