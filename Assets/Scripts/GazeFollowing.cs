using UnityEngine;

public class CenterObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab; // 生成する3Dオブジェクトのプレハブ
    public float spawnDistance = 2.0f; // カメラからの距離
    public float horizontalOffset = 0.5f; // 左右のオフセット
    public float verticalOffset = 0.0f; // 上下のオフセット

    private Camera mainCamera; // OVRCameraRigのカメラを格納する変数
    private GameObject spawnedObject; // 生成されたオブジェクトを格納する変数
    private bool isFollowing = false; // オブジェクトが追従するかどうかを管理するフラグ

    void Start()
    {
        // OVRCameraRigのメインカメラを取得
        mainCamera = Camera.main;
    }

    void Update()
    {
        // スペースキーを押すたびに追従ON/OFFを切り替える
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isFollowing = !isFollowing; // 追従のON/OFFを切り替える

            if (isFollowing && spawnedObject == null)
            {
                // 追従がONでかつオブジェクトがまだ生成されていない場合に生成
                SpawnObjectInCenter();
            }
        }

        // 追従がONの場合、オブジェクトがカメラの視線に追従
        if (isFollowing && spawnedObject != null)
        {
            UpdateObjectPosition();
        }
    }

    void SpawnObjectInCenter()
    {
        // カメラの前方にオブジェクトを配置し、オフセットを適用
        Vector3 spawnPosition = mainCamera.transform.position +
                                mainCamera.transform.forward * spawnDistance +
                                new Vector3(horizontalOffset, verticalOffset, 0);
        spawnedObject = Instantiate(objectPrefab, spawnPosition, Quaternion.identity);
    }

    void UpdateObjectPosition()
    {
        // カメラの前方にオブジェクトを追従し、オフセットを適用
        Vector3 updatedPosition = mainCamera.transform.position +
                                  mainCamera.transform.forward * spawnDistance +
                                  new Vector3(horizontalOffset, verticalOffset, 0);
        spawnedObject.transform.position = updatedPosition;
    }
}
