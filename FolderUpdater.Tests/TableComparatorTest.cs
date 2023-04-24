using SUpdater.FolderUpdater;

namespace FolderUpdater.Tests
{
	[TestClass]
	public class TableComparatorTest
	{
		[TestMethod]
		public void GetNewLocalFiles_10arrAnd10arr_return0()
		{
			FileInfoHash[] arr1 = new FileInfoHash[10];
			FileInfoHash[] arr2 = new FileInfoHash[arr1.Length];

			for (int i = 0; i < arr1.Length; i++)
			{
				arr1[i] = new FileInfoHash(i.ToString(), "test");
				arr2[i] = new FileInfoHash(i.ToString(), "test");
			}

			FileInfoHash[] result = TableComparator.GetNewLocalFiles(arr1, arr2);
			Assert.IsTrue(result.Length == 0);
		}
		[TestMethod]
		public void GetNewLocalFiles_10arrAnd10arr_return1()
		{
			FileInfoHash[] arr1 = new FileInfoHash[10];
			FileInfoHash[] arr2 = new FileInfoHash[9];

			for (int i = 0; i < arr1.Length; i++)
				arr1[i] = new FileInfoHash(i.ToString(), "test");

			for (int i = 0; i < arr2.Length; i++)
				arr2[i] = new FileInfoHash(i.ToString(), "test");


			FileInfoHash[] result = TableComparator.GetNewLocalFiles(arr2, arr1);
			Assert.IsTrue(result.Length == 1);
		}
		[TestMethod]
		public void GetNewLocalFiles_10arrAnd10arr_return1_Error()
		{
			FileInfoHash[] arr1 = new FileInfoHash[10];
			FileInfoHash[] arr2 = new FileInfoHash[9];

			for (int i = 0; i < arr1.Length - 1; i++)
				arr1[i] = new FileInfoHash(i.ToString(), "test");
			arr1[9] = new FileInfoHash("Error", "test");
			for (int i = 0; i < arr2.Length; i++)
				arr2[i] = new FileInfoHash(i.ToString(), "test");


			FileInfoHash[] result = TableComparator.GetNewLocalFiles(arr2, arr1);
			Assert.IsTrue(result.Length == 1 && result[0].Name == "Error");
		}

		//public void GetDifferences
	}
}