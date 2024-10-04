using UnityEngine;
using OculusSampleFramework; // Oculus IntegrationのHand Tracking用名前空間を追加

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // 生成するオブジェクト
    public OVRHand leftHand;        // 左手のハンドトラッキング情報を取得するためのOVRHand
    public float spawnDistance = 0.5f; // カメラからどれだけ離れた位置にオブジェクトを生成するか
    private GameObject spawnedObject; // 生成したオブジェクトを保持する

    void Update()
    {
        // 左手が存在し、トラッキングされている場合
        if (leftHand != null && leftHand.IsTracked)
        {
            // 親指と人差し指がつまんでいる状態を取得
            bool isPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

            // オブジェクトがまだ生成されていない場合、つまんだときに生成
            if (isPinching && spawnedObject == null)
            {
                // カメラの位置を基にオブジェクトを生成
                Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
                spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
            }

            // オブジェクトが生成されている場合、カメラの視線に追従させる
            if (spawnedObject != null)
            {
                Vector3 followPosition = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
                spawnedObject.transform.position = followPosition;
            }

            // 指を離したらオブジェクトを消去
            if (!isPinching && spawnedObject != null)
            {
                Destroy(spawnedObject);
                spawnedObject = null; // 生成したオブジェクトの参照をリセット
            }
        }
    }
}
