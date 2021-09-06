namespace UnityEngine.ProBuilder
{
	public class ActionResult
	{
		public enum Status
		{
			Success = 0,
			Failure = 1,
			Canceled = 2,
			NoChange = 3,
		}

		public ActionResult(ActionResult.Status status, string notification)
		{
		}

	}
}
