using System;

using System.Collections.Generic;
namespace MacBookAirCSharp
{
    public class Josepus
	{
		//	요세푸스(Josepus) 알고리즘
		//	1) N, K를 입력받습니다.
		//	2) 큐를 생성합니다.
		//	3) 큐에 1부터 N까지 값을 추가합니다.
		//	4) 큐가 비어있지 않는 한 반복
		//		4-1) K-1개의 데이터를 큐에서 제거하고 다시 큐에 추가합니다.
		//		4-2) 1개의 데이터를 큐에서 제거하고 그 값을 출력합니다.
		//	예제) (7, 3) --> 결과 출력 채팅창에, (11, 5) --> 결과 출력 채팅창에
		public Josepus()
		{
			Console.Write("n, k : ");
			var s = Console.ReadLine().Split();
			int n = Convert.ToInt32(s[0]), k = Convert.ToInt32(s[1]);
			var queue = new Queue<int>();
			var result = new List<int>();
			for (int i = 1; i <= n; i++) queue.Enqueue(i);
			while (queue.Count > 0)
			{
				for (int i = 0; i < k - 1; i++) queue.Enqueue(queue.Dequeue());
				result.Add(queue.Dequeue());
			}
			Console.WriteLine(string.Join(" ", result));
		}
    }
}
